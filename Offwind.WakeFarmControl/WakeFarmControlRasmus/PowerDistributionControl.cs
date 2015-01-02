using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        #region "Original function comments"
        //P_ref is a vector of power refenreces for tehe wind turbine with dimension 1xN
        //v_nac is a vector of wind speed at each wind turbine with dimension 1xN
        //P_demand is a scale of the wind farm power demand.
        //parm is a struct of wind turbine parameters e.g. NREL5MW
        #endregion
        internal static void powerDistributionControl(out ILArray<double> P_ref, out ILArray<double> P_a, ILArray<double> v_nac, double P_demand, WindTurbineParameters parm)
        {
            #region "Used variables declaration"
            double rho;
            ILArray<double> R;
            ILArray<double> rated;
            ILArray<double> Cp;
            int i;
            #endregion

            rho = parm.rho;                 //air density for each wind turbine(probably the same for all)
            R = parm.radius.C;              //rotor radius for each wind turbine(NREL.r=63m)
            rated = parm.rated.C;           //Rated power for each wind turbine(NREL.Prated=5MW)
            Cp = parm.Cp.C;                 // Max cp of the turbines for each wind turbine(NREL.Cp.max=0.45)

            P_a = zeros(parm.N, 1);
            P_ref = zeros(parm.N, 1);

            // Compute available power at each turbine
            for (i = 1; i <= parm.N; i++)
            {
                P_a._(i, '=', min_(__[ rated._(i), (pi / 2) * rho * _p(R._(i), 2) * _p(v_nac._(i), 3) * Cp._(i) ]));
            }

            var sum_P_a_ = sum_(P_a);

            //Distribute power according to availibility
            for (i = 1; i <= parm.N; i++)
            {
                if (P_demand < sum_P_a_)
                {
                    P_ref._(i, '=', max_(__[ 0, min_(__[ rated._(i), P_demand * P_a._(i) / sum_P_a_ ]) ]));
                }
                else
                {
                    P_ref._(i, '=', P_a._(i));
                }
            }
        }
    }
}
