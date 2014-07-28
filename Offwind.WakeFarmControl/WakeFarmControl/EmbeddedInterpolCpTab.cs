using ILNumerics;

namespace WakeFarmControl
{
    public sealed class EmbeddedInterpolCpTab
    {
        private PersistentVariables _persistentCp = null;

        //Finalval is the result of the look up of Lambda and Beta in the CP map.
        //The function uses a bilinear interpolation method, and has been developed
        //to replace interpn in an embedded matlab environment
        public void Interpolate(double Beta, double Lambda, ILArray<double> table2, ILArray<double> Betavec2, ILArray<double> Lambdavec2, out double Finalval)
        {
            ILArray<int> Bt;
            int B1;
            int B2;
            ILArray<int> Lt;
            int L1;
            int L2;
            ILArray<double> Yvals;
            ILArray<double> Yintervals;

            //Setting up persistent variables

            //Function initialization

            //The first time the function is run, it stores supplied map as a persistent
            //variable.
            if (_persistentCp == null) // Is only run once
            {
                _persistentCp = new PersistentVariables();
                _persistentCp.Table = table2.C;
                _persistentCp.Betavec = Betavec2.C;
                _persistentCp.Lambdavec = Lambdavec2.C;
            }

            //Step 1, finding two adjecent indexes of the BetaVec, which contain the
            //supplied beta value

            Bt = ILMath.empty<int>();
            ILMath.min(ILMath.abs(_persistentCp.Betavec - Beta), Bt);      //Finding index 1
            B1 = Bt.GetValue(0);                               //Necessary specification in embedded
            //matlab

            if (Beta > _persistentCp.Betavec.GetValue(B1))                     //Finding index 2
            {
                if (B1 == (_persistentCp.Betavec.Length - 1))              //testing if endpoint-extrapolation
                {
                    B2 = B1;                          //should be used
                    B1 = B1 - 1;
                }
                else
                {
                    B2 = B1 + 1;
                }
            }
            else
            {
                if (B1 == 0)
                {
                    B1 = 1;
                    B2 = 0;
                }
                else
                {
                    B2 = B1 - 1;
                }
            }

            //Step 2, finding two adjecent indexes of the LambdaVec, which contain the
            //supplied Lambda value
            Lt = ILMath.empty<int>();
            ILMath.min(ILMath.abs(_persistentCp.Lambdavec - Lambda), Lt);
            L1 = Lt.GetValue(0);
            if (Lambda > _persistentCp.Lambdavec.GetValue(L1)) //Need to work out of indexes
            {
                if (L1 == (_persistentCp.Lambdavec.Length - 1))
                {
                    L2 = L1;
                    L1 = L1 - 1;
                }
                else
                {
                    L2 = L1 + 1;
                }
            }
            else
            {
                if (L1 == 0)
                {
                    L1 = 1;
                    L2 = 0;
                }
                else
                {
                    L2 = L1 - 1;
                }
            }

            //Step 3
            //Finding the four indexed values by means of the indexes
            Yvals = new double[,] { { _persistentCp.Table.GetValue(B1, L1), _persistentCp.Table.GetValue(B2, L1) },
                                    { _persistentCp.Table.GetValue(B1, L2), _persistentCp.Table.GetValue(B2, L2) } };

            //Step 4
            //Making two sets of linear interpolations by using the different lambda values
            Yintervals = ILMath.array(new double[] {
                                            ((Yvals.GetValue(0, 1) - Yvals.GetValue(0, 0))
                                                    / (_persistentCp.Lambdavec.GetValue(L2) - _persistentCp.Lambdavec.GetValue(L1))
                                                    * (Lambda - _persistentCp.Lambdavec.GetValue(L1))
                                                + Yvals.GetValue(0, 0)),
                                            ((Yvals.GetValue(1, 1) - Yvals.GetValue(1, 0))
                                                        / (_persistentCp.Lambdavec.GetValue(L2) - _persistentCp.Lambdavec.GetValue(L1))
                                                        * (Lambda - _persistentCp.Lambdavec.GetValue(L1))
                                                    + Yvals.GetValue(1, 0))
                                        },
                                        2, 1);

            //Step 5
            //Making the final linear interpolation on the results obtained in
            //stepp 4
            Finalval = ((Yintervals.GetValue(1) - Yintervals.GetValue(0)) / (_persistentCp.Betavec.GetValue(B2) - _persistentCp.Betavec.GetValue(B1)))
                            * (Beta - _persistentCp.Betavec.GetValue(B1))
                        + Yintervals.GetValue(0);

        }

    }
}
