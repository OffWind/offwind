using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WakeCode
{
    public class ResultDrawer
    {
        private static Color[] Colors = new Color[] {
            Color.FromArgb(59, 76, 192),
            Color.FromArgb(103, 136, 238),
            Color.FromArgb(163, 192, 254),
            Color.FromArgb(217, 220, 225),
            Color.FromArgb(247, 186, 159),
            Color.FromArgb(235, 125, 98),
            Color.FromArgb(185, 22, 41)
        };

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

        private static Image DrawContourPlot(int xCount, int yCount, double[] x, double[] y, double[,] rho, double[,] rho_vell, int imageWidth, int imageHeight)
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
            double rho_vellMin = rho_vell[0, 0], rho_vellMax = rho_vell[0, 0];

            for (var i = 0; i < xCount; i++)
            {
                for (var j = 0; j < yCount; j++)
                {
                    xMin = Math.Min(x[i], xMin); xMax = Math.Max(x[i], xMax);
                    yMin = Math.Min(y[j], yMin); yMax = Math.Max(y[j], yMax);
                    //rhoMin = Math.Min(rho[i, j], rhoMin); rhoMax = Math.Max(rho[i, j], rhoMax);
                    rho_vellMin = Math.Min(rho_vell[i, j], rho_vellMin); rho_vellMax = Math.Max(rho_vell[i, j], rho_vellMax);
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

                    Color pixelColor = GetGradientColor(Colors, (rho_vellMax - rho_vellMin > eps) ? (rho_vell[i, j] - rho_vellMin) / (rho_vellMax - rho_vellMin) : 0.5);

                    bitmap.SetPixel(pixelXCoord, pixelYCoord, pixelColor);
                }
            }

            return bitmap;
        }

        public static Image ProcessResult(GeneralData generalData, CalcData calcData, int imageWidth, int imageHeight)
        {
            return DrawContourPlot(generalData.GridPointsX, generalData.GridPointsY, calcData.x, calcData.y, new double[1,1], calcData.vell_i, imageWidth, imageHeight);
        }
    }
}
