using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class PowerDistributionControl : MatlabCode
    {
        //P_ref is a vector of power refenreces for tehe wind turbine with dimension 1xN
        //v_nac is a vector of wind speed at each wind turbine with dimension 1xN
        //P_demand is a scale of the wind farm power demand.
        //parm is a struct of wind turbine parameters e.g. NREL5MW
        public static void DistributePower(out ILArray<double> P_ref, out ILArray<double> P_a, ILArray<double> v_nac, double P_demand, WindTurbineParameters parm)
        {
            double rho;
            ILArray<double> R;
            ILArray<double> rated;
            int N;
            ILArray<double> Cp;

            rho = parm.rho;                 //air density for each wind turbine(probably the same for all)
            R = parm.radius.C;              //rotor radius for each wind turbine(NREL.r=63m)
            rated = parm.rated.C;           //Rated power for each wind turbine(NREL.Prated=5MW)
            Cp = parm.Cp.C;                 // Max cp of the turbines for each wind turbine(NREL.Cp.max=0.45)

            P_a = zeros(parm.N, 1);
            P_ref = zeros(parm.N, 1);

            // Compute available power at each turbine
            for (var i = 1; i <= parm.N; i++)
            {
                P_a._set(i, '=', min(_[rated._get(i), (pi / 2) * rho * _p(R._get(i), 2) * _p(v_nac._get(i), 3) * Cp._get(i)]));
            }

            var sum_P_a_ = (double)sum(P_a);

            //Distribute power according to availibility
            for (var i = 1; i <= parm.N; i++)
            {
                if (P_demand < sum_P_a_)
                {
                    P_ref._set(i, '=', max(_[0, min(_[rated._get(i), P_demand * P_a._get(i) / sum_P_a_])]));
                }
                else
                {
                    P_ref._set(i, '=', P_a._get(i));
                }
            }
        }
    }
}
