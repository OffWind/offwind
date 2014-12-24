using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class WT_order : MatlabCode
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(out ILArray<double> xOrder, out ILArray<double> yOrder, ILArray<double> xTurb, ILArray<double> yTurb)
        {
            ILArray<double> sorted = sortrows(_[ xTurb.T, yTurb.T ], 1);
            ILArray<double> turbineOrder = zeros(length(sorted), 2);
            int sortCtr = 0;
            for (var i = 1; i <= length(sorted); i++)
            {
                for (var j = i + 1; j <= length(sorted); j++)
                {
                    if (sorted._get(i, 1) == sorted._get(j, 1))
                    {
                        sortCtr = sortCtr + 1;
                    }
                }
                turbineOrder._set(i, i + sortCtr, ':', '=', sortrows(sorted._get(i, i + sortCtr, ':'), 2));

                sortCtr = 0;
            }
            xOrder = turbineOrder._get(':', 1);
            yOrder = turbineOrder._get(':', 2);
        }
    }
}
