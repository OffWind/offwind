% Wake Simulation
% (C) Rasmus Christensen
% Control and Automation, Aalborg University 2014

% Compute the velocity in front of each wind-turbine, with respect to the wind input.

function [vel_output] = Compute_Vell(yTurb ,xTurbC ,yTurbC ,x ,wField ,Uhub ,kWake ,iMax ,jMax ,nTurb ,dTurb ,Ct, dy)
    vell_i = wField;
    
    shadow = zeros(1,nTurb);

	r0 = 0.5*dTurb;
	nk = 2*ceil(dTurb/dy);

    for k = 1:nTurb
		J = 0;
		SS = 0;
		SS0 = pi*r0*r0;

        for i = 1:k-1
			RR_i = r0 + kWake*(x(xTurbC(k)) - x(xTurbC(i)));
			Dij = abs(yTurb(i)-yTurb(k));

			if (RR_i >= (r0 + Dij)) || (Dij <= dy)
				SS = SS+((r0*r0)/(RR_i*RR_i));
			else
				if RR_i >= (r0 + Dij) && Dij > dy
					J 			= J+1;
					Alpha_i 	= acos((RR_i*RR_i)+(Dij*Dij)-(r0*r0)/(2*RR_i*Dij));
					Alpha_k 	= acos(((r0*r0)+(Dij*Dij)-(RR_i*RR_i))/(2*r0*Dij));
					Area 		= AArea(RR_i, r0, Dij);
					shadow(J) 	= (Alpha_i*(RR_i*RR_i) + Alpha_k*(r0*r0))- 2*Area;
					SS 			= SS + ((shadow(J))/SS0)*((r0*r0)/(RR_i*RR_i));
				end
			end
        end

        for ii = xTurbC(k):iMax
			rrt 	= r0 + kWake*(x(ii)-x(xTurbC(k)));
			nj 		= ceil(rrt/dy);

			jjMin   = floor(max(1, yTurbC(k)-nj));
			jjMax   = ceil(min([jMax, yTurbC(k)+nj]));

            for j = jjMin:jjMax
                if ((-vell_i(ii,j)+Uhub(k)) > 0) && (ii > xTurbC(k)+nk)
					vell_i(ii,j) = min([vell_i(ii-1,j), Uhub(k) + Uhub(k)*(sqrt(1-Ct(k))-1)*((r0*r0)/(rrt*rrt))*(1-(1-sqrt(1-Ct(k)))*SS)]);
                    vell_i(ii,j) = max([0, vell_i(ii,j)]);
				else
					vell_i(ii,j) = (Uhub(k) + Uhub(k)*(sqrt(1-Ct(k))-1)*(r0/rrt)*(r0/rrt))*(1-(1-sqrt(1-Ct(k)))*SS);
                    vell_i(ii,j) = max([0, vell_i(ii,j)]);
                end
            end
        end
    end
    
	vel_output = vell_i;

end

function [area_out] = AArea(x, y, z) % Internal function to compute the shadowed area.
	PP = (x+y+z)*0.5;
	area_out = sqrt(PP*(PP-x)*(PP-y)*(PP-z));
end