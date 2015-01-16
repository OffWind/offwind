using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        #region "Original function comments"
        //% v_nac = WAKECALCULATION(Ct,i,wind)
        // RLC, Aalborg
        // The below is based on the .F90 code developed by ?, and will give a
        // better estimate of the actual wake the individual turbines experience. 
        #endregion
        internal static void wakeCalculationsRLC(out ILArray<double> vNac, double dTurb, int nTurb, double kWake, ILArray<double> x, int gridX, int gridY, ILArray<double> yOrder, double dy, ILArray<int> xTurbC, ILArray<int> yTurbC, ILArray<double> Ct, ILArray<double> wField, double[] vHub, WindTurbineParameters parm, SimParm simParm)
        {
            #region "Used variables declaration"
            double[,] Velocity;
            int j;
            #endregion

            // Velocity Computation
            Compute_Vell(out Velocity, yOrder, xTurbC, yTurbC, x, wField, vHub, kWake, gridX, gridY, nTurb, dTurb, Ct, dy);

            // Extracting the individual Nacelle Wind Speeds from the wind velocity matrix.
            //Velocity = Velocity';
            vNac = zeros(nTurb, 1);

            for (j = 1; j <= length(xTurbC); j++)
            {
                vNac._(j, '=', Velocity[yTurbC._(j) - 1, xTurbC._(j) - 1]);
            }
        }
    }
}
