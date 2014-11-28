using System;
using ILNumerics;

namespace WakeFarmControl
{
    public class TurbineDrivetrainModel
    {
        private readonly EmbeddedInterpol _eInterpol = new EmbeddedInterpol();

        public void Model(ILArray<double> x, ILArray<double> u, ILMatFile wt, ILMatFile env, double timeStep, out double OmegaOut, out double Ct, out double Cp)
        {
            // Parameters

            var R = (double)wt.GetArray<double>("wt_rotor_radius");
            var I = (double)wt.GetArray<double>("wt_rotor_inertia");

            // Definitons etc.

            var Omega = x.GetValue(0);
            var Ve = x.GetValue(1);
            var Beta = u.GetValue(0);
            var Tg = u.GetValue(1);

            // Algorithm
            double Lambda;
            if (Ve == 0)
            {
                Lambda  = 25;
            }
            else
            {
                Lambda = Omega * R / Ve;
            }

            _eInterpol.interpTable(Beta, Lambda, wt.GetArray<double>("wt_cp_table"), wt.GetArray<double>("wt_cp_beta"), wt.GetArray<double>("wt_cp_tsr"), false, out Cp);
            _eInterpol.interpTable(Beta, Lambda, wt.GetArray<double>("wt_ct_table"), wt.GetArray<double>("wt_ct_beta"), wt.GetArray<double>("wt_ct_tsr"), false, out Ct);

            if (Ct > 1)
            {
                Ct = 1;
            }

            var env_rho = (double)env.GetArray<double>("env_rho");
            var Tr = 0.5 * env_rho * ILMath.pi * Math.Pow(R, 2) * Math.Pow(Ve, 3) * Ct / Omega;
            OmegaOut = Omega + timeStep * (Tr - Tg) / I; //Integration method: Forward Euler
        }
    }
}
