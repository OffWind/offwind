using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        internal static void turbineDrivetrainModel(out double OmegaOut, out double Ct, out double Cp, double x_1_, double x_2_, double u_1_, double u_2_, WtMatFileDataStructure wt, EnvMatFileDataStructure env, double timeStep)
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

            Omega = x_1_;
            Ve = x_2_;
            Beta = u_1_;
            Tg = u_2_;

            // Algorithm
            if (Ve == 0)
            {
                Lambda  = 25;
            }
            else
            {
                Lambda = Omega * R / Ve;
            }

            interpTable(out Cp, Beta, Lambda, wt.cp.table, wt.cp.beta, wt.cp.tsr, false);
            interpTable(out Ct, Beta, Lambda, wt.ct.table, wt.ct.beta, wt.ct.tsr, false);

            if (Ct > 1)
            {
                Ct = 1;
            }

            Tr = 1.0 / 2 * env.rho * pi * _p(R, 2) * _p(Ve, 3) * Ct / Omega;

            OmegaOut = Omega + timeStep * (Tr - Tg) / I; //Integration method: Forward Euler
        }
    }
}
