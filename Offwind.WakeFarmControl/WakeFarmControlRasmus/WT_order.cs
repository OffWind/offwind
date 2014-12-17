using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class WT_order
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(ILArray<double> xTurb, ILArray<double> yTurb, out ILArray<double> xOrder, out ILArray<double> yOrder)
        {
            ILArray<double> sorted = ((ILArray<double>)(xTurb.T.Concat(yTurb.T, 1))).sortrows(0);
            ILArray<double> turbineOrder = ILMath.zeros(sorted.length(), 2);
            int sortCtr = 0;
            for (var i = 1; i <= sorted.length(); i++)
            {
                for (var j = i + 1; j <= sorted.length(); j++)
                {
                    if (sorted.GetValue(i - 1, 0) == sorted.GetValue(j - 1, 0))
                    {
                        sortCtr = sortCtr + 1;
                    }
                }
                turbineOrder.SetRows(((ILArray<double>)(sorted[ILMath.r(i - 1, i + sortCtr - 1), ILMath.full])).sortrows(1), i - 1, i + sortCtr - 1);
                sortCtr = 0;
            }
            xOrder = turbineOrder[ILMath.full, 0];
            yOrder = turbineOrder[ILMath.full, 1];
        }
    }
}
