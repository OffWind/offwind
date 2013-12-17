using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WakeCode
{
    public class ResultDrawer
    {
        //private static Color[] Colors = new Color[] {
        //    Color.FromArgb(59, 76, 192),
        //    Color.FromArgb(103, 136, 238),
        //    Color.FromArgb(163, 192, 254),
        //    Color.FromArgb(217, 220, 225),
        //    Color.FromArgb(247, 186, 159),
        //    Color.FromArgb(235, 125, 98),
        //    Color.FromArgb(185, 22, 41)
        //};

        private static Color[] Colors = new Color[] {
            Color.FromArgb(59, 76, 192),
            Color.FromArgb(63, 83, 199),
            Color.FromArgb(68, 90, 204),
            Color.FromArgb(73, 97, 210),
            Color.FromArgb(78, 104, 216),
            Color.FromArgb(83, 111, 221),
            Color.FromArgb(88, 117, 225),
            Color.FromArgb(93, 124, 230),
            Color.FromArgb(98, 130, 234),
            Color.FromArgb(103, 136, 238),
            Color.FromArgb(109, 143, 241),
            Color.FromArgb(114, 149, 244),
            Color.FromArgb(119, 154, 247),
            Color.FromArgb(125, 160, 249),
            Color.FromArgb(130, 166, 251),
            Color.FromArgb(136, 171, 253),
            Color.FromArgb(141, 176, 254),
            Color.FromArgb(147, 181, 255),
            Color.FromArgb(153, 185, 255),
            Color.FromArgb(158, 190, 255),
            Color.FromArgb(163, 194, 254),
            Color.FromArgb(169, 198, 254),
            Color.FromArgb(174, 201, 252),
            Color.FromArgb(179, 205, 251),
            Color.FromArgb(185, 208, 249),
            Color.FromArgb(190, 210, 246),
            Color.FromArgb(195, 213, 244),
            Color.FromArgb(199, 215, 241),
            Color.FromArgb(204, 217, 237),
            Color.FromArgb(209, 218, 233),
            Color.FromArgb(213, 219, 229),
            Color.FromArgb(217, 220, 225),
            Color.FromArgb(221, 220, 220),
            Color.FromArgb(225, 218, 214),
            Color.FromArgb(229, 216, 208),
            Color.FromArgb(233, 213, 203),
            Color.FromArgb(236, 210, 197),
            Color.FromArgb(239, 207, 191),
            Color.FromArgb(241, 204, 184),
            Color.FromArgb(243, 200, 178),
            Color.FromArgb(245, 196, 172),
            Color.FromArgb(246, 191, 166),
            Color.FromArgb(247, 186, 159),
            Color.FromArgb(247, 181, 153),
            Color.FromArgb(247, 176, 147),
            Color.FromArgb(247, 171, 140),
            Color.FromArgb(246, 165, 134),
            Color.FromArgb(245, 159, 128),
            Color.FromArgb(244, 153, 122),
            Color.FromArgb(242, 146, 116),
            Color.FromArgb(240, 139, 110),
            Color.FromArgb(238, 132, 104),
            Color.FromArgb(235, 125, 98),
            Color.FromArgb(232, 118, 92),
            Color.FromArgb(229, 111, 86),
            Color.FromArgb(225, 103, 81),
            Color.FromArgb(221, 95, 75),
            Color.FromArgb(216, 86, 70),
            Color.FromArgb(212, 78, 65),
            Color.FromArgb(207, 69, 60),
            Color.FromArgb(202, 59, 55),
            Color.FromArgb(196, 49, 50),
            Color.FromArgb(191, 37, 46),
            Color.FromArgb(185, 22, 41)
        };

        private static Color GetColor(Color[] colors, double position)
        {
            for (int i = 0; i < colors.Length; i++)
            {
                if (i + 1 > (colors.Length - 1) * position)
                {
                    return colors[i];
                }
            }

            return Color.Black;
        }

        private static Color GetGradientColor(Color[] colors, double position)
        {
            for (int i = 0; i < colors.Length - 1; i++)
            {
                if (i + 1 >= (colors.Length - 1) * position)
                {
                    return GetGradientColor(colors[i], colors[i + 1], (colors.Length - 1) * position - i);
                }
            }

            return Color.Black;
        }

        private static Color GetGradientColor(Color minPositionColor, Color maxPositionColor, double position)
        {
            var positionR = minPositionColor.R + (int)(position * (maxPositionColor.R - minPositionColor.R));
            var positionG = minPositionColor.G + (int)(position * (maxPositionColor.G - minPositionColor.G));
            var positionB = minPositionColor.B + (int)(position * (maxPositionColor.B - minPositionColor.B));

            return Color.FromArgb(positionR, positionG, positionB);
        }

        private static Image DrawContourPlot(int xCount, int yCount, double[] x, double[] y, double[,] rho, double airDensity, double[,] rho_vell, int imageWidth, int imageHeight)
        {
            if (x.GetLength(0) != xCount ||
                y.GetLength(0) != yCount ||
                //rho.GetLength(0) != xCount || rho.GetLength(1) != yCount ||
                rho_vell.GetLength(0) != xCount || rho_vell.GetLength(1) != yCount)
            {
                return null;
            }

            if (xCount < 1 || yCount < 1)
            {
                return null;
            }

            double xMin = x[0], xMax = x[0];
            double yMin = y[0], yMax = y[0];
            double rhoMin = rho[0, 0], rhoMax = rho[0, 0];
            double rho_vellMin = (airDensity * rho_vell[0, 0]), rho_vellMax = (airDensity * rho_vell[0, 0]);

            for (var i = 0; i < xCount; i++)
            {
                for (var j = 0; j < yCount; j++)
                {
                    xMin = Math.Min(x[i], xMin); xMax = Math.Max(x[i], xMax);
                    yMin = Math.Min(y[j], yMin); yMax = Math.Max(y[j], yMax);
                    //rhoMin = Math.Min(rho[i, j], rhoMin); rhoMax = Math.Max(rho[i, j], rhoMax);
                    rho_vellMin = Math.Min(airDensity * rho_vell[i, j], rho_vellMin); rho_vellMax = Math.Max(airDensity * rho_vell[i, j], rho_vellMax);
                }
            }

            Bitmap bitmap = new Bitmap(imageWidth, imageHeight);
            double eps = 1.0E-14;
            for (var i = 0; i < xCount; i++)
            {
                for (var j = 0; j < yCount; j++)
                {
                    int pixelXCoord = 0;
                    if (xMax - xMin > eps)
                    {
                        pixelXCoord = (int)Math.Round((x[i] - xMin) / (xMax - xMin) * (bitmap.Width - 1));
                    }

                    int pixelYCoord = 0;
                    if (yMax - yMin > eps)
                    {
                        pixelYCoord = (int)Math.Round((1 - (y[j] - yMin) / (yMax - yMin)) * (bitmap.Height - 1));
                    }

                    Color pixelColor = GetGradientColor(Colors, (rho_vellMax - rho_vellMin > eps) ? (airDensity * rho_vell[i, j] - rho_vellMin) / (rho_vellMax - rho_vellMin) : 0.5);

                    bitmap.SetPixel(pixelXCoord, pixelYCoord, pixelColor);
                }
            }

            return bitmap;
        }

        public static Image ProcessResult(GeneralData generalData, CalcData calcData, int imageWidth, int imageHeight)
        {
            return DrawContourPlot(generalData.GridPointsX, generalData.GridPointsY, calcData.x, calcData.y, new double[1, 1], generalData.AirDensity, calcData.vell_i, imageWidth, imageHeight);
        }
    }
}
