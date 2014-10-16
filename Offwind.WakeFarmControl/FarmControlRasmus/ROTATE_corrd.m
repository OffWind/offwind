% Wake Code - Matlab
% Rasmus Christensen
% Control and Automation, Aalborg University

% N_turb = number of turbines.
% X_turb = x-position of turbine.
% Y_turb = y-position of turbine.

function [out_x, out_y] = ROTATE_corrd(xTurb,yTurb,rotA)
    x_out = zeros(1,length(xTurb)); % Initialization x-coordinates
    y_out = zeros(1,length(yTurb)); % Initialization y-coordinates
    
    rotA = rotA*pi/180; % Conversion to radians

	for i = 1:length(xTurb)
		x_out(i) = xTurb(i)*cos(rotA) - xTurb(i)*sin(rotA);
		y_out(i) = xTurb(i)*sin(rotA) + yTurb(i)*cos(rotA);
    end

    if min(x_out) < 0 % Moves the x-points if these are negative.
        x_out = x_out + 500 + abs(min(x_out));
    end
    
    if min(y_out) < 0 % Moves the y-points if these are negative.
        y_out = y_out + 500 + abs(min(y_out));
    end
    
	out_x = x_out;
	out_y = y_out;
end