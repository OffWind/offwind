using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class wakeCalculationsRLC : MatlabCode
    {
        //% v_nac = WAKECALCULATION(Ct,i,wind)
        // RLC, Aalborg
        // The below is based on the .F90 code developed by ?, and will give a
        // better estimate of the actual wake the individual turbines experience. 
        public static void Calculate(out ILArray<double> vNac, ILArray<double> Ct, ILArray<double> wField, ILArray<double> vHub, WindTurbineParameters parm, SimParm simParm)
        {
            ILArray<double> data  = parm.wf.C;
            double dTurb = 2 * parm.radius._get(1);
            int nTurb = parm.N;
            double rotA  = parm.rotA;
            double kWake = parm.kWake;
            int gridRes = simParm.gridRes; // Grid Resolution, the lower the number, the higher the amount of points computed.
            int endSize = simParm.grid;
    
            //if (Ct < 0) // !ILMath.isreal(Ct) | 
            {
                //disp('Ct is negative or complex');
            }

            ILArray<double> x = _a(1, gridRes, endSize);// x-grid.
            ILArray<double> y = _a(1, gridRes, endSize);// y-grid.
            int gridX = length(x); // Number of grid points.
            int gridY = length(y); // Number of grid points.
        
            ILArray<double> xCoor   = data._get(':', 1); // Coordiante of turbine, x-position
            ILArray<double> yCoor   = data._get(':', 2); // Coordinate of turbine, y-position

            ILArray<double> xTurb;
            ILArray<double> yTurb;
            ROTATE_corrd.Calculate(out xTurb, out yTurb, xCoor, yCoor, rotA); // Rotated (and scaled) coordinates
            ILArray<double> xOrder;
            ILArray<double> yOrder;
            WT_order.Calculate(out xOrder, out yOrder, xTurb, yTurb); // Ordered turbines. 

            int ppp = 2; // This parameter is also a bit weird.. But it changes the grid.
            ILArray<double> xGrid;
            double _;
            DOMAIN_pt.Calculate(out xGrid, out _, gridX, dTurb, xOrder, ppp); // 
            ppp = 5;
            ILArray<double> yGrid;
            double dy;
            DOMAIN_pt.Calculate(out yGrid, out dy, gridY, dTurb, yOrder, ppp);

            ILArray<int> xTurbC;
            Turb_centr_coord.Calculate(out xTurbC, nTurb, gridX, xGrid, xOrder, gridRes); // Determines the grid point closest to the turbine.
            ILArray<int> yTurbC;
            Turb_centr_coord.Calculate(out yTurbC, nTurb, gridY, yGrid, yOrder, gridRes); // Determines the grid point closest to the turbine. 

            // Velocity Computation
            ILArray<double> Velocity;
            Compute_Vell.Calculate(out Velocity, yOrder, xTurbC, yTurbC, x, wField, vHub, kWake, gridX, gridY, nTurb, dTurb, Ct, dy);

            // Extracting the individual Nacelle Wind Speeds from the wind velocity matrix.
            //Velocity = Velocity';
            vNac = zeros(nTurb, 1);

            for (var j = 1; j <= length(xTurbC); j++)
            {
                vNac._set(j, Velocity._get(yTurbC._get(j), xTurbC._get(j)));
            }
        }
    }
}
