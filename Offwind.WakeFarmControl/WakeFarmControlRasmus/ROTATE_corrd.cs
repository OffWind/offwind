using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class ROTATE_corrd : MatlabCode
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University

        // N_turb = number of turbines.
        // X_turb = x-position of turbine.
        // Y_turb = y-position of turbine.
        public static void Calculate(out ILArray<double> out_x, out ILArray<double> out_y, ILArray<double> xTurb, ILArray<double> yTurb, double rotA)
        {
            ILArray<double> x_out = zeros(1, length(xTurb)); // Initialization x-coordinates
            ILArray<double> y_out = zeros(1, length(yTurb)); // Initialization y-coordinates
    
            rotA = rotA * pi / 180; // Conversion to radians

            for (var i = 1; i <= length(xTurb); i++)
            {
                x_out._(i, '=', xTurb._(i) * cos(rotA) - xTurb._(i) * sin(rotA));
                y_out._(i, '=', xTurb._(i) * sin(rotA) + yTurb._(i) * cos(rotA));
            }

            if (min(x_out) < 0) // Moves the x-points if these are negative.
            {
                x_out = x_out + 500 + abs(min(x_out));
            }

            if (min(y_out) < 0) // Moves the y-points if these are negative.
            {
                y_out = y_out + 500 + abs(min(y_out));
            }
    
	        out_x = x_out;
	        out_y = y_out;
        }
    }
}
