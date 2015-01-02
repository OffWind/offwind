using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        #region "Original function comments"
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        #endregion
        internal static void Turb_centr_coord(out ILArray<int> output, int nTurb, int iMax, ILArray<double> x, ILArray<double> xTurb, int gridRes)
        {
            #region "Used variables declaration"
            ILArray<int> xxcTurb;
            int i;
            int ii;
            #endregion

            xxcTurb = zeros_(1, nTurb);

            for (i = 1; i <= nTurb; i++)
            {
                for (ii = 1; ii <= iMax - 1; ii++)
                {
                    if (abs(x._(ii)) <= abs(xTurb._(i)) && abs(xTurb._(i)) < abs(x._(ii + 1)))
                    {
                        xxcTurb._(i, '=', ii * sign(xTurb._(i)));
                        break;
                    }
                }
            }
            output = xxcTurb;
        }
    }
}
