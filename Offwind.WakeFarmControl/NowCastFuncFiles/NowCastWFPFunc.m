function NowCastWFPFunc(Data,TPredict,Method,r,Ts)
%Nowcasting with  model based on total wind farm power
%
% NowCastWFPFunc(Data,TPredict,Method,r,Ts)
%
% Data : Total wind farm power
% TPredict : Time for starting multi step prediction.  If TPredict<1 it
%            is assumed  a fraction of the end time (default 0.5)
% Method : 'AR(1)' or 'Persistence' only first letter count (default 'a')
% r : Decimation with a moving average of order r (default 1)
% Ts : Sampling time (default 0.1)
% 
% External input: None

% Time-stamp: <2014-10-17 14:09:40 tk>
% Version 1: Initial version
% Torben Knudsen
% Aalborg University, Dept. of Electronic Systems, Section of Automation
% and Control
% E-mail: tk@es.aau.dk

%% setting up inputs
TsDef= 0.1;
rDef= 1;
MethodDef= 'a';
TPredictDef= 0.5;
if nargin < 5; Ts= []; end;
if nargin < 4; r= []; end;
if nargin < 3; Method= []; end;
if nargin < 2; TPredict= []; end;
if nargin < 1; error('Error TK: To few input arguments'); end;
if isempty(r); r= rDef; end;
if isempty(TPredict); TPredict= TPredictDef; end;
if isempty(Ts); Ts= TsDef; end;
if isempty(Method); Method= MethodDef; end;

%% Parameters

IMT= 1;                                 % Include measurement time;
q0= 0;
TauLambdaLRel= 0.1;                     % TauLambdaL= 10% of the samples
Lambda0= 0.5;                           % Initial Lambda
TauLambdaInf= 600;                      % TauLambdaInf= 10min
TimeScaling= 1/3600;                    % From seconds to hours

%% Initialization

% Use Offwind simulation data made by Rasmus 2014-10-14
% The format is:
% [time sumPower sumRef sumAvai] as a matrix NS x 4.  Power i MW
% 48 WT are simulated.  Data for individual WT are also found e.g. in
% Power, P_ref, PA, beta etc
if strncmpi(Method,'a',1);
  Method= 'AR(1)';
else;
  Method= 'Persistence';
end;
TitleStr= ['Nowcasting with ' Method ' model based on total wind farm ' ...
           'power, Offwind simulation'];
Data= Data(:);
NS= size(Data,1);                        % Number of samples
NS= max(find(Data>0));         % Number of samples;
Data= Data(1:NS);                 % Limit the data
T= Ts*(1:NS)';
TimePlot= T*TimeScaling;                   % Time in hours
NWT= 48;
NomWFP= NWT*5e6*1e-6;               % Power in MW
PWF= Data;                     % Total Power in MW
if r>1;
  PWF= DecimateWMA(PWF,r);
  NS= size(PWF,1);                        % Number of samples
  Ts= Ts*r;
  T= Ts*(1:NS)';
  TimePlot= T*TimeScaling;                   % Time in hours
end;
MinWFP= 0;                            % For real WFs


%% Definitions etc.

% Calculate Lamba* from TauLambda* and dt
TauLambdaL= TauLambdaLRel*(T(end)-T(1));% TauLambdaL= 10% of the samples
LambdaL= exp(-Ts/TauLambdaL);
LambdaInf= exp(-Ts/TauLambdaInf);

%% Algorithm

% Initialization

% Prediction from time in TPredict
% if TPredict is a fraction calculate TPredict
if TPredict<1; 
  TPredict= round(TPredict*T(end)/Ts)*Ts;
end;
if TPredict < TauLambdaInf;
  warning(['TK: Prediction time is so small that the estimator/predictor ' ...
           'might not have converged yet']);
end;
TPS= min(find(T>=TPredict));            % Use time from measurements

% Multi step prediction
if strncmpi(Method,'a',1);
  % ARX1 version;
  % Recursive parameter estimation
  [Theta,Sigma,Xh,Lambda,Time]= RLSMARX1(PWF,1,LambdaInf);
  Res= [TimePlot(Time) PWF(Time) Xh Sigma Lambda];
  Res= [nan(Time(1)-1,size(Res,2)); Res];

  % Notice that length of Time is shorter than length of T if batch RLS
  % start is used so TPSEst < TPS in that case
  TPSEst= min(find(Time*Ts>=TPredict));      % Use time from estimates
  
  % Parameter values must be taken for index TPSEst
  A= Theta(TPSEst,1);
  if abs(A)>1;
    warning(['TK: Unstable pole, max(abs(eig)): ' num2str(abs(A))]);
  end;
  B= Theta(TPSEst,2);
  xhms= zeros(NS-TPS,1);
  covxhms= zeros(NS-TPS,1);
  xhms(1)= Xh(TPSEst+1);
  covxhms(1)= Sigma(TPSEst+1);
  aux= Sigma(TPSEst+1);
  for i= 2:NS-TPS;
    xhms(i)= A*xhms(i-1)+B;
    aux= A*aux*A';
    covxhms(i)= covxhms(i-1)+aux;
  end;

  % Prepend xhms with the present measurement so the plot clearly indicates
  % the time for the measurement
else;
  % Persistence version;
  % Initialization
  Lambda= Lambda0;
  q= q0;
  xh= PWF(1);
  Res= [T(1) PWF(1) xh q Lambda0];

  % Recursive estimation of incremental covariance
  for i= 2:NS;
    xt= PWF(i)-xh;
    dt= T(i)-T(i-1);
    q= Lambda*q+(1-Lambda)*xt^2/dt;
    Res= [Res;[T(i) PWF(i) xh q Lambda]];
    Lambda= LambdaL*Lambda+(1-LambdaL)*LambdaInf;
    xh= PWF(i);
  end;
  Res(:,1)= Res(:,1)*TimeScaling;

  % Persistence version;
  xhms= Res(TPS+1,3)*ones(NS-TPS,1);
  covxhms= Res(TPS+1,4)*Ts*(1:NS-TPS)';
end;

if IMT;
  xhms= [PWF(TPS); xhms];
  covxhms= [0; covxhms];
end;
sigmaxhms= sqrt(covxhms);
ConIntAWFP= xhms*[1 1 1]+sigmaxhms*[-2 0 2];
ConIntAWFP= min(max(ConIntAWFP,MinWFP),NomWFP); % Limit confidence limits

% Plot results
% Plot actual as black solid and lower, prediction and upper confidence
% limits as red, green and blue solid for 10 min, dashed for one hour and
% dotted for the rest.
figure;
NS10Min= min(NS-TPS-1,round(600/Ts));
NSOneHour= min(NS-TPS-1,round(3600/Ts));
set(gcf,'defaultaxescolororder',eye(3));
plot(TimePlot,Res(:,2),'k',...
     TimePlot(TPS+1-IMT:TPS+NS10Min),ConIntAWFP(1:NS10Min+IMT,:),...
     TimePlot(TPS+NS10Min+1:TPS+NSOneHour),...
     ConIntAWFP((NS10Min+1:NSOneHour)+IMT,:),'--',...
     TimePlot(TPS+NSOneHour+1:end),ConIntAWFP(NSOneHour+1+IMT:end,:),':');
title(TitleStr);
Legend= {'x' 'xhmsL' 'xhms' 'xhmsU'};
legend(Legend);
grid('on');
