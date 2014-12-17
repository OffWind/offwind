using System;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class ROTATE_corrd
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University

        // N_turb = number of turbines.
        // X_turb = x-position of turbine.
        // Y_turb = y-position of turbine.
        public static void Calculate(ILArray<double> xTurb, ILArray<double> yTurb, double rotA, out ILArray<double> out_x, out ILArray<double> out_y)
        {
            ILArray<double> x_out = ILMath.zeros(1, xTurb.length()); // Initialization x-coordinates
            ILArray<double> y_out = ILMath.zeros(1, yTurb.length()); // Initialization y-coordinates
    
            rotA = rotA * ILMath.pi / 180; // Conversion to radians

	        for (var i = 1; i <= xTurb.length(); i++)
            {
		        x_out.SetValue(xTurb.GetValue(i - 1) * Math.Cos(rotA) - xTurb.GetValue(i - 1) * Math.Sin(rotA), i - 1);
		        y_out.SetValue(xTurb.GetValue(i - 1) * Math.Sin(rotA) + yTurb.GetValue(i - 1) * Math.Cos(rotA), i - 1);
            }

            if (ILMath.min(x_out) < 0) // Moves the x-points if these are negative.
            {
                x_out = x_out + 500 + ILMath.abs(ILMath.min(x_out));
            }

            if (ILMath.min(y_out) < 0) // Moves the y-points if these are negative.
            {
                y_out = y_out + 500 + ILMath.abs(ILMath.min(y_out));
            }
    
	        out_x = x_out;
	        out_y = y_out;
        }
    }
}
