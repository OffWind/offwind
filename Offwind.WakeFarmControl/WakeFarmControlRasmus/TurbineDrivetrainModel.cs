using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public class TurbineDrivetrainModel : MatlabCode
    {
        private readonly EmbeddedInterpol _eInterpol = new EmbeddedInterpol();

        internal void Model(out double OmegaOut, out double Ct, out double Cp, ILArray<double> x, ILArray<double> u, WtMatFileDataStructure wt, EnvMatFileDataStructure env, double timeStep)
        {
            #region "Used variables declaration"
            double R;
            double I;
            double Omega;
            double Ve;
            double Beta;
            double Tg;
            double Lambda;
            double Tr;
            #endregion

            // Parameters

            R = wt.rotor.radius;
            I = wt.rotor.inertia;

            // Definitons etc.

            Omega = x._(1);
            Ve = x._(2);
            Beta = u._(1);
            Tg = u._(2);

            // Algorithm
            if (Ve == 0)
            {
                Lambda  = 25;
            }
            else
            {
                Lambda = Omega * R / Ve;
            }

            _eInterpol.interpTable(out Cp, Beta, Lambda, wt.cp.table, wt.cp.beta, wt.cp.tsr, false);
            _eInterpol.interpTable(out Ct, Beta, Lambda, wt.ct.table, wt.ct.beta, wt.ct.tsr, false);

            if (Ct > 1)
            {
                Ct = 1;
            }

            Tr = 0.5 * env.rho * pi * _p(R, 2) * _p(Ve, 3) * Ct / Omega;

            OmegaOut = Omega + timeStep * (Tr - Tg) / I; //Integration method: Forward Euler
        }
    }
}
