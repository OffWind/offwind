using ILNumerics;

namespace WakeFarmControl
{
    public sealed class DOMAIN_pt
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(int iMax, double dTurb, ILArray<double> xOrder, int pppPoint, out ILArray<double> output, out double ddx)
        {
            ILArray<double> x = ILMath.zeros(1, xOrder.length()); // Initialization

            double xMax = ((double)(ILMath.max(xOrder))) + dTurb * pppPoint;
            double xMin = ((double)(ILMath.min(xOrder))) - 2 * dTurb;

            x.SetValue(xMin, 0);
            ddx = (xMax - xMin) / (iMax - 1);

            for (var i = 1; i <= iMax - 1; i++)
            {
                x.SetValue(x.GetValue(i - 1) + ddx, i);
            }

            output = x;
        }
    }
}
