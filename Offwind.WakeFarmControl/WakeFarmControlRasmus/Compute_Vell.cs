using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class Compute_Vell : MatlabCode
    {
        // Wake Simulation
        // (C) Rasmus Christensen
        // Control and Automation, Aalborg University 2014

        // Compute the velocity in front of each wind-turbine, with respect to the wind input.
        public static void Calculate(out ILArray<double> vel_output, ILArray<double> yTurb, ILArray<int> xTurbC, ILArray<int> yTurbC, ILArray<double> x, ILArray<double> wField, ILArray<double> Uhub, double kWake, int iMax, int jMax, int nTurb, double dTurb, ILArray<double> Ct, double dy)
        {
            ILArray<double> vell_i = wField.C;

            ILArray<double> shadow = zeros(1, nTurb);

            double r0 = 0.5 * dTurb;
            double nk = 2 * ceil(dTurb / dy);

            for (var k = 1; k <= nTurb; k++)
            {
                int J = 0;
                double SS = 0;
                var SS0 = pi * r0 * r0;

                for (var i = 1; i <= k - 1; i++)
                {
                    double RR_i = r0 + kWake * (x._get(xTurbC._get(k)) - x._get(xTurbC._get(i)));
                    double Dij = abs(yTurb._get(i) - yTurb._get(k));

                    if ((RR_i >= (r0 + Dij)) || (Dij <= dy))
                    {
                        SS = SS + ((r0 * r0) / (RR_i * RR_i));
                    }
                    else
                    {
                        if (RR_i >= (r0 + Dij) && Dij > dy)
                        {
                            J = J + 1;
                            double Alpha_i = acos((RR_i * RR_i) + (Dij * Dij) - (r0 * r0) / (2 * RR_i * Dij));
                            double Alpha_k = acos(((r0 * r0) + (Dij * Dij) - (RR_i * RR_i)) / (2 * r0 * Dij));
                            double Area;
                            AArea(RR_i, r0, Dij, out Area);
                            shadow._set(J, (Alpha_i * (RR_i * RR_i) + Alpha_k * (r0 * r0)) - 2 * Area);
                            SS = SS + ((shadow._get(J)) / SS0) * ((r0 * r0) / (RR_i * RR_i));
                        }
                    }
                }

                for (var ii = xTurbC._get(k); ii <= iMax; ii++)
                {
                    double rrt = r0 + kWake * (x._get(ii) - x._get(xTurbC._get(k)));
                    double nj = (ceil(rrt / dy));

                    int jjMin = (int)floor(max(1, yTurbC._get(k) - nj));
                    int jjMax = (int)ceil(min(new double[] { jMax, yTurbC._get(k) + nj }));

                    for (var j = jjMin; j <= jjMax; j++)
                    {
                        if (((-vell_i._get(ii, j) + Uhub._get(k)) > 0) && (ii > xTurbC._get(k) + nk))
                        {
                            vell_i._set(ii, j, min(_[ vell_i._get(ii - 1, j), Uhub._get(k) + Uhub._get(k) * (sqrt(1 - Ct._get(k)) - 1) * ((r0 * r0) / (rrt * rrt)) * (1 - (1 - sqrt(1 - Ct._get(k))) * SS) ]));
                            vell_i._set(ii, j, max(_[ 0, vell_i._get(ii, j) ]));
                        }
                        else
                        {
                            vell_i._set(ii, j, (Uhub._get(k) + Uhub._get(k) * (sqrt(1 - Ct._get(k)) - 1) * (r0 / rrt) * (r0 / rrt)) * (1 - (1 - sqrt(1 - Ct._get(k))) * SS));
                            vell_i._set(ii, j, max(_[ 0, vell_i._get(ii, j) ]));
                        }
                    }
                }
            }

            vel_output = vell_i;
        }

        public static void AArea(double x, double y, double z, out double area_out) // Internal function to compute the shadowed area.
        {
            double PP = (x + y + z) * 0.5;
            area_out = sqrt(PP * (PP - x) * (PP - y) * (PP - z));
        }
    }
}
