% Wake Code - Matlab
% Rasmus Christensen
% Control and Automation, Aalborg University

function [output] = Turb_centr_coord(nTurb, iMax, x, xTurb, gridRes)

    xxcTurb = zeros(1,nTurb);
    
	for i = 1:nTurb
		for ii = 1:iMax-1
			if abs(x(ii)) <= abs(xTurb(i)) && abs(xTurb(i)) < abs(x(ii+1))
				xxcTurb(i) = ii*sign(xTurb(i));
                break;
			end
		end
	end
	output = xxcTurb;
end