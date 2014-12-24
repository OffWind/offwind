using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public class TurbineDrivetrainModel : MatlabCode
    {
        private readonly EmbeddedInterpol _eInterpol = new EmbeddedInterpol();

        internal void Model(out double OmegaOut, out double Ct, out double Cp, ILArray<double> x, ILArray<double> u, WtMatFileDataStructure wt, EnvMatFileDataStructure env, double timeStep)
        {
            // Parameters

            var R = wt.rotor.radius;
            var I = wt.rotor.inertia;

            // Definitons etc.

            var Omega = x._get(1);
            var Ve = x._get(2);
            var Beta = u._get(1);
            var Tg = u._get(2);

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

            _eInterpol.interpTable(out Cp, Beta, Lambda, wt.cp.table, wt.cp.beta, wt.cp.tsr, false);
            _eInterpol.interpTable(out Ct, Beta, Lambda, wt.ct.table, wt.ct.beta, wt.ct.tsr, false);

            if (Ct > 1)
            {
                Ct = 1;
            }

            var Tr = 0.5 * env.rho * pi * _p(R, 2) * _p(Ve, 3) * Ct / Omega;
            OmegaOut = Omega + timeStep * (Tr - Tg) / I; //Integration method: Forward Euler
        }
    }
}
