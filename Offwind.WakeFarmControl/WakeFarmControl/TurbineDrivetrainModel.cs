using System;
using ILNumerics;

namespace WakeFarmControl
{
    public class TurbineDrivetrainModel
    {
        private readonly EmbeddedInterpolCpTab _eInterpolCp = new EmbeddedInterpolCpTab();
        private readonly EmbeddedInterpolCtTab _eInterpolCt = new EmbeddedInterpolCtTab();

        public void Model(ILArray<double> x, ILArray<double> u, ILMatFile wt, ILMatFile env, double DT, out double Omega, out double Ct, out double Cp)
        {
            // Parameters

            var R = (double)wt.GetArray<double>("wt_rotor_radius");
            var I = (double)wt.GetArray<double>("wt_rotor_inertia");
            var Rho = (double)env.GetArray<double>("env_rho");

            // Definitons etc.

            Omega = x.GetValue(0);
            var Ve = x.GetValue(1);
            var Beta = u.GetValue(0);
            var Tg = u.GetValue(1);

            // Algorithm

            var Lambda = Omega * R / Ve;
            _eInterpolCp.Interpolate(Beta, Lambda, wt.GetArray<double>("wt_cp_table"), wt.GetArray<double>("wt_cp_beta"), wt.GetArray<double>("wt_cp_tsr"), out Cp);
            _eInterpolCt.Interpolate(Beta, Lambda, wt.GetArray<double>("wt_ct_table"), wt.GetArray<double>("wt_ct_beta"), wt.GetArray<double>("wt_ct_tsr"), out Ct);

            var Tr = 0.5 * Rho * ILMath.pi * Math.Pow(R, 2) * Math.Pow(Ve, 3) * Cp / Omega;
            Omega = Omega + DT * (Tr - Tg) / I; //Integration method: Forward Euler
        }
    }
}
