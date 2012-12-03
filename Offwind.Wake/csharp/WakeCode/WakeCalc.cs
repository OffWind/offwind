using System;

namespace WakeCode
{
    public class WakeCalc
    {
        private const double pi = 3.1415926535897;

        public void Initialize(GeneralData generalData, CalcData calcData)
        {
            calcData.x = new double[generalData.GridPointsX];
            calcData.y = new double[generalData.GridPointsY];
            calcData.vell_i = new double[generalData.GridPointsX, generalData.GridPointsY];
            calcData.R_TURB = new double[generalData.TurbinesAmount];
            calcData.WPOWER = new double[generalData.TurbinesAmount];

            calcData.xc_turb = new Int32[generalData.TurbinesAmount];
            calcData.yc_turb = new Int32[generalData.TurbinesAmount];
        }

        public void Run(GeneralData generalData, CalcData calcData)
        {
            //************************************************************************
            //ROTATE THE DOMAIN, AND THE X,Y COORDINATE OF THE TURBINE so that the wind to be in x direction
            //------------------------------------------------------------------
            ROTATE_coord(generalData);
            if (generalData.TurbineThrust > 1)
            {
                Console.WriteLine(" The value of the TurbineThrust should be less 1, hence TurbineThrust=0.3)");
                generalData.TurbineThrust = 0.3;
            }

            calcData.Cp = 0.5 * (1 + Math.Sqrt(1 - generalData.TurbineThrust)) * generalData.TurbineThrust;

            ORDER(generalData);

            DOMAIN_PT(ref calcData.x, ref generalData.GridPointsX, ref calcData.dx, ref generalData.TurbineDiameter, ref generalData.x_turb, ref generalData.TurbinesAmount, ref calcData.xmax, ref calcData.xmin, 5.0);
            DOMAIN_PT(ref calcData.y, ref generalData.GridPointsY, ref calcData.dy, ref generalData.TurbineDiameter, ref generalData.y_turb, ref generalData.TurbinesAmount, ref calcData.ymax, ref calcData.ymin, 2.0);
            Turb_centr_coord(ref generalData.TurbinesAmount, ref generalData.GridPointsX, ref calcData.x, ref generalData.x_turb, ref calcData.xc_turb);
            Turb_centr_coord(ref generalData.TurbinesAmount, ref generalData.GridPointsY, ref calcData.y, ref generalData.y_turb, ref calcData.yc_turb);
            COMPUTE_VELL(generalData, calcData);
            COMPUTE_WPower(generalData, calcData);
        }

        /// <summary>
        /// rotate the coordinate of the turbines
        /// </summary>
        /// <param name="generalData"></param>
        private void ROTATE_coord(GeneralData generalData)
        {
            var XX_TURB = new double[generalData.TurbinesAmount];
            var YY_TURB = new double[generalData.TurbinesAmount];
            double ang1;
            ang1 = generalData.RotationAngle * pi / 180;
            for (var i = 0; i <= generalData.TurbinesAmount - 1; i++)
            {
                XX_TURB[i] = generalData.x_turb[i] * Math.Cos(ang1) - generalData.y_turb[i] * Math.Sin(ang1);
                YY_TURB[i] = generalData.x_turb[i] * Math.Sin(ang1) + generalData.y_turb[i] * Math.Cos(ang1);
            }

            for (var i = 0; i <= generalData.TurbinesAmount - 1; i++)
            {
                generalData.x_turb[i] = XX_TURB[i];
                generalData.y_turb[i] = YY_TURB[i];
            }
        } // 


        /// <summary>
        /// COMPUTE THE GRID POINTS
        /// </summary>
        /// <param name="XX"></param>
        /// <param name="IIMAX"></param>
        /// <param name="DDX"></param>
        /// <param name="DDtur"></param>
        /// <param name="XX_TURB"></param>
        /// <param name="NN_TURB"></param>
        /// <param name="XXMAX"></param>
        /// <param name="XXMIN"></param>
        /// <param name="pppoint"></param>
        private void DOMAIN_PT(ref double[] XX, ref System.Int32 IIMAX, ref double DDX, ref double DDtur, ref double[] XX_TURB, ref System.Int32 NN_TURB, ref double XXMAX, ref double XXMIN, double pppoint)
        {
            XXMAX = XX_TURB[0];
            XXMIN = XX_TURB[0];
            for (var i = 1; i <= NN_TURB - 1; i++)
            {
                if (XX_TURB[i] > XXMAX)
                {
                    XXMAX = XX_TURB[i];
                }
                if (XX_TURB[i] < XXMIN)
                {
                    XXMIN = XX_TURB[i];
                }
            }

            XXMAX = XXMAX + DDtur * pppoint;
            XXMIN = XXMIN - 2 * DDtur;

            XX[0] = XXMIN;
            DDX = (XXMAX - XXMIN) / (IIMAX - 1);
            for (var i = 1; i <= IIMAX - 1; i++)
            {
                XX[i] = XX[i - 1] + DDX;
            }
        }

        /// <summary>
        /// ROTATE COORDINATE THAT THE wind to be in X ? DIRECTION
        /// the subroutine determine the coordinates of the center of the turbine
        /// </summary>
        /// <param name="nn"></param>
        /// <param name="iimax"></param>
        /// <param name="xx"></param>
        /// <param name="xx_turb"></param>
        /// <param name="xxc_turb"></param>
        private void Turb_centr_coord(ref System.Int32 nn, ref System.Int32 iimax, ref double[] xx, ref double[] xx_turb, ref System.Int32[] xxc_turb)
        {
            for (var i = 0; i <= nn - 1; i++)
                for (var ii = 0; ii <= iimax - 2; ii++)
                {
                    if (xx[ii] <= xx_turb[i] && xx_turb[i] < xx[ii + 1])
                    {
                        xxc_turb[i] = ii + 1;
                        break;
                    }
                }
        }

        /// <summary>
        /// ORDER of THE TURBINE in function of x coordinate
        /// </summary>
        /// <param name="generalData"></param>
        private void ORDER(GeneralData generalData)
        {
            double aa;
            double bb;

            for (var i = 1; i <= generalData.TurbinesAmount - 1; i++)
                for (var j = 0; j <= generalData.TurbinesAmount - i - 1; j++)
                {
                    if (generalData.x_turb[j] > generalData.x_turb[j + 1])
                    {
                        aa = generalData.x_turb[j];
                        bb = generalData.y_turb[j];
                        generalData.x_turb[j] = generalData.x_turb[j + 1];
                        generalData.y_turb[j] = generalData.y_turb[j + 1];
                        generalData.x_turb[j + 1] = aa;
                        generalData.y_turb[j + 1] = bb;
                    }
                }

            for (var i = 0; i <= generalData.TurbinesAmount - 1; i++)
            {
                for (var k = i + 1; k <= generalData.TurbinesAmount - 1; k++)
                {
                    if (generalData.x_turb[i] == generalData.x_turb[k])
                    {
                        if (generalData.y_turb[i] > generalData.y_turb[k])
                        {
                            aa = generalData.y_turb[i];
                            generalData.y_turb[i] = generalData.y_turb[k];
                            generalData.y_turb[k] = aa;
                        }
                    }
                }
            }
        }

        private int INT(double doubleValue)
        {
            return (int)doubleValue;
        }

        /// <summary>
        /// SUBROUTINE  _SHADOW AREA
        /// </summary>
        /// <param name="generalData"></param>
        /// <param name="generalData"></param>
        private void COMPUTE_VELL(GeneralData generalData, CalcData calcData)
        {
            var SHADOW = new double[generalData.TurbinesAmount];
            double rr_max = 0;
            double area = 0;

            for (var i = 0; i <= generalData.GridPointsX - 1; i++)
                for (var j = 0; j <= generalData.GridPointsY - 1; j++)
                {
                    calcData.vell_i[i, j] = generalData.VelocityAtHub;
                }

            double r0 = 0.5 * generalData.TurbineDiameter;

            int nk = 2 * (INT(generalData.TurbineDiameter / calcData.dy));
            for (var k = 1; k <= generalData.TurbinesAmount; k++)
            {
                int J = 0;
                double SS = 0.0;
                double ss0 = (pi * r0 * r0);

                for (var i = 1; i <= k - 1; i = i + 1) // calculate the influence of the turbine i over the turbine k
                {
                    double RR_I = r0 + generalData.WakeDecay * (calcData.x[calcData.xc_turb[k - 1] - 1] - calcData.x[calcData.xc_turb[i - 1] - 1]);
                    double DIJ = Math.Abs(generalData.y_turb[i - 1] - generalData.y_turb[k - 1]);
                    if (RR_I >= (r0 + DIJ) || DIJ <= calcData.dy)
                    {
                        SS = SS + ((r0 * r0) / (RR_I * RR_I));
                    }
                    else
                    {
                        if ((DIJ) < (RR_I + r0) && (DIJ) > calcData.dy)
                        {
                            J = J + 1;
                            double ALPHA_I = (RR_I * RR_I) + (DIJ * DIJ) - (r0 * r0);
                            ALPHA_I = ALPHA_I / (2 * RR_I * DIJ);
                            ALPHA_I = Math.Acos(ALPHA_I);
                            double ALPHA_K = (r0 * r0) + (DIJ * DIJ) - (RR_I * RR_I);
                            ALPHA_K = ALPHA_K / (2 * r0 * DIJ);
                            ALPHA_K = Math.Acos(ALPHA_K);
                            AAREA(ref RR_I, ref r0, ref DIJ, ref area);

                            SHADOW[J - 1] = (ALPHA_I * (Math.Pow(RR_I, 2)) + ALPHA_K * (Math.Pow(r0, 2)));
                            SHADOW[J - 1] = SHADOW[J - 1] - 2 * area;
                            SS = SS + ((SHADOW[J - 1]) / ss0) * ((r0 * r0) / (RR_I * RR_I));
                        }
                        else
                        {
                            SS = SS;
                        }
                    }
                }

                for (var ii = calcData.xc_turb[k - 1]; ii <= generalData.GridPointsX; ii++)
                {
                    double rrt = r0 + generalData.WakeDecay * (calcData.x[ii - 1] - calcData.x[calcData.xc_turb[k - 1] - 1]);
                    rr_max = Math.Max(rrt, rr_max);
                    int nj = (INT(rrt / calcData.dy));
                    int jj_min = Math.Max(1, calcData.yc_turb[k - 1] - nj);
                    int jj_max = Math.Min(generalData.GridPointsY, calcData.yc_turb[k - 1] + nj);

                    for (J = jj_min; J <= jj_max; J++)
                    {
                        if (((-calcData.vell_i[ii - 1, J - 1] + generalData.VelocityAtHub) > 0) && (ii > calcData.xc_turb[k - 1] + nk))
                        {
                            double vv = calcData.vell_i[ii - 1, J - 1];
                            calcData.vell_i[ii - 1, J - 1] = generalData.VelocityAtHub + generalData.VelocityAtHub * (Math.Sqrt(1 - generalData.TurbineThrust) - 1) * ((r0 * r0) / (rrt * rrt));
                            calcData.vell_i[ii - 1, J - 1] = calcData.vell_i[ii - 1, J - 1] * (1 - (1 - Math.Sqrt(1 - generalData.TurbineThrust)) * SS);
                            //vell_i(ii,j)=(vell_i(ii,j)+0.15*vv)/1.15;
                            calcData.vell_i[ii - 1, J - 1] = Math.Min(vv, calcData.vell_i[ii - 1, J - 1]);
                        }
                        else
                        {
                            calcData.vell_i[ii - 1, J - 1] = generalData.VelocityAtHub + generalData.VelocityAtHub * (Math.Sqrt(1 - generalData.TurbineThrust) - 1) * (r0 / rrt) * (r0 / rrt);
                            calcData.vell_i[ii - 1, J - 1] = calcData.vell_i[ii - 1, J - 1] * (1 - (1 - Math.Sqrt(1 - generalData.TurbineThrust)) * SS);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// SUBROUTINE  compute the power at the distance PowerDistance behind the WT
        /// </summary>
        /// <param name="generalData"></param>
        /// <param name="generalData"></param>
        /// <param name="calcData"> </param>
        private void COMPUTE_WPower(GeneralData generalData, CalcData calcData)
        {
            var SHADOW = new double[generalData.TurbinesAmount];
            var v_power = new double[generalData.TurbinesAmount];

            double area = 0;
            double r0 = 0.5 * generalData.TurbineDiameter;
            double ss0 = (pi * r0 * r0);
            var I = (int)Math.Truncate(generalData.PowerDistance / calcData.dx);
            int nd = Math.Max(1, I);

            for (var k = 1; k <= generalData.TurbinesAmount; k++)
            {
                int J = 0;
                double SS = 0.0;
                int nk = Math.Max(1, calcData.xc_turb[k - 1] - nd);
                double vv1 = calcData.vell_i[nk - 1, calcData.yc_turb[k - 1] - 1];
                calcData.WPOWER[k - 1] = 0.0;
                double vv2 = 0.0;
                for (var i = k - 1; i >= 1; i = i - 1) // calculate the influence of the turbine i over the turbine k
                {
                    double RR_I = r0 + generalData.WakeDecay * (calcData.x[nk - 1] - calcData.x[calcData.xc_turb[i - 1] - 1]);
                    double DIJ = Math.Abs(generalData.y_turb[i - 1] - generalData.y_turb[k - 1]);

                    if (((DIJ) < (RR_I + r0)) && (RR_I <= (r0 + DIJ)))
                    {
                        J = J + 1;

                        double ALPHA_I = (RR_I * RR_I) + (DIJ * DIJ) - (r0 * r0);
                        ALPHA_I = ALPHA_I / (2 * RR_I * DIJ);
                        ALPHA_I = Math.Acos(ALPHA_I);

                        double ALPHA_K = (r0 * r0) + (DIJ * DIJ) - (RR_I * RR_I);
                        ALPHA_K = ALPHA_K / (2 * r0 * DIJ);
                        ALPHA_K = Math.Acos(ALPHA_K);
                        AAREA(ref RR_I, ref r0, ref DIJ, ref area);

                        SHADOW[J - 1] = (ALPHA_I * (Math.Pow(RR_I, 2)) + ALPHA_K * (Math.Pow(r0, 2)));
                        SHADOW[J - 1] = SHADOW[J - 1] - 2 * area;

                        SS = SS + SHADOW[J - 1];
                        if (SS < ss0)
                        {
                            int jj_max;
                            int jj_min;
                            int mm;
                            if (generalData.y_turb[k - 1] > generalData.y_turb[i - 1])
                            {
                                mm = (INT(RR_I / calcData.dy));
                                jj_max = Math.Min(generalData.GridPointsY, calcData.yc_turb[i - 1] + mm + 1);
                                jj_min = Math.Max(1, calcData.yc_turb[i - 1] + mm - 2);
                                vv1 = calcData.vell_i[nk - 1, jj_max - 1];
                                v_power[J - 1] = calcData.vell_i[nk - 1, jj_min - 1];
                            }
                            else
                            {
                                mm = (INT(RR_I / calcData.dy));
                                jj_max = Math.Min(generalData.GridPointsY, calcData.yc_turb[i - 1] + mm + 1);
                                jj_min = Math.Max(1, calcData.yc_turb[i - 1] + mm - 2);
                                vv1 = calcData.vell_i[nk - 1, jj_min - 1];
                                v_power[J - 1] = calcData.vell_i[nk - 1, jj_max - 1];
                            }
                        }
                        else
                        {
                            J = J - 1;
                        }
                    }
                }

                if (J > 0)
                {
                    for (var i = 1; i <= J; i++)
                    {
                        vv2 = v_power[J - 1] * SHADOW[J - 1] + vv2;
                    }
                }
                vv2 = (vv2 + vv1 * (ss0 - SS)) / ss0;

                calcData.WPOWER[k - 1] = 0.5 * generalData.AirDensity * (Math.Pow(vv2, 3)) * ss0 * calcData.Cp;
            }
        }

        /// <summary>
        /// FUNCTION : COMPUTE AREA
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="Z"></param>
        /// <param name="area"></param>
        private void AAREA(ref double X, ref double Y, ref double Z, ref double area)
        {
            double PP = (X + Y + Z) * 0.5;
            area = Math.Sqrt(PP * (PP - X) * (PP - Y) * (PP - Z));
            return;
        }
    }
}
