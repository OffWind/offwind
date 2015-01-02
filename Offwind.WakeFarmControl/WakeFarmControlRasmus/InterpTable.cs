using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        #region "Original function comments"
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        // DESCRIPTION:
        // RLC - 8/9/2014, interpolation, for getting an accurate CP/CT value. The
        // reason for a standalone script, is that the built in MATLAB script
        // (interp) includes a lot of redundancy checks that are not necessary, and
        // not feasible for embedded solutions. 
        // Based on the NREL5MW Turbine.
        // Uses Linear Polynomial Extrapolation, to get a value for CP, given a Beta
        // (Revolutional speed) and Lambda (Tip-Speed-Ratio) of the Turbine. 
        // The interpolation is computed as:
        // y = y0 + (y1 - y0)*(x-x0)/(x1-x0)
        //
        // Beta is the revolutional entry.
        // Lambda is the TSR entry ratio. 
        // turbineTable defines the table to lookup in. 
        // negYes defines wether the CP value should be allowed to be negative.
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        #endregion
        internal static void interpTable(out double interpValue, double Beta, double Lambda, ILArray<double> table, ILArray<double> turbineTableBeta, ILArray<double> turbineTableLambda, bool negYes)
        {
            #region "Used variables declaration"
            ILArray<double> turbineTable;
            int sizeBt;
            int sizeLa;
            int Bt0;
            int Bt1;
            int La0;
            int La1;
            ILArray<double> tableLookup;
            ILArray<double> lambdaIntervals;
            double betaIntervals;
            #endregion

            //% Setup, loads the table, and stores it.
            //persistent turbineTable tableLoad

            //if isempty(tableLoad)
                turbineTable        = table.C;
            //    tableLoad           = 1;
            //end

            size(out sizeBt, out sizeLa, turbineTable);

            //% Index Interpolation
            // Finds the beta-point of the interpolation.
            min(out Bt0, abs(turbineTableBeta - Beta));  // Determines the index of the closest point.

            if (Beta > turbineTableBeta._(Bt0))
            {
                if (Bt0 == sizeBt) //length(turbineTableBeta)
                {
                    Bt1 = Bt0;
                    Bt0 = Bt0 - 1;
                }
                else
                {
                    Bt1 = Bt0 + 1;
                }
            }
            else
            {
                if (Bt0 == 1)
                {
                    Bt1 = Bt0 + 1;
                }
                else
                {
                    Bt1 = Bt0;
                    Bt0 = Bt1 - 1;
                }
            }

            // Finds the Lambda-point of the interpolation.
            min(out La0, abs(turbineTableLambda - Lambda)); // Determines the index of the closest point.

            if (Lambda > turbineTableLambda._(La0))
                if (La0 == sizeLa) //length(turbineTableLambda)
                {
                    La1 = La0;
                    La0 = La1 - 1;
                }
                else
                {
                    La1 = La0 + 1;
                }
            else
            {
                if (La0 == 1)
                {
                    La1 = La0 + 1;
                }
                else
                {
                    La1 = La0;
                    La0 = La1 - 1;
                }
            }

            //% Table Interpolation
            // Table lookup using indexes obtained previously:
            tableLookup     = __[ turbineTable._(Bt0, La0), turbineTable._(Bt0, La1), ';',
                                  turbineTable._(Bt1, La0), turbineTable._(Bt1, La1) ];

            // Interpolating, using the Lambda values first, then the Betas.
            lambdaIntervals = __[ ( (tableLookup._(1, 2) - tableLookup._(1, 1)) / (turbineTableLambda._(La1) - turbineTableLambda._(La0)) ) * (Lambda - turbineTableLambda._(La0)) + tableLookup._(1, 1), ';',
                                  ( (tableLookup._(2, 2) - tableLookup._(2, 1)) / (turbineTableLambda._(La1) - turbineTableLambda._(La0)) ) * (Lambda - turbineTableLambda._(La0)) + tableLookup._(1, 2) ];

            // Interpolation, using the Beta values (using the intervales computed above).
            betaIntervals   = ( (lambdaIntervals._(2) - lambdaIntervals._(1)) / (turbineTableBeta._(Bt1) - turbineTableBeta._(Bt0)) ) * (Beta - turbineTableBeta._(Bt0)) + lambdaIntervals._(1);

            //% Negativity Handling
            // If the negYes value is set as true in the system, the functino will only
            // give an absolute value of Ct and Cp - have to be modified so it does this
            // correctly.

            if (negYes)
            {
                interpValue = 0;
            }
            else
            {
                interpValue = betaIntervals;
            }
        }
    }
}
