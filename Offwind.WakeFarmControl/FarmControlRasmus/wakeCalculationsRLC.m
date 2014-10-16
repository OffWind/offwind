%% v_nac = WAKECALCULATION(Ct,i,wind)
% RLC, Aalborg
% The below is based on the .F90 code developed by ?, and will give a
% better estimate of the actual wake the individual turbines experience. 

function vNac = wakeCalculationsRLC(Ct, wField, vHub ,parm ,simParm)
    data  = parm.wf;
    dTurb = 2*parm.radius(1);
    nTurb = parm.N;
    rotA  = parm.rotA;
    kWake = parm.kWake;
    gridRes = simParm.gridRes; % Grid Resolution, the lower the number, the higher the amount of points computed.
    endSize = simParm.grid;
    
    if ~isreal(Ct) | Ct < 0
        disp('Ct is negative or complex');
    end
    
    x       = 1:gridRes:endSize;% x-grid.
    y       = 1:gridRes:endSize;% y-grid.
    gridX   = length(x); % Number of grid points.
    gridY   = length(y); % Number of grid points.
        
    xCoor   = data(:,1); % Coordiante of turbine, x-position
    yCoor   = data(:,2); % Coordinate of turbine, y-position
    
    [xTurb,yTurb]   = ROTATE_corrd(xCoor,yCoor,rotA); % Rotated (and scaled) coordinates
    [xOrder,yOrder] = WT_order(xTurb,yTurb); % Ordered turbines. 

    ppp = 2; % This parameter is also a bit weird.. But it changes the grid.
    [xGrid, ~] = DOMAIN_pt(gridX,dTurb,xOrder,ppp); % 
    ppp = 5;
    [yGrid, dy] = DOMAIN_pt(gridY,dTurb,yOrder,ppp);

    [xTurbC] = Turb_centr_coord(nTurb, gridX, xGrid, xOrder, gridRes); % Determines the grid point closest to the turbine.
    [yTurbC] = Turb_centr_coord(nTurb, gridY, yGrid, yOrder, gridRes); % Determines the grid point closest to the turbine. 

    % Velocity Computation
    [Velocity] = Compute_Vell(yOrder ,xTurbC ,yTurbC ,x ,wField, vHub ,kWake ,gridX ,gridY ,nTurb ,dTurb , Ct, dy);

    % Extracting the individual Nacelle Wind Speeds from the wind velocity matrix.
    %Velocity = Velocity';
    vNac = zeros(nTurb,1);
    
    for j = 1:length(xTurbC)
        vNac(j)    = Velocity(yTurbC(j),xTurbC(j));
    end
end

    