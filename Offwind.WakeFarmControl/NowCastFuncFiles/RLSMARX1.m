function [Theta,Sigma,Xh,Lambda,Time,AByTime,BByTime,SigmaByTime]= RLSMARX1(X,U,lambda,theta0,sigma0,Plot)
%RLS calculates recursive least squares parameter estimates corresponding
% to the output input data X U and the model x(n+1)= A*x(n)+B*u(n)+e(n)
% <=> x(n+1)'= [x(n)' u(n)']*[A'; B']+e(n)
% Duble exponential forgetting can be used where lamba increases
% exponentielly from lamba0 to lambda.
%
% [Theta,Sigma,Xh,Lambda,Time]= RLSMARX1(X,U,lambda,theta0,sigma0)
%
% X: Output Matrix with size number of samples * number of channels
% U: Input Matrix with size number of samples * number of channels
%    zero scalar/matrix corresponds to no input, ones corresponds to estimating
%    a mean value parameter. (default ones)
% lambda : Forgetting factor (default 0.99) if lambda= [lambda lambda0]
%          the forgetting factor increases exponentially from lambda0 
%          to lambda.
% theta0 : Initial parameter estimate. Notice that theta0= [A0';B0'];
% sigma0 : Initial covvariance estimate.  
%          If no initial parameters are specifyed a offline/batch
%          estimate is used for the start.
% Plot   : If 1/true plot informative plots (default: false)
%
% External input: 

% Time-stamp: <2014-10-17 11:44:27 tk>
% Version 1: 2014-10-01 app.
% Version 2: 2014-10-02 12:53:07 tk Included LS/offline/batch startup
% Version 3: 2014-10-07 13:57:54 tk Included additional output and
%            plotting
% Torben Knudsen
% Aalborg University, Dept. of Electronic Systems, Section of Automation
% and Control
% E-mail: tk@es.aau.dk

%% setting up inputs
UDef= 1;
lambdaDef= 0.99;
theta0Def= 0;
sigma0Def= 0;
PlotDef= 0;
if nargin < 6; Plot= []; end;
if nargin < 5; sigma0= []; end;
if nargin < 4; theta0= []; end;
if nargin < 3; lambda= []; end;
if nargin < 2; U= []; end;
if nargin < 1; error('Error TK: To few input arguments'); end;
if isempty(U); U= UDef; end;
if isempty(Plot); Plot= PlotDef; end;
if isempty(lambda); lambda= lambdaDef; end;
if isempty(theta0); theta0= theta0Def; end;
if isempty(sigma0); sigma0= sigma0Def; end;

%% Parameters

TauLambdaLRel= 0.1;                     % TauLambdaL= 10% of the samples
lambdaInf= lambda(1);
if length(lambda)>1;                    % Initial lambda;
  lambda0= lambda(2);
else;
  lambda0= lambdaInf;                           
end;
IniNumSampFrac= 0.1;                    % Number of samples to use for init

%% Definitions etc.

N= size(X,1);
n= size(X,2);
m= size(U,2);
% Calculate Lamba* from TauLambda* assuming Ts= 1
TauLambdaL= TauLambdaLRel*N;
lambdaL= exp(-1/TauLambdaL);
if all(U==1);
  U= ones(N,1);
end;

%% Algorithm

% Initialisation
if all(all(sigma0==0));                 % No sigma0 specifyed
  sigma= 0*eye(n);
else;
  sigma= sigma0;
end;
if all(all(theta0==0));                 % No sigma0 specifyed
  theta= zeros(n+m,n);
else;
  theta= theta0;
end;

% Start with a offline estimate if initial parameters are not specifyed
if all([theta0; sigma0]==0);
  NIni= max(n+1,ceil(N*IniNumSampFrac));
  XX= [X(1:NIni-1,:) U(1:NIni-1,:)];
  YY= X((1:NIni-1)+1,:);
  R= XX'*XX;
  theta= inv(R)*XX'*YY;
  Epsilon= YY-XX*theta;
  sigma= Epsilon'*Epsilon/NIni;
  xh= XX(end,:)*theta;
else
  NIni= 1;
  xh= X(1,:);
  R= 1e6*eye(n);
end;

lambda= lambda0;
Res= [NIni theta(:)' sigma(:)' xh lambda];

AByTime= zeros(n,n,N-NIni+1);
AByTime(:,:,1)= theta(1:n,1:n)';
BByTime= zeros(n,m,N-NIni+1);
BByTime(:,:,1)= theta(n+1:n+m,1:n)';
SigmaByTime= zeros(n,n,N-NIni+1);
SigmaByTime(:,:,1)= sigma;

% Recursion
% Model x(n+1)= A*x(n)+B*u(n)+e(n) <=>
% x(n+1)'= [x(n)' u(n)']*[A'; B']+e(n)
for i= NIni+1:N;
  phi= [X(i-1,:) U(i-1,:)]';           % phi(i)
  R= lambda*R+phi*phi';                 % R(i)
  xh= phi'*theta;                       % xh(i)
  epsilon= X(i,:)-xh;                   % epsilon(i)
  theta= theta+R\phi*epsilon;           % theta(i)
  sigma= lambda*sigma+(1-lambda)*epsilon'*epsilon;
  Res= [Res;[i theta(:)' sigma(:)' xh lambda]];
  lambda= lambdaL*lambda+(1-lambdaL)*lambdaInf;
  AByTime(:,:,i-NIni+1)= theta(1:n,1:n)'; % Index start at 2
  BByTime(:,:,i-NIni+1)= theta(n+1:n+m,1:n)';
  SigmaByTime(:,:,i-NIni+1)= sigma;
end;
% Res is organised like 
% i theta(:)' sigma(:)' xh lambda with the dimensions
% 1 (n+m)*n   n^2       n   1
Time= Res(:,1);
Theta= Res(:,1+1:1+(n+m)*n);
Sigma= Res(:,1+1+(n+m)*n:1+(n+m)*n+n^2);
Xh= Res(:,1+1+(n+m)*n+n^2:1+(n+m)*n+n^2+n);
Lambda= Res(:,1+1+(n+m)*n+n^2+n:end);

% Do informative plotting and output
if Plot;
  figure;
  plot([U X]);
  title('Input and measurements');
  figure;
  plot(Time,reshape(AByTime,n*n,N-NIni+1)');
  title('A parameters');
  figure;
  plot(Time,reshape(BByTime,n*m,N-NIni+1)');
  title('B parameters');
  figure;
  plot(Time,reshape(SigmaByTime,n*n,N-NIni+1)');
  title('Sigma parameters');
  figure;
  plot(Time,Lambda);
  title('Lambda');
  Epsilon= X(Time,:)-Xh;
  Epsilon= Epsilon(100:end,:);
  figure;
  XCorrtkAll(Epsilon);
  figure;
  normplot(Epsilon);
  gridtk('on',figures);
  Lab= ['Final parameters A n*n B n*m Sigma n*n'];
  RowLab= {''};
  ColLab= {''};
  Res= [AByTime(:,:,end) BByTime(:,:,end) SigmaByTime(:,:,end)];
  printmattk(Res,Lab);
end;
