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
            ILArray<double> x = zeros(1, length(xOrder)); // Initialization

            double xMax = max(xOrder) + dTurb * pppPoint;
            double xMin = ((double)(min(xOrder))) - 2 * dTurb;

            x._set(1, xMin);
            ddx = (xMax - xMin) / (iMax - 1);

            for (var i = 1; i <= iMax - 1; i++)
            {
                x._set(i + 1, x._get(i) + ddx);
            }

            output = x;
        }
    }
}
