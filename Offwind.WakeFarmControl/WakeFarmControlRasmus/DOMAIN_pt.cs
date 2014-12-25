using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class DOMAIN_pt : MatlabCode
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(out ILArray<double> output, out double ddx, int iMax, double dTurb, ILArray<double> xOrder, int pppPoint)
        {
            #region "Used variables declaration"
            ILArray<double> x;
            double xMax;
            double xMin;
            int i;
            #endregion

            x = zeros(1, length(xOrder)); // Initialization

            xMax = max(xOrder) + dTurb * pppPoint;
            xMin = ((double)(min(xOrder))) - 2 * dTurb;

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
