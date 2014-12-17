using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class wakeCalculationsRLC
    {
        //% v_nac = WAKECALCULATION(Ct,i,wind)
        // RLC, Aalborg
        // The below is based on the .F90 code developed by ?, and will give a
        // better estimate of the actual wake the individual turbines experience. 
        public static void Calculate(ILArray<double> Ct, ILArray<double> wField, ILArray<double> vHub, WindTurbineParameters parm, SimParm simParm, out ILArray<double> vNac)
        {
            ILArray<double> data  = parm.wf.C;
            double dTurb = 2 * parm.radius.GetValue(0);
            int nTurb = parm.N;
            double rotA  = parm.rotA;
            double kWake = parm.kWake;
            int gridRes = simParm.gridRes; // Grid Resolution, the lower the number, the higher the amount of points computed.
            int endSize = simParm.grid;
    
            //if (Ct < 0) // !ILMath.isreal(Ct) | 
            {
                //disp('Ct is negative or complex');
            }
    
            ILArray<double> x = ILMath.counter(1, gridRes, (endSize - 1) / gridRes + 1);// x-grid.
            ILArray<double> y = ILMath.counter(1, gridRes, (endSize - 1) / gridRes + 1);// y-grid.
            int gridX   = x.length(); // Number of grid points.
            int gridY   = y.length(); // Number of grid points.
        
            ILArray<double> xCoor   = data[ILMath.full, 0]; // Coordiante of turbine, x-position
            ILArray<double> yCoor   = data[ILMath.full, 1]; // Coordinate of turbine, y-position

            ILArray<double> xTurb;
            ILArray<double> yTurb;
            ROTATE_corrd.Calculate(xCoor, yCoor, rotA, out xTurb, out yTurb); // Rotated (and scaled) coordinates
            ILArray<double> xOrder;
            ILArray<double> yOrder;
            WT_order.Calculate(xTurb, yTurb, out xOrder, out yOrder); // Ordered turbines. 

            int ppp = 2; // This parameter is also a bit weird.. But it changes the grid.
            ILArray<double> xGrid;
            double _;
            DOMAIN_pt.Calculate(gridX, dTurb, xOrder, ppp, out xGrid, out _); // 
            ppp = 5;
            ILArray<double> yGrid;
            double dy;
            DOMAIN_pt.Calculate(gridY, dTurb, yOrder, ppp, out yGrid, out dy);

            ILArray<int> xTurbC;
            Turb_centr_coord.Calculate(nTurb, gridX, xGrid, xOrder, gridRes, out xTurbC); // Determines the grid point closest to the turbine.
            ILArray<int> yTurbC;
            Turb_centr_coord.Calculate(nTurb, gridY, yGrid, yOrder, gridRes, out yTurbC); // Determines the grid point closest to the turbine. 

            // Velocity Computation
            ILArray<double> Velocity;
            Compute_Vell.Calculate(yOrder, xTurbC, yTurbC, x, wField, vHub, kWake, gridX, gridY, nTurb, dTurb, Ct, dy, out Velocity);

            // Extracting the individual Nacelle Wind Speeds from the wind velocity matrix.
            //Velocity = Velocity';
            vNac = ILMath.zeros(nTurb, 1);
    
            for (var j = 1; j <= xTurbC.length(); j++)
            {
                vNac.SetValue(Velocity.GetValue(yTurbC.GetValue(j - 1), xTurbC.GetValue(j - 1)), j - 1);
            }
        }
    }
}
