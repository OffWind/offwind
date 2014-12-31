using System;
using ILNumerics;

namespace WakeFarmControl.NowCast
{
    internal partial class TranslatedCode
    {
        private const double UDef = 1;
        private const double lambdaDef = 0.99;
        private const double theta0Def = 0;
        private const double sigma0Def = 0;
        private const int PlotDef = 0;

        private static void RLSMARX1(out ILArray<double> Theta, out ILArray<double> Sigma, out ILArray<double> Xh, out ILArray<double> Lambda, out ILArray<int> Time, out ILArray<double> AByTime, out ILArray<double> BByTime, out ILArray<double> SigmaByTime, ILArray<double> X, ILArray<double> U = null, ILArray<double> lambda = null, ILArray<double> theta0 = null, ILArray<double> sigma0 = null, int Plot = PlotDef)
        {
            #region "Original function comments"
            //RLS calculates recursive least squares parameter estimates corresponding
            // to the output input data X U and the model x(n+1)= A*x(n)+B*u(n)+e(n)
            // <=> x(n+1)'= [x(n)' u(n)']*[A'; B']+e(n)
            // Duble exponential forgetting can be used where lamba increases
            // exponentielly from lamba0 to lambda.
            //
            // [Theta,Sigma,Xh,Lambda,Time]= RLSMARX1(X,U,lambda,theta0,sigma0)
            //
            // X: Output Matrix with size number of samples * number of channels
            // U: Input Matrix with size number of samples * number of channels
            //    zero scalar/matrix corresponds to no input, ones corresponds to estimating
            //    a mean value parameter. (default ones)
            // lambda : Forgetting factor (default 0.99) if lambda= [lambda lambda0]
            //          the forgetting factor increases exponentially from lambda0 
            //          to lambda.
            // theta0 : Initial parameter estimate. Notice that theta0= [A0';B0'];
            // sigma0 : Initial covvariance estimate.  
            //          If no initial parameters are specifyed a offline/batch
            //          estimate is used for the start.
            // Plot   : If 1/true plot informative plots (default: false)
            //
            // External input: 

            // Time-stamp: <2014-10-17 11:44:27 tk>
            // Version 1: 2014-10-01 app.
            // Version 2: 2014-10-02 12:53:07 tk Included LS/offline/batch startup
            // Version 3: 2014-10-07 13:57:54 tk Included additional output and
            //            plotting
            // Torben Knudsen
            // Aalborg University, Dept. of Electronic Systems, Section of Automation
            // and Control
            // E-mail: tk@es.aau.dk
            #endregion

            #region "Used variables declaration"
            double TauLambdaLRel;
            double lambdaInf;
            double lambda0;
            double IniNumSampFrac;
            int N;
            int n;
            int m;
            double TauLambdaL;
            double lambdaL;
            ILArray<double> sigma;
            ILArray<double> theta;
            int NIni;
            ILArray<double> XX;
            ILArray<double> YY;
            ILArray<double> R;
            ILArray<double> Epsilon;
            ILArray<double> xh;
            ILArray<double> Res;
            int i;
            ILArray<double> phi;
            ILArray<double> epsilon;
            #endregion

            //% setting up inputs
            //UDef = 1;
            //lambdaDef = 0.99;
            //theta0Def = 0;
            //sigma0Def = 0;
            //PlotDef = 0;
            //if (nargin < 6) { Plot = []; }
            if (sigma0 == null) { sigma0 = ILMath.empty(); }
            if (theta0 == null) { theta0 = ILMath.empty(); }
            if (lambda == null) { lambda = ILMath.empty(); }
            if (U == null) { U = ILMath.empty(); }
            //if (nargin < 1) { error('Error TK: To few input arguments'); }
            if (isempty(U)) { U = UDef; }
            //if (isempty(Plot)) { Plot = PlotDef; }
            if (isempty(lambda)) { lambda = lambdaDef; }
            if (isempty(theta0)) { theta0 = theta0Def; }
            if (isempty(sigma0)) { sigma0 = sigma0Def; }

            //% Parameters

            TauLambdaLRel = 0.1;                     // TauLambdaL= 10% of the samples
            lambdaInf = lambda.GetValue(1 - 1);
            if (length(lambda) > 1)                    // Initial lambda;
            {
                lambda0 = lambda.GetValue(2 - 1);
            }
            else
            {
                lambda0 = lambdaInf;                           
            }
            IniNumSampFrac = 0.1;                    // Number of samples to use for init

            //% Definitions etc.

            N = ILMath.size(X, 1 - 1);
            n = ILMath.size(X, 2 - 1);
            m = ILMath.size(U, 2 - 1);
            //throw new ApplicationException(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", X.Dimensions, N, n, U.Dimensions, m));
            // Calculate Lamba* from TauLambda* assuming Ts= 1
            TauLambdaL = TauLambdaLRel * N;
            lambdaL = exp(-1 / TauLambdaL);
            if (all(U == 1))
            {
                U = ILMath.ones(N, 1);
            }

            //% Algorithm

            // Initialisation
            if (all(all(sigma0 == 0)))                 // No sigma0 specifyed
            {
                sigma = 0 * ILMath.eye(n, n);
            }
            else
            {
                sigma = sigma0;
            }
            if (all(all(theta0 == 0)))                 // No sigma0 specifyed
            {
                theta = ILMath.zeros(n + m, n);
            }
            else
            {
                theta = theta0;
            }

            // Start with a offline estimate if initial parameters are not specifyed
            if (all(_[ theta0, ';', sigma0 ] == 0))
            {
                NIni = Math.Max(n + 1, (int)(Math.Ceiling(N * IniNumSampFrac)));
                XX = _[ X[ILMath.r(1 - 1, NIni - 1 - 1), ILMath.full], U[ILMath.r(1 - 1, NIni - 1 - 1), ILMath.full] ];
                YY = X[_a(1, NIni - 1) + 1, ILMath.full];
                R = ILMath.multiply(XX.T, XX);
                ILArray<double> R_ = ILMath.empty();
                ILMath.invert(R, R_);
                theta = ILMath.multiply(ILMath.multiply(R_, (XX.T)), YY);
                Epsilon = YY - ILMath.multiply(XX, theta);
                sigma = ILMath.multiply(Epsilon.T, Epsilon) / NIni;
                xh = ILMath.multiply(XX[ILMath.end, ILMath.full], theta);
            }
            else
            {
                NIni = 1;
                xh = X[1 - 1, ILMath.full];
                R = 1e6 * ILMath.eye(n, n);
            }

            lambda = lambda0;
            Res = _[ NIni, (theta[ILMath.full]).T, (sigma[ILMath.full]).T, xh, lambda ];

            AByTime = ILMath.zeros(n, n, N - NIni + 1);
            AByTime[ILMath.full, ILMath.full, 1 - 1] = (theta[ILMath.r(1 - 1, n - 1), ILMath.r(1 - 1, n - 1)]).T;
            BByTime = ILMath.zeros(n, m, N - NIni + 1);
            BByTime[ILMath.full, ILMath.full, 1 - 1] = (theta[ILMath.r(n + 1 - 1, n + m - 1), ILMath.r(1 - 1, n - 1)]).T;
            SigmaByTime = ILMath.zeros(n, n, N - NIni + 1);
            SigmaByTime[ILMath.full, ILMath.full, 1 - 1] = sigma;

            // Recursion
            // Model x(n+1)= A*x(n)+B*u(n)+e(n) <=>
            // x(n+1)'= [x(n)' u(n)']*[A'; B']+e(n)
            for (i = NIni + 1; i <= N; i++)
            {
                phi = (_[ X[i - 1 - 1, ILMath.full], U[ i - 1 - 1, ILMath.full] ]).T;           // phi(i)
                //throw new ApplicationException(string.Format("{0}\t{1}\t{2}\t{3}", lambda.Dimensions, R.Dimensions, phi.Dimensions, (phi.T).Dimensions));
                R = ((double)lambda) * R + ILMath.multiply(phi, (phi.T));                 // R(i)
                xh = ILMath.multiply((phi.T), theta);                       // xh(i)
                epsilon = X[i - 1, ILMath.full] - xh;                   // epsilon(i)
                theta = theta + ILMath.multiply(ILMath.linsolve(R, phi), epsilon);           // theta(i)
                sigma = ILMath.multiply(lambda, sigma) + ILMath.multiply(ILMath.multiply((1 - lambda), (epsilon.T)), epsilon);
                //throw new ApplicationException(string.Format("{0}\t{1}\t{2}\t{3}\t{4}", Res.Dimensions, theta[ILMath.full].T.Dimensions, sigma[ILMath.full].T.Dimensions, xh.Dimensions, lambda.Dimensions));
                Res = _[ Res, ';', _[ i, theta[ILMath.full].T, sigma[ILMath.full].T, xh, lambda ] ];
                lambda = lambdaL * lambda + (1 - lambdaL) * lambdaInf;
                AByTime[ILMath.full, ILMath.full, i - NIni + 1 - 1] = (theta[ILMath.r(1 - 1, n - 1), ILMath.r(1 - 1, n - 1)]).T; // Index start at 2
                BByTime[ILMath.full, ILMath.full, i - NIni + 1 - 1] = (theta[ILMath.r(n + 1 - 1, n + m - 1), ILMath.r(1 - 1, n - 1)]).T;
                SigmaByTime[ILMath.full, ILMath.full, i - NIni + 1 - 1] = sigma;
            }
            // Res is organised like 
            // i theta(:)' sigma(:)' xh lambda with the dimensions
            // 1 (n+m)*n   n^2       n   1
            Time = ILMath.toint32(Res[ILMath.full, 1 - 1]);
            Theta = Res[ILMath.full, ILMath.r(1 + 1 - 1, 1 + (n + m) * n - 1)];
            Sigma = Res[ILMath.full, ILMath.r(1 + 1 + (n + m) * n - 1, 1 + (n + m) * n + _p(n, 2) - 1)];
            Xh = Res[ILMath.full, ILMath.r(1 + 1 + (n + m) * n + _p(n, 2) - 1, 1 + (n + m) * n + _p(n, 2) + n - 1)];
            Lambda = Res[ILMath.full, ILMath.r(1 + 1 + (n + m) * n + _p(n, 2) + n - 1, ILMath.end)];

            // Do informative plotting and output
            //if (Plot)
            //{
            //    figure;
            //    plot([U X]);
            //    title('Input and measurements');
            //    figure;
            //    plot(Time,reshape(AByTime,n*n,N-NIni+1)');
            //    title('A parameters');
            //    figure;
            //    plot(Time,reshape(BByTime,n*m,N-NIni+1)');
            //    title('B parameters');
            //    figure;
            //    plot(Time,reshape(SigmaByTime,n*n,N-NIni+1)');
            //    title('Sigma parameters');
            //    figure;
            //    plot(Time,Lambda);
            //    title('Lambda');
            //    Epsilon= X(Time,:)-Xh;
            //    Epsilon= Epsilon(100:end,:);
            //    figure;
            //    XCorrtkAll(Epsilon);
            //    figure;
            //    normplot(Epsilon);
            //    gridtk('on',figures);
            //    Lab= ['Final parameters A n*n B n*m Sigma n*n'];
            //    RowLab= {''};
            //    ColLab= {''};
            //    Res= [AByTime(:,:,end) BByTime(:,:,end) SigmaByTime(:,:,end)];
            //    printmattk(Res,Lab);
            //}
        }
    }
}
