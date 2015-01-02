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
        internal static void DOMAIN_pt(out ILArray<double> output, out double ddx, int iMax, double dTurb, ILArray<double> xOrder, int pppPoint)
        {
            #region "Used variables declaration"
            ILArray<double> x;
            double xMax;
            double xMin;
            int i;
            #endregion

            x = zeros(1, length(xOrder)); // Initialization

            xMax = max_(xOrder) + dTurb * pppPoint;
            xMin = min_(xOrder) - 2 * dTurb;

            x._(1, '=', xMin);
            ddx = (xMax - xMin) / (iMax - 1);

            for (i = 1; i <= iMax - 1; i++)
            {
                x._(i + 1, '=', x._(i) + ddx);
            }

            output = x;
        }
    }
}
