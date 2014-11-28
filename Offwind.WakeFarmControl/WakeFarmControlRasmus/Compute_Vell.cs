using System;
using ILNumerics;

namespace WakeFarmControl
{
    public sealed class Compute_Vell
    {
        // Wake Simulation
        // (C) Rasmus Christensen
        // Control and Automation, Aalborg University 2014

        // Compute the velocity in front of each wind-turbine, with respect to the wind input.
        public static void Calculate(ILArray<double> yTurb, ILArray<int> xTurbC, ILArray<int> yTurbC, ILArray<double> x, ILArray<double> wField, ILArray<double> Uhub, double kWake, int iMax, int jMax, int nTurb, double dTurb, ILArray<double> Ct, double dy, out ILArray<double> vel_output)
        {
            ILArray<double> vell_i = wField.C;

            ILArray<double> shadow = ILMath.zeros(1, nTurb);

            double r0 = 0.5 * dTurb;
            double nk = 2 * ((double)(ILMath.ceil(dTurb / dy)));

            for (var k = 1; k <= nTurb; k++)
            {
                int J = 0;
                double SS = 0;
                var SS0 = ILMath.pi * r0 * r0;

                for (var i = 1; i <= k - 1; i++)
                {
                    double RR_i = r0 + kWake * (x.GetValue(xTurbC.GetValue(k - 1)) - x.GetValue(xTurbC.GetValue(i - 1)));
                    double Dij = (double)(ILMath.abs(yTurb.GetValue(i - 1) - yTurb.GetValue(k - 1)));

                    if ((RR_i >= (r0 + Dij)) || (Dij <= dy))
                    {
                        SS = SS + ((r0 * r0) / (RR_i * RR_i));
                    }
                    else
                    {
                        if (RR_i >= (r0 + Dij) && Dij > dy)
                        {
                            J = J + 1;
                            double Alpha_i = (double)(ILMath.acos((RR_i * RR_i) + (Dij * Dij) - (r0 * r0) / (2 * RR_i * Dij)));
                            double Alpha_k = (double)(ILMath.acos(((r0 * r0) + (Dij * Dij) - (RR_i * RR_i)) / (2 * r0 * Dij)));
                            double Area;
                            AArea(RR_i, r0, Dij, out Area);
                            shadow.SetValue((Alpha_i * (RR_i * RR_i) + Alpha_k * (r0 * r0)) - 2 * Area, J - 1);
                            SS = SS + ((shadow.GetValue(J - 1)) / SS0) * ((r0 * r0) / (RR_i * RR_i));
                        }
                    }
                }

                for (var ii = xTurbC.GetValue(k - 1); ii <= iMax; ii++)
                {
                    double rrt = r0 + kWake * (x.GetValue(ii - 1) - x.GetValue(xTurbC.GetValue(k - 1)));
                    int nj = (int)(ILMath.ceil(rrt / dy));

                    int jjMin = (int)(ILMath.floor((double)(ILMath.max((ILArray<int>)0, (ILArray<int>)(yTurbC.GetValue(k - 1) - nj)))));
                    int jjMax = (int)(ILMath.ceil((double)(ILMath.min((ILArray<int>)(new int[] { jMax, yTurbC.GetValue(k - 1) + nj })))));

                    for (var j = jjMin; j <= jjMax; j++)
                    {
                        if (((-vell_i.GetValue(ii - 1, j - 1) + Uhub.GetValue(k - 1)) > 0) && (ii > xTurbC.GetValue(k - 1) + nk))
                        {
                            vell_i.SetValue(Math.Min(vell_i.GetValue(ii - 2, j - 1), Uhub.GetValue(k - 1) + Uhub.GetValue(k - 1) * (Math.Sqrt(1 - Ct.GetValue(k - 1)) - 1) * ((r0 * r0) / (rrt * rrt)) * (1 - (1 - Math.Sqrt(1 - Ct.GetValue(k - 1))) * SS)), ii - 1, j - 1);
                            vell_i.SetValue(Math.Max(0, vell_i.GetValue(ii - 1, j - 1)), ii - 1, j - 1);
                        }
                        else
                        {
                            vell_i.SetValue((Uhub.GetValue(k - 1) + Uhub.GetValue(k - 1) * (Math.Sqrt(1 - Ct.GetValue(k - 1)) - 1) * (r0 / rrt) * (r0 / rrt)) * (1 - (1 - Math.Sqrt(1 - Ct.GetValue(k - 1))) * SS), ii - 1, j - 1);
                            vell_i.SetValue(Math.Max(0, vell_i.GetValue(ii - 1, j - 1)), ii - 1, j - 1);
                        }
                    }
                }
            }

            vel_output = vell_i;
        }

        public static void AArea(double x, double y, double z, out double area_out) // Internal function to compute the shadowed area.
        {
            double PP = (x + y + z) * 0.5;
            area_out = Math.Sqrt(PP * (PP - x) * (PP - y) * (PP - z));
        }
    }
}
