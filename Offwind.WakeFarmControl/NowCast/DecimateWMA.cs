using System;
using ILNumerics;

namespace WakeFarmControl.NowCast
{
    internal partial class TranslatedCode
    {
        private const int symDef = 0;

        private static void DecimateWMA(out ILArray<double> y, out ILArray<double> t, ILArray<double> x, int r, int sym = symDef)
        {
            #region "Original function comments"
            //DecimateWMA Decimate the matrix column by column using a moving average
            // FIR filter of length r
            //
            // [y,yi]= DecimateWMA(x,r,sym)
            //
            // x : Matrix (nxm) with columns to be decimated
            // r : Length of MA filter
            // sym : sym= 1/true gives t corresponding to a symetric/non causal filter
            //       i.e. t is changed to mid point
            // y : Decimated matrix (floor(n/r)xm)
            // t : Index/time vector corresponding to the original index/time for x
            // 
            // External input: None

            // Time-stamp: <2014-10-14 11:33:53 tk>
            // Version 1: 2014-10-14 11:31:55 tk This version uses reshape and mean
            //            which is much faster compared to the more straight forward
            //            version
            // Torben Knudsen
            // Aalborg University, Dept. of Electronic Systems, Section of Automation
            // and Control
            // E-mail: tk@es.aau.dk

            //% setting up inputs
            //symDef = 0;
            //if nargin < 3; sym= symDef; end;
            //if nargin < 2; error('Error TK: To few input arguments'); end;

            //% Parameters

            //% Definitions etc.

            //% Algorithm
            #endregion

            #region "Used variables declaration"
            int n;
            int m;
            int ny;
            int j;
            ILArray<double> xx;
            #endregion

            // Only use integer number off r rows
            var size_x_ = x.Size;
            n = size_x_[0];
            m = size_x_[1];
            ny = (int)(Math.Floor(((double)n) / r));
            x = x[ILMath.r(1 - 1, ny * r - 1), ILMath.full];

            y = ILMath.zeros(ny, m);
            // Directly calculate the output
            // This is much slower compared to the below using reshape
            // $$$ for i= 1:ny;
            // $$$   y(i,:)= sum(x((1:r)+(i-1)*r,:))/r;
            // $$$ end;
            // Use reshape
            for (j = 1; j <= m; j++)
            {
                xx = ILMath.reshape(x[ILMath.full, j - 1], r, ny);
                y[ILMath.full, j - 1] = (ILMath.mean(xx)).T;
            }
            t = _a(1, ny) * r;
            // If symmetric the first time/index must be (r+1)/2
            if (sym != 0)
            {
                t = t - r + (r + 1) / 2;
            }
        }
    }
}
