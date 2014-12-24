using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class Turb_centr_coord : MatlabCode
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(out ILArray<int> output, int nTurb, int iMax, ILArray<double> x, ILArray<double> xTurb, int gridRes)
        {
            ILArray<int> xxcTurb = zeros<int>(1, nTurb);

            for (var i = 1; i <= nTurb; i++)
            {
                for (var ii = 1; ii <= iMax - 1; ii++)
                {
                    if (abs(x._get(ii)) <= abs(xTurb._get(i)) && abs(xTurb._get(i)) < abs(x._get(ii + 1)))
                    {
                        xxcTurb._set(i, '=', ii * sign(xTurb._get(i)));
                        break;
                    }
                }
            }
            output = xxcTurb;
        }
    }
}
