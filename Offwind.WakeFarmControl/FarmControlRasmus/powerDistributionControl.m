function [P_ref, P_a]  = powerDistributionControl(v_nac,P_demand,parm)

%P_ref is a vector of power refenreces for tehe wind turbine with dimension 1xN
%v_nac is a vector of wind speed at each wind turbine with dimension 1xN
%P_demand is a scale of the wind farm power demand.
%parm is a struct of wind turbine parameters e.g. NREL5MW

rho     = parm.rho; %air density for each wind turbine(probably the same for all)
R       = parm.radius; %rotor radius for each wind turbine(NREL.r=63m)
rated   = parm.rated; %Rated power for each wind turbine(NREL.Prated=5MW)
Cp      = parm.Cp; % Max cp of the turbines for each wind turbine(NREL.Cp.max=0.45)

P_a     = zeros(parm.N,1);
P_ref   = zeros(parm.N,1);

% Compute available power at each turbine
for i=1:parm.N
    P_a(i)= min([rated(i),(pi/2)*rho*R(i)^2*v_nac(i)^3*Cp(i)]);
end

%Distribute power according to availibility
for i=1:parm.N
        if P_demand < sum(P_a)
            P_ref(i) = max([0, min([rated(i),P_demand*P_a(i)/sum(P_a)])]);
        else
            P_ref(i) = P_a(i);
        end
end