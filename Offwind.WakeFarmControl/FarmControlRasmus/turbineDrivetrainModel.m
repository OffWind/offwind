function [OmegaOut, Ct, Cp] = turbineDrivetrainModel(x,u,wt,env,timeStep)
% Parameters
R   = wt.rotor.radius;
I   = wt.rotor.inertia;

% Definitons etc.
Omega   = x(1);
Ve      = x(2);
Beta    = u(1);
Tg      = u(2);

% Algorithm
if Ve == 0;
    Lambda  = 25;
else
    Lambda = Omega*R/Ve;
end

Cp     = interpTable(Beta,Lambda,wt.cp.table,wt.cp.beta,wt.cp.tsr,false);
Ct     = interpTable(Beta,Lambda,wt.ct.table,wt.ct.beta,wt.ct.tsr,false);

if Ct > 1;
    Ct = 1;
end

Tr     = 1/2*env.rho*pi*(R^2)*(Ve^3)*Ct/Omega;

OmegaOut  = Omega + timeStep*(Tr-Tg)/I; %Integration method: Forward Euler