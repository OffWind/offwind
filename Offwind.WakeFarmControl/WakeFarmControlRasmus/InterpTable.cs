using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class EmbeddedInterpol : MatlabCode
    {
        private PersistentVariables _persistentVariables = null;

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


        public void interpTable(out double interpValue, double Beta, double Lambda, ILArray<double> table, ILArray<double> turbineTableBeta, ILArray<double> turbineTableLambda, bool negYes)
        {
            int Bt0;
            int Bt1;
            int La0;
            int La1;
            ILArray<double> tableLookup;
            ILArray<double> lambdaIntervals;
            double betaIntervals;

            //% Setup, loads the table, and stores it.
            //persistent turbineTable tableLoad

            //if (_persistentVariables == null)
            {
                _persistentVariables = new PersistentVariables();
                _persistentVariables.TurbineTable = table.C;
            }

            var turbineTableSize = size(_persistentVariables.TurbineTable);
            var sizeBt = turbineTableSize[0];
            var sizeLa = turbineTableSize[1];

            //% Index Interpolation
            // Finds the beta-point of the interpolation.
            ILArray<int> Bt0_;
            min(out Bt0_, abs(turbineTableBeta - Beta));  // Determines the index of the closest point.
            Bt0 = (int)Bt0_;

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
            ILArray<int> La0_;
            min(out La0_, abs(turbineTableLambda - Lambda)); // Determines the index of the closest point.
            La0 = (int)La0_;

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
            tableLookup = new double[,] {
                { _persistentVariables.TurbineTable._(Bt0, La0), _persistentVariables.TurbineTable._(Bt1, La0) },
                { _persistentVariables.TurbineTable._(Bt0, La1), _persistentVariables.TurbineTable._(Bt1, La1) }
            };

            // Interpolating, using the Lambda values first, then the Betas.
            lambdaIntervals = ILMath.array(
                new double[] {
                    ((tableLookup._(1, 2) - tableLookup._(1, 1))
                        / (turbineTableLambda._(La1) - turbineTableLambda._(La0)))
                        * (Lambda - turbineTableLambda._(La0))
                    + tableLookup._(1, 1),
                    ((tableLookup._(2, 2) - tableLookup._(2, 1))
                        / (turbineTableLambda._(La1) - turbineTableLambda._(La0)))
                        * (Lambda - turbineTableLambda._(La0))
                    + tableLookup._(1, 2)
                }
            );

            // Interpolation, using the Beta values (using the intervales computed above).
            betaIntervals = ((lambdaIntervals._(2) - lambdaIntervals._(1)) / (turbineTableBeta._(Bt1) - turbineTableBeta._(Bt0)))
                                 * (Beta - turbineTableBeta._(Bt0))
                             + lambdaIntervals._(1);

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
