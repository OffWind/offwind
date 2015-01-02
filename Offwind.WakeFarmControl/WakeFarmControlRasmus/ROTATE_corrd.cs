using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControlR
{
    internal partial class TranslatedCode
    {
        #region "Original function comments"
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University

        // N_turb = number of turbines.
        // X_turb = x-position of turbine.
        // Y_turb = y-position of turbine.
        #endregion
        internal static void ROTATE_corrd(out ILArray<double> out_x, out ILArray<double> out_y, ILArray<double> xTurb, ILArray<double> yTurb, double rotA)
        {
            #region "Used variables declaration"
            ILArray<double> x_out;
            ILArray<double> y_out;
            int i;
            #endregion

            x_out = zeros(1, length(xTurb)); // Initialization x-coordinates
            y_out = zeros(1, length(yTurb)); // Initialization y-coordinates
    
            rotA = rotA * pi / 180; // Conversion to radians

            for (i = 1; i <= length(xTurb); i++)
            {
                x_out._(i, '=', xTurb._(i) * cos(rotA) - xTurb._(i) * sin(rotA));
                y_out._(i, '=', xTurb._(i) * sin(rotA) + yTurb._(i) * cos(rotA));
            }

            if (min_(x_out) < 0) // Moves the x-points if these are negative.
            {
                x_out = x_out + 500 + abs(min_(x_out));
            }

            if (min_(y_out) < 0) // Moves the y-points if these are negative.
            {
                y_out = y_out + 500 + abs(min_(y_out));
            }
    
	        out_x = x_out;
	        out_y = y_out;
        }
    }
}
