using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        //% v_nac = WAKECALCULATION(Ct,i,wind)
        // RLC, Aalborg
        // The below is based on the .F90 code developed by ?, and will give a
        // better estimate of the actual wake the individual turbines experience. 
        internal static void wakeCalculationsRLC(out ILArray<double> vNac, ILArray<double> Ct, ILArray<double> wField, ILArray<double> vHub, WindTurbineParameters parm, SimParm simParm)
        {
            #region "Used variables declaration"
            ILArray<double> data;
            double dTurb;
            int nTurb;
            double rotA;
            double kWake;
            int gridRes;
            int endSize;
            ILArray<double> x;
            ILArray<double> y;
            int gridX;
            int gridY;
            ILArray<double> xCoor;
            ILArray<double> yCoor;
            ILArray<double> xTurb;
            ILArray<double> yTurb;
            ILArray<double> xOrder;
            ILArray<double> yOrder;
            int ppp;
            ILArray<double> xGrid;
            ILArray<double> yGrid;
            double dy;
            ILArray<int> xTurbC;
            ILArray<int> yTurbC;
            ILArray<double> Velocity;
            int j;
            #endregion

            data = parm.wf.C;
            dTurb = 2 * parm.radius._(1);
            nTurb = parm.N;
            rotA  = parm.rotA;
            kWake = parm.kWake;
            gridRes = simParm.gridRes; // Grid Resolution, the lower the number, the higher the amount of points computed.
            endSize = simParm.grid;
    
            //if (Ct < 0) // !ILMath.isreal(Ct) | 
            {
                //disp('Ct is negative or complex');
            }

            x = _a(1, gridRes, endSize);// x-grid.
            y = _a(1, gridRes, endSize);// y-grid.
            gridX = length(x); // Number of grid points.
            gridY = length(y); // Number of grid points.
        
            xCoor   = data._(':', 1); // Coordiante of turbine, x-position
            yCoor   = data._(':', 2); // Coordinate of turbine, y-position

            ROTATE_corrd(out xTurb, out yTurb, xCoor, yCoor, rotA); // Rotated (and scaled) coordinates
            WT_order(out xOrder, out yOrder, xTurb, yTurb); // Ordered turbines. 

            ppp = 2; // This parameter is also a bit weird.. But it changes the grid.
            double _;
            DOMAIN_pt(out xGrid, out _, gridX, dTurb, xOrder, ppp); // 
            ppp = 5;
            DOMAIN_pt(out yGrid, out dy, gridY, dTurb, yOrder, ppp);

            Turb_centr_coord(out xTurbC, nTurb, gridX, xGrid, xOrder, gridRes); // Determines the grid point closest to the turbine.
            Turb_centr_coord(out yTurbC, nTurb, gridY, yGrid, yOrder, gridRes); // Determines the grid point closest to the turbine. 

            // Velocity Computation
            Compute_Vell(out Velocity, yOrder, xTurbC, yTurbC, x, wField, vHub, kWake, gridX, gridY, nTurb, dTurb, Ct, dy);

            // Extracting the individual Nacelle Wind Speeds from the wind velocity matrix.
            //Velocity = Velocity';
            vNac = zeros(nTurb, 1);

            for (j = 1; j <= length(xTurbC); j++)
            {
                vNac._(j, '=', Velocity._(yTurbC._(j), xTurbC._(j)));
            }
        }
    }
}
