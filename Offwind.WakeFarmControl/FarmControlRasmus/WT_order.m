% Wake Code - Matlab
% Rasmus Christensen
% Control and Automation, Aalborg University

function [xOrder,yOrder] = WT_order(xTurb,yTurb)
    sorted = sortrows([xTurb',yTurb'], 1);
    turbineOrder = zeros(length(sorted),2);
    sortCtr = 0;
    for i = 1:length(sorted)
        for j = i+1:length(sorted)
            if sorted(i,1) == sorted(j,1)
                sortCtr = sortCtr + 1;
            end
        end
        turbineOrder(i:i+sortCtr,:) = sortrows(sorted(i:i+sortCtr,:), 2);
        sortCtr = 0;
    end
    xOrder = turbineOrder(:,1);
    yOrder = turbineOrder(:,2);
end