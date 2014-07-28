using System;
using ILNumerics;

namespace WakeFarmControl
{
    public sealed class PowerDistributionControl
    {
        //P_ref is a vector of power refenreces for tehe wind turbine with dimension 1xN
        //v_nac is a vector of wind speed at each wind turbine with dimension 1xN
        //P_demand is a scale of the wind farm power demand.
        //parm is a struct of wind turbine parameters e.g. NREL5MW
        public static void DistributePower(ILArray<double> v_nac, double P_demand, ILArray<double> Power, WindTurbineParameters parm, out ILArray<double> P_ref, out ILArray<double> P_a)
        {
            double rho;
            ILArray<double> R;
            ILArray<double> rated;
            int N;
            ILArray<double> Cp;
            double P_avail;

            rho = parm.rho;                 //air density for each wind turbine(probably the same for all)
            R = parm.radius.C;              //rotor radius for each wind turbine(NREL.r=63m)
            rated = parm.rated.C;           //Rated power for each wind turbine(NREL.Prated=5MW)
            N = parm.N;                     //Number of turbines in windfarm
            Cp = parm.Cp.C;                 // Max cp of the turbines for each wind turbine(NREL.Cp.max=0.45)

            P_a = ILMath.zeros(N, 1);
            P_ref = ILMath.zeros(N, 1);

            // Compute available power at each turbine
            for (var i = 0; i <= N - 1; i++)
            {
                //P_a=A*pi*r*r*Cp*v*v*v
                P_a[i] = Math.Min(rated.GetValue(i), (ILMath.pi / 2) * rho * Math.Pow(R.GetValue(i), 2) * Math.Pow(v_nac.GetValue(i), 3) * Cp.GetValue(i));
            }

            //Compute total available power
            P_avail = (double)ILMath.sum(P_a);

            //Distribute power according to availibility
            for (var i = 0; i <= N - 1; i++)
            {
                if (P_demand < P_avail)
                {
                    P_ref[i] = Math.Max(0, Math.Min(rated.GetValue(i), P_demand * P_a.GetValue(i) / P_avail));
                }
                else
                {
                    P_ref[i] = P_a.GetValue(i);
                }
            }
        }
    }
}
