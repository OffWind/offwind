using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class EmbeddedInterpol
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


        public void interpTable(double Beta, double Lambda, ILArray<double> table, ILArray<double> turbineTableBeta, ILArray<double> turbineTableLambda, bool negYes, out double interpValue)
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

            var turbineTableSize = _persistentVariables.TurbineTable.Size;
            var sizeBt = turbineTableSize[0];
            var sizeLa = turbineTableSize[1];

            //% Index Interpolation
            // Finds the beta-point of the interpolation.
            ILArray<int> Bt0_ = ILMath.empty<int>();
            ILMath.min(ILMath.abs(turbineTableBeta - Beta), Bt0_);  // Determines the index of the closest point.
            Bt0 = Bt0_.GetValue(0);

            if (Beta > turbineTableBeta.GetValue(Bt0))
            {
                if (Bt0 == sizeBt - 1) //length(turbineTableBeta)
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
                if (Bt0 == 0)
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
            ILArray<int> La0_ = ILMath.empty<int>();
            ILMath.min(ILMath.abs(turbineTableLambda - Lambda), La0_); // Determines the index of the closest point.
            La0 = La0_.GetValue(0);

            if (Lambda > turbineTableLambda.GetValue(La0))
                if (La0 == sizeLa - 1) //length(turbineTableLambda)
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
                if (La0 == 0)
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
                { _persistentVariables.TurbineTable.GetValue(Bt0, La0), _persistentVariables.TurbineTable.GetValue(Bt1, La0) },
                { _persistentVariables.TurbineTable.GetValue(Bt0, La1), _persistentVariables.TurbineTable.GetValue(Bt1, La1) }
            };

            // Interpolating, using the Lambda values first, then the Betas.
            lambdaIntervals = ILMath.array(
                new double[] {
                    ((tableLookup.GetValue(0, 1) - tableLookup.GetValue(0, 0))
                        / (turbineTableLambda.GetValue(La1) - turbineTableLambda.GetValue(La0)))
                        * (Lambda - turbineTableLambda.GetValue(La0))
                    + tableLookup.GetValue(0, 0),
                    ((tableLookup.GetValue(1, 1) - tableLookup.GetValue(1, 0))
                        / (turbineTableLambda.GetValue(La1) - turbineTableLambda.GetValue(La0)))
                        * (Lambda - turbineTableLambda.GetValue(La0))
                    + tableLookup.GetValue(0, 1)
                }
            );

            // Interpolation, using the Beta values (using the intervales computed above).
            betaIntervals = ((lambdaIntervals.GetValue(1) - lambdaIntervals.GetValue(0)) / (turbineTableBeta.GetValue(Bt1) - turbineTableBeta.GetValue(Bt0)))
                                 * (Beta - turbineTableBeta.GetValue(Bt0))
                             + lambdaIntervals.GetValue(0);

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
