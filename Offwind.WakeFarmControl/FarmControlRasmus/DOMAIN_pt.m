% Wake Code - Matlab
% Rasmus Christensen
% Control and Automation, Aalborg University

function [output, ddx] = DOMAIN_pt(iMax,dTurb,xOrder,pppPoint)
    x    = zeros(1,length(xOrder)); % Initialization
    
	xMax = max(xOrder) + dTurb*pppPoint;
	xMin = min(xOrder) - 2*dTurb;
    
	x(1) = xMin;
	ddx  = (xMax - xMin)/(iMax-1);

	for i = 1:iMax-1
		x(i+1) = x(i)+ddx;
    end
    
	output = x;
end