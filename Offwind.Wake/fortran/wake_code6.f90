MODULE GeneralData
        IMPLICIT NONE
        INTEGER (kind=4)::N_TURB, IMAX, JMAX
	real (kind=8):: dx,dy, pi, xmax, ymax, ymin, xmin,ang
      

      real (kind=8), ALLOCATABLE :: x(:),y(:), vell_i(:,:)
      real (kind=8), ALLOCATABLE :: x_turb(:), y_turb(:), R_TURB (:), WPOWER(:) ! location of the turbine
      integer(kind=4), ALLOCATABLE :: xc_turb(:), yc_turb(:)
END MODULE GeneralData

MODULE SolverData
       USE GeneralData
       IMPLICIT NONE
      REAL (kind=8) :: Ct,Dturb,Kwake,H,Uhub, Dwake
      REAL (kind=8) :: rho, Cp, dist
  
      REAL, DIMENSION(1000,1000)::V,Darea,Darea_D
END MODULE SolverData 

PROGRAM WAKE


!****** declaration of the variable *******************************
      USE GeneralData
      USE SolverData
      IMPLICIT NONE
      INTEGER(kind=4):: i,j,k
      REAL (kind=8) ::ppp
      REAL (kind=4)::  W_coef
      character*80 file1
!*************************************************
      pi=3.1415926535897
!************************************************************************

!ROTATE THE DOMAIN, AND THE X,Y COORDINATE OF THE TURBINE so that the wind to be in x direction
!------------------------------------------------------------------
     CALL READ_DATA
     CALL ROTATE_COORD
     If (Ct>1) THEN
        PRINT *,' The value of the Ct should be less 1, hence Ct=0.3)'
        Ct = 0.3
     ENDIF
     Cp=0.5*(1+sqrt(1-Ct))*Ct
     CALL ORDER 
     ppp=5.
     CALL DOMAIN_PT(x,IMAX, dx, Dturb, x_turb,N_TURB, xmax,xmin,ppp)
     ppp=2.
     CALL DOMAIN_PT(y,JMAX, dy,Dturb, y_turb,N_TURB,ymax,ymin,ppp)    
     Call Turb_centr_coord(N_TURB,IMAX, x,x_turb, xc_turb)
     Call Turb_centr_coord(N_TURB,JMAX, y,y_turb, yc_turb)
     CALL COMPUTE_VELL

     CALL WRITE_DATA

     CALL COMPUTE_WPower
     CALL WRITE_DATA_power


 END !PROGRAM WAKE 
!*****************************************************

!*********************************************************
!   rotate the coordinate of the turbine
!--------------------------------------------
  SUBROUTINE  ROTATE_coord
  USE GeneralDatA
  USE SolverData
  IMPLICIT NONE

  INTEGER i
  REAL (KIND=8):: XX_TURB(N_TURB), YY_TURB(N_TURB), ang1
  ang1=ang*pi/180
  DO i = 1,N_TURB
      XX_TURB(i)=x_turb(i)*cos(ang1)-y_turb(i)*sin(ang1)
      YY_TURB(i)=x_turb(i)*sin(ang1)+y_turb(i)*cos(ang1)
  ENDDO

  DO i = 1,N_TURB
     x_turb(i)= XX_TURB(i)
     y_turb(i)= YY_TURB(i)
  ENDDO
END ! 
!----------------------------------------------------
!************************************************
!  SUBROUTINE READ THE DATA !
!------------------------------------------------
 SUBROUTINE READ_DATA
  USE GeneralDatA
  USE SolverData
  IMPLICIT NONE
  INTEGER :: i,j

         OPEN(unit=30, file='initial_data.inp', status='unknown')

         READ(30,*) IMAX  ! The number of grid points in x direction
         READ(30,*) JMAX  ! The number of the grid points in Y direction
         ALLOCATE (x(IMAX),y(JMAX),vell_i(IMAX,JMAX))
         READ(30 ,*)Dturb ! THE DIAMETER OF THE TURBIN
         READ(30,*)H      !  THE HEIGHT OF THE TURBINE
         READ(30 ,*) Ct   ! TURBINE THRUST COEFFICIENT
         READ(30,*)Kwake  ! wake expand scalar
         READ(30,*)Uhub   !m/s - VELOCITY AT THE HUB, WITHOUT THE INFLUENCE OF THE WIND TURBIN
         READ(30,*)N_TURB !THE NUMBER OF THE TURBINE
         ALLOCATE (x_turb(N_TURB), y_turb(N_TURB), R_TURB(N_TURB),WPOWER(N_TURB)) 
         ALLOCATE (xc_turb(N_TURB), yc_turb(N_TURB))
         READ(30,*) rho  ! THE DENSITY OF THE AIR 
         READ(30,*) dist  ! the distance behind the turbine where the power is computed
         READ(30,*) ang   ! rotational angle of the axis: vellocity has the same direction as Ox
         READ(30,*)
         READ(30,*)
         DO i=1,N_TURB
             READ(30,*)x_turb(i),y_turb(i) ! pozition of the turbine
         ENDDO
         READ(30,*)  

close(30)
END  ! END SUBROUTINE READ DATA


!--------------------------------------------------------------------
!   COMPUTE THE GRID POINTS 
!************************************************************************
SUBROUTINE DOMAIN_PT (XX,IIMAX, DDX, DDtur, XX_TURB,NN_TURB, XXMAX, XXMIN, pppoint)

   IMPLICIT NONE
   INTEGER(kind=4) :: i, IIMAX, NN_TURB
   REAL(kind=8) ::XX(IIMAX)
   REAL (kind=8) ::XX_TURB(NN_TURB) 
   REAL(kind=8) ::XXMAX, XXMIN, DDtur, DDX, pppoint

   XXMAX =XX_TURB(1)
   XXMIN=XX_TURB(1)
   DO i=2,NN_TURB
      If (XX_TURB(i) > XXMAX) XXMAX =XX_TURB(i)
      If (XX_TURB(i) < XXMIN) XXMIN = XX_TURB(i)

   ENDDO

   XXMAX = XXMAX + DDtur*pppoint
   XXMIN = XXMIN - 2*DDtur 
   
   XX(1)= XXMIN 
   DDX=( XXMAX - XXMIN)/(IIMAX -1)
   DO i=2, IIMAX 
      XX(i)= XX(i-1)+DDX
   ENDDO
   pppoint=0.
END ! subroutine that compute the grid points
!-----------------------------------------------------------





!************************************************************?
!   ROTATE COORDINATE THAT THE wind to be in X ? DIRECTION
!------------------------------------------------------------- 

!***********************************************************************
! the subroutine determine the coordinates of the center of the turbine
!*****************************************************

SUBROUTINE Turb_centr_coord(nn,iimax, xx,xx_turb, xxc_turb)
   IMPLICIT NONE
   INTEGER(kind=4)::nn, iimax, i, ii
   INTEGER (kind=4):: xxc_turb(nn)
   REAL (kind=8):: xx_turb(nn), xx(iimax)

   DO i=1,nn
     DO ii=1,iimax-1
        IF (xx(ii) <= xx_turb(i) .and. xx_turb(i) <  xx(ii+1)) THEN
           xxc_turb(i)=ii
           EXIT
        ENDIF
     ENDDO
   ENDDO
END ! subroutine deternines the center coordinates of the turbines
!--------------------------------------------------------------

!*****************************************************************
! ORDER of THE TURBINE in function of x coordinate
!_******************************************************************??? 
 SUBROUTINE ORDER
  USE GeneralDatA
  USE SolverData
  IMPLICIT NONE
  INTEGER i,j, k
  REAL (kind=8)::aa,bb
  DO i=1, N_TURB-1
     DO j=1, N_TURB-i
        If(x_turb(j) > x_turb(j+1)) THEN
            aa= x_turb(j)
            bb= y_turb(j)
            x_turb(j) = x_turb(j+1)
            y_turb(j) = y_turb(j+1)
            x_turb(j+1) = aa
            y_turb(j+1) = bb
        ENDIF
     ENDDO
   ENDDO

!DO j=1,N_turb
  DO i=1, N_TURB
     DO k=i+1, N_TURB
        If(x_turb(i) == x_turb(k)) THEN
           IF (y_turb(i) > y_turb(k))  THEN
              aa= y_turb(i)
              y_turb(i) = y_turb(k)
              y_turb(k) = aa
           endif
        ENDIF
     ENDDO
   ENDDO
!ENDDO

 END ! SUBROUTINE puts in order the turbine
!--------------------------------------------------------------------------


!************************************************************************
! *                         SUBROUTINE  _DATA                        *
!*********************************************************************** 
SUBROUTINE WRITE_DATA
      USE GeneralData
      USE SolverData
      IMPLICIT NONE
      INTEGER i ,j
!	character*80 file1

!	file1='datab.1'

      OPEN(unit=10, file='FLOW.xyz', status='unknown') 
      OPEN(unit=110, file='FLOW.q', status='unknown') 
      write(10,*),IMAX,JMAX
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(10, *) x(i)
         ENDDO
      ENDDO
      write(10,*)
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(10, *) y(j)
         ENDDO
      ENDDO


      write(110,*) IMAX,JMAX
      write(110,*),'0.1   ','  10  ','  10000  ','  0.1 '
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(110, *)rho
         ENDDO
      ENDDO
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(110, *)rho*vell_i(i,j)
         ENDDO
      ENDDO
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(110, *)0
         ENDDO
      ENDDO
      DO j=1,JMAX
         DO i=1,IMAX
            WRITE(110, *)0
         ENDDO
      ENDDO

      CLOSE(10)
      CLOSE(110)

      END
!************************************************************************

!************************************************************************
! *                         SUBROUTINE  _SHADOW AREA 
!*********************************************************************** 
SUBROUTINE COMPUTE_VELL
      USE GeneralData
      USE SolverData
      IMPLICIT NONE
      INTEGER I, J, K, Ni, nj, jj_max, jj_min , ii, nk
      REAL (kind=8):: SHADOW(N_TURB),DIJ, RR_I, ALPHA_I, ALPHA_K, LIJ
      REAL (kind=8):: PP,SS,ss0, RR_k, vv
 
      REAL (kind=8) :: r0, x_dist, rr_max, rrt, area
   
     DO i=1,IMAX
        DO j=1,JMAX
           vell_i(i,j)=Uhub
        ENDDO
     ENDDO

     r0=0.5*Dturb ! all the tubine have the same diameter


 nk=2*int(Dturb/dy)
 DO K =1, N_TURB
    J=0
    SS=0.
    SS0=(pi*r0*r0)
    
    DO I = 1, K-1,1 ! calculate the influence of the turbine i over the turbine k
       RR_I= r0 + Kwake*(x(xc_turb(k))  - x(xc_turb(i)) )
       DIJ= abs(Y_TURB(I)-Y_TURB(K))
       IF(RR_I >= (r0 + DIJ) .or. DIJ<= dy) THEN 
           SS=SS+ ((r0*r0)/(RR_I*RR_I))
       ELSE 
           IF((DIJ) < (RR_I+r0).and.(DIJ)> dy) THEN
              J=J+1
               ALPHA_I=(RR_I*RR_I) +(DIJ*DIJ)-(r0*r0)
               ALPHA_I= ALPHA_I/(2* RR_I*DIJ)
               ALPHA_I=ACOS(ALPHA_I)
               ALPHA_K=(r0*r0)+(DIJ*DIJ)-(RR_I* RR_I)
               ALPHA_K= ALPHA_K/(2* r0*DIJ)
              ALPHA_K=ACOS(ALPHA_K)
              call AAREA(RR_I, r0,DIJ,area)
      
              SHADOW(J)=(ALPHA_I*(RR_I**2)+ ALPHA_K*(r0**2))
              SHADOW(J)= SHADOW(J)-2*area
              SS=SS+((SHADOW(J))/SS0)*((r0*r0)/(RR_I*RR_I))
            ELSE
               SS=SS
            ENDIF

         ENDIF 
             
     ENDDO
     DO ii=xc_turb(k),IMAX
          rrt= r0 + Kwake*(x(ii)- x(xc_turb(k)) )
          rr_max=max(rrt,rr_max)
          nj=INT(rrt/dy)
          jj_min=max(1,yc_turb(k)-nj)
          jj_max= min(JMAX, yc_turb(k)+nj)         

           DO j=jj_min, jj_max
               IF (((-vell_i(ii,j)+Uhub) > 0) .and.( ii >xc_turb(k)+nk)) THEN
                  vv=vell_i(ii,j)
                  vell_i(ii,j)=Uhub + Uhub*(sqrt(1-Ct)-1)*((r0*r0)/(rrt*rrt))
                  vell_i(ii,j)=vell_i(ii,j)*(1-(1-sqrt(1-Ct))*SS)
                  !vell_i(ii,j)=(vell_i(ii,j)+0.15*vv)/1.15
                   vell_i(ii,j)=min(vv,vell_i(ii,j))
               ELSE
                  vell_i(ii,j)=Uhub + Uhub*(sqrt(1-Ct)-1)*(r0/rrt)*(r0/rrt)
                  vell_i(ii,j)=vell_i(ii,j)*(1-(1-sqrt(1-Ct))*SS)
               ENDIF

           ENDDO
      ENDDO


   ENDDO
END   ! subroutine that compute the velocity in front of the wind turbine







!******************************************************************************

!
!************************************************************************
! *       SUBROUTINE  compute the power at the distance dist behind the WT
!*********************************************************************** 
SUBROUTINE COMPUTE_WPower
      USE GeneralData
      USE SolverData
      IMPLICIT NONE
      INTEGER I, J, K, Ni, nj, jj_max, jj_min , ii,JJ, nd, nk,mm
      REAL (kind=8):: SHADOW(N_TURB),DIJ, RR_I, ALPHA_I, ALPHA_K, LIJ
      REAL (kind=8):: PP,SS,ss0, RR_k, vv, SPOWER, vv1, vv2, v_power(N_TURB)
 
      REAL (kind=8) :: r0, x_dist, rr_max, rrt, area

     r0=0.5*Dturb ! all the tubine have the same diameter

 SS0=(pi*r0*r0)
 i=dist/dx
 nd=max(1,i)

 DO K =1, N_TURB
    J=0
    SS=0.
    nk=max(1,xc_turb(k)-nd)
    vv1=vell_i(nk, yc_turb(k))
    WPOWER(K)=0.
    vv2=0.    
    DO I = k-1,1,-1 ! calculate the influence of the turbine i over the turbine k
       RR_I= r0 + Kwake*(x(nk) - x(xc_turb(i)) )
       DIJ= abs(Y_TURB(I)-Y_TURB(K))
       
   
       IF(((DIJ) < (RR_I+r0)).and.(RR_I <= (r0 + DIJ))) THEN
              J=J+1
               ALPHA_I=(RR_I*RR_I) +(DIJ*DIJ)-(r0*r0)
               ALPHA_I= ALPHA_I/(2* RR_I*DIJ)
               ALPHA_I=ACOS(ALPHA_I)
               ALPHA_K=(r0*r0)+(DIJ*DIJ)-(RR_I* RR_I)
               ALPHA_K= ALPHA_K/(2* r0*DIJ)
              ALPHA_K=ACOS(ALPHA_K)
              call AAREA(RR_I, r0,DIJ,area)
      
              SHADOW(J)=(ALPHA_I*(RR_I**2)+ ALPHA_K*(r0**2))
              SHADOW(J)= SHADOW(J)-2*area

              SS=SS + SHADOW(J)
              IF (SS < SS0) THEN 
                if (y_turb(k) > y_turb(i)) THEN
                   mm=int(RR_I/dy)
                    jj_max=min(jmax, yc_turb(i)+mm+1)
                    jj_min=max(1, yc_turb(i)+mm-2)
                    vv1= vell_i(nk,jj_max)
                    v_power(j)= vell_i(nk,jj_min)
                 else
                    mm=int(RR_I/dy)
                   jj_max=min(jmax, yc_turb(i)+mm+1)
                    jj_min=max(1, yc_turb(i)+mm-2)
                    vv1=vell_i(nk,jj_min)
                    v_power(j)= vell_i(nk,jj_max)
                 endif
               ELSE
                   J=J-1
               ENDIF
       ENDIF
             
    ENDDO

       If (j>0) THEN
          DO i=1,j 
             vv2=v_power(j)*SHADOW(j)+vv2

          ENDDO
      ENDIF
           vv2=(vv2 + vv1*(SS0-SS))/SS0
   
           WPOWER(k)=0.5*rho*( vv2**3)*SS0*Cp

ENDDO 
END   ! subroutine that compute the velocity in front of the wind turbine


!**********************************************************************************



!************************************************************************
! *                         SUBROUTINE  _DATA Power                       *
!*********************************************************************** 
SUBROUTINE WRITE_DATA_power
      USE GeneralData
      USE SolverData
      IMPLICIT NONE
      INTEGER i 
      character*80 file1
      file1='power_data.1'
      OPEN(unit=20, file='Power_Output.dat', status='unknown')
            WRITE(20, *)'   Turbine Number(m)   ', 'Turbine Location-X(m)   ', 'Turbine Location-Y(m)    ', 'POWER(W)'
      DO i=1,N_TURB
            WRITE(20, *)i, x_turb(i),y_turb(i), WPOWER(i)
      ENDDO

      CLOSE(20)
      END
!************************************************************************	
          
       


!************************************************************************
!  FUNCTION : COMPUTE AREA
!--------------------------------------------------------------

SUBROUTINE AAREA(X,Y,Z, area)
  REAL (kind=8):: X, Y , Z, PP, area
  PP=(X+Y+Z)*0.5
  AREA=SQRT(PP*(PP-X)*(PP-Y)*(PP-Z))
RETURN
END
!-------------------------------------------------------------







