using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        internal static void turbinesCalculations(out double dTurb, out int nTurb, out double kWake, out ILArray<double> x, out int gridX, out int gridY, out ILArray<double> yOrder, out double dy, out ILArray<int> xTurbC, out ILArray<int> yTurbC, WindTurbineParameters parm, SimParm simParm)
        {
            #region "Used variables declaration"
            ILArray<double> data;
            double rotA;
            int gridRes;
            int endSize;
            ILArray<double> y;
            ILArray<double> xCoor;
            ILArray<double> yCoor;
            ILArray<double> xTurb;
            ILArray<double> yTurb;
            ILArray<double> xOrder;
            int ppp;
            ILArray<double> xGrid;
            ILArray<double> yGrid;
            #endregion

            data = parm.wf.C;
            dTurb = 2 * parm.radius._(1);
            nTurb = parm.N;
            rotA = parm.rotA;
            kWake = parm.kWake;
            gridRes = simParm.gridRes; // Grid Resolution, the lower the number, the higher the amount of points computed.
            endSize = simParm.grid;

            //if (Ct < 0) // !ILMath.isreal(Ct) | 
            {
                //disp('Ct is negative or complex');
            }

            x = _c(1.0, gridRes, endSize);// x-grid.
            y = _c(1.0, gridRes, endSize);// y-grid.
            gridX = length(x); // Number of grid points.
            gridY = length(y); // Number of grid points.

            xCoor = data[_(':'), _(1)]; // Coordiante of turbine, x-position
            yCoor = data[_(':'), _(2)]; // Coordinate of turbine, y-position

            ROTATE_corrd(out xTurb, out yTurb, xCoor, yCoor, rotA); // Rotated (and scaled) coordinates
            WT_order(out xOrder, out yOrder, xTurb, yTurb); // Ordered turbines. 

            ppp = 2; // This parameter is also a bit weird.. But it changes the grid.
            DOMAIN_pt(out xGrid, out _double, gridX, dTurb, xOrder, ppp); // 
            ppp = 5;
            DOMAIN_pt(out yGrid, out dy, gridY, dTurb, yOrder, ppp);

            Turb_centr_coord(out xTurbC, nTurb, gridX, xGrid, xOrder, gridRes); // Determines the grid point closest to the turbine.
            Turb_centr_coord(out yTurbC, nTurb, gridY, yGrid, yOrder, gridRes); // Determines the grid point closest to the turbine. 
        }
    }
}
