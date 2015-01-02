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
        internal static void WT_order(out ILArray<double> xOrder, out ILArray<double> yOrder, ILArray<double> xTurb, ILArray<double> yTurb)
        {
            #region "Used variables declaration"
            ILArray<double> sorted;
            ILArray<double> turbineOrder;
            int sortCtr;
            int i;
            int j;
            #endregion

            sorted = sortrows(__[ xTurb.T, yTurb.T ], 1);
            turbineOrder = zeros(length(sorted), 2);
            sortCtr = 0;
            for (i = 1; i <= length(sorted); i++)
            {
                for (j = i + 1; j <= length(sorted); j++)
                {
                    if (sorted._(i, 1) == sorted._(j, 1))
                    {
                        sortCtr = sortCtr + 1;
                    }
                }
                turbineOrder[_(i, ':', i + sortCtr), _(':')] = sortrows(sorted[_(i, ':', i + sortCtr), _(':')], 2);
                sortCtr = 0;
            }
            xOrder = turbineOrder[_(':'), _(1)];
            yOrder = turbineOrder[_(':'), _(2)];
        }
    }
}
