function interpValue = interpTable(Beta,Lambda,table,turbineTableBeta,turbineTableLambda,negYes)
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
% DESCRIPTION:
% RLC - 8/9/2014, interpolation, for getting an accurate CP/CT value. The
% reason for a standalone script, is that the built in MATLAB script
% (interp) includes a lot of redundancy checks that are not necessary, and
% not feasible for embedded solutions. 
% Based on the NREL5MW Turbine.
% Uses Linear Polynomial Extrapolation, to get a value for CP, given a Beta
% (Revolutional speed) and Lambda (Tip-Speed-Ratio) of the Turbine. 
% The interpolation is computed as:
% y = y0 + (y1 - y0)*(x-x0)/(x1-x0)
%
% Beta is the revolutional entry.
% Lambda is the TSR entry ratio. 
% turbineTable defines the table to lookup in. 
% negYes defines wether the CP value should be allowed to be negative.
%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

%% Setup, loads the table, and stores it.
%persistent turbineTable tableLoad

%if isempty(tableLoad)
    turbineTable        = table;
%    tableLoad           = 1;
%end

[sizeBt, sizeLa] = size(turbineTable);

%% Index Interpolation
% Finds the beta-point of the interpolation.
[~,Bt0] = min(abs(turbineTableBeta-Beta));  % Determines the index of the closest point.

if Beta > turbineTableBeta(Bt0)
    if Bt0 == sizeBt; %length(turbineTableBeta)
        Bt1 = Bt0;
        Bt0 = Bt0 - 1;
    else
        Bt1 = Bt0 + 1;
    end
else
    if Bt0 == 1;
        Bt1 = Bt0 + 1;
    else
        Bt1 = Bt0;
        Bt0 = Bt1 - 1;
    end
end


% Finds the Lambda-point of the interpolation.
[~,La0] = min(abs(turbineTableLambda-Lambda)); % Determines the index of the closest point.

if Lambda > turbineTableLambda(La0)
    if La0 == sizeLa; %length(turbineTableLambda)
        La1 = La0;
        La0 = La1 - 1;
    else
        La1 = La0 + 1;
    end
else
    if La0 == 1;
        La1 = La0 + 1;
    else
        La1 = La0;
        La0 = La1 - 1;
    end
end
%% Table Interpolation
% Table lookup using indexes obtained previously:
tableLookup     = [turbineTable(Bt0,La0), turbineTable(Bt0,La1);
                   turbineTable(Bt1,La0), turbineTable(Bt1,La1)];

% Interpolating, using the Lambda values first, then the Betas.
lambdaIntervals = [((tableLookup(1,2)-tableLookup(1,1))/(turbineTableLambda(La1)-turbineTableLambda(La0))) * (Lambda-turbineTableLambda(La0)) + tableLookup(1,1);
                   ((tableLookup(2,2)-tableLookup(2,1))/(turbineTableLambda(La1)-turbineTableLambda(La0))) * (Lambda-turbineTableLambda(La0)) + tableLookup(1,2)];


% Interpolation, using the Beta values (using the intervales computed above).
betaIntervals   = ((lambdaIntervals(2)-lambdaIntervals(1))/(turbineTableBeta(Bt1)-turbineTableBeta(Bt0)))*(Beta-turbineTableBeta(Bt0))+lambdaIntervals(1);

%% Negativity Handling
% If the negYes value is set as true in the system, the functino will only
% give an absolute value of Ct and Cp - have to be modified so it does this
% correctly.

if (negYes)
    interpValue = 0;
else
    interpValue = betaIntervals;
end