using System;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControl.NowCast
{
    internal partial class TranslatedCode : MatlabCode
    {
        internal const double TsDef = 0.1;
        internal const int rDef = 1;
        internal const string MethodDef = "a";
        internal const double TPredictDef = 0.5;

        internal static void NowCastWFPFunc(out string outMethod, out ILArray<double> outTime, out ILArray<double> outX, out ILArray<double> outXhmsAll, out int outXhmsAllTimeOffset, out int outXhmsLLength, out int outXhmsUOffset, ILArray<double> Data, double TPredict = TPredictDef, string Method = MethodDef, int r = rDef, double Ts = TsDef)
        {
            #region "Original function comments"
            //Nowcasting with  model based on total wind farm power
            //
            // NowCastWFPFunc(Data,TPredict,Method,r,Ts)
            //
            // Data : Total wind farm power
            // TPredict : Time for starting multi step prediction.  If TPredict<1 it
            //            is assumed  a fraction of the end time (default 0.5)
            // Method : 'AR(1)' or 'Persistence' only first letter count (default 'a')
            // r : Decimation with a moving average of order r (default 1)
            // Ts : Sampling time (default 0.1)
            // 
            // External input: None

            // Time-stamp: <2014-10-17 14:09:40 tk>
            // Version 1: Initial version
            // Torben Knudsen
            // Aalborg University, Dept. of Electronic Systems, Section of Automation
            // and Control
            // E-mail: tk@es.aau.dk
            #endregion

            #region "Used variables declaration"
            int IMT;
            double q0;
            double TauLambdaLRel;
            double Lambda0;
            double TauLambdaInf;
            double TimeScaling;
            //string TitleStr;
            int NS;
            ILArray<double> T;
            ILArray<double> TimePlot;
            double NWT;
            double NomWFP;
            ILArray<double> PWF;
            double MinWFP;
            double TauLambdaL;
            double LambdaL;
            double LambdaInf;
            int TPS;
            ILArray<double> Theta;
            ILArray<double> Sigma;
            ILArray<double> Xh;
            ILArray<double> Lambda;
            ILArray<int> Time;
            ILArray<double> Res;
            int TPSEst;
            double A;
            double B;
            ILArray<double> xhms;
            ILArray<double> covxhms;
            double aux;
            int i;
            ILArray<double> q;
            double xh;
            double xt;
            double dt;
            ILArray<double> sigmaxhms;
            ILArray<double> ConIntAWFP;
            int NS10Min;
            int NSOneHour;
            #endregion

            //% setting up inputs
            //TsDef = 0.1;
            //rDef = 1;
            //MethodDef = "a";
            //TPredictDef = 0.5;
            //if nargin < 5; Ts= []; end;
            //if nargin < 4; r= []; end;
            //if nargin < 3; Method= []; end;
            //if nargin < 2; TPredict= []; end;
            //if nargin < 1; error('Error TK: To few input arguments'); end;
            //if isempty(r); r= rDef; end;
            //if isempty(TPredict); TPredict= TPredictDef; end;
            //if isempty(Ts); Ts= TsDef; end;
            //if isempty(Method); Method= MethodDef; end;

            //% Parameters

            IMT = 1;                                    // Include measurement time;
            q0 = 0;
            TauLambdaLRel = 0.1;                        // TauLambdaL= 10% of the samples
            Lambda0 = 0.5;                              // Initial Lambda
            TauLambdaInf= 600;                          // TauLambdaInf= 10min
            TimeScaling = 1.0 / 3600;                   // From seconds to hours

            //% Initialization

            // Use Offwind simulation data made by Rasmus 2014-10-14
            // The format is:
            // [time sumPower sumRef sumAvai] as a matrix NS x 4.  Power i MW
            // 48 WT are simulated.  Data for individual WT are also found e.g. in
            // Power, P_ref, PA, beta etc
            if (strncmpi(Method, "a", 1))
            {
                Method = "AR(1)";
            }
            else
            {
                Method = "Persistence";
            }
            //TitleStr = "Nowcasting with " + Method + " model based on total wind farm " //...
            //            + "power, Offwind simulation";
            Data = Data[_(':')];
            NS = size(Data, 1);                        // Number of samples
            NS = max_(find(Data > 0));         // Number of samples;
            Data = Data[_(1, ':', NS)];                 // Limit the data
            T = Ts * (_c(1, NS)).T;
            TimePlot = T * TimeScaling;                   // Time in hours
            NWT = 48;
            NomWFP = NWT * 5e6 * 1e-6;               // Power in MW
            PWF = Data;                     // Total Power in MW
            if (r > 1)
            {
                DecimateWMA(out PWF, out _ILArray_double, PWF, r);
                NS = size(PWF, 1);                        // Number of samples
                Ts = Ts * r;
                T = Ts * (_c(1, NS)).T;
                TimePlot = T * TimeScaling;                   // Time in hours
            }
            MinWFP = 0;                            // For real WFs


            //% Definitions etc.

            // Calculate Lamba* from TauLambda* and dt
            TauLambdaL = TauLambdaLRel * (T._(end) - T._(1));// TauLambdaL= 10% of the samples
            LambdaL = exp(-Ts / TauLambdaL);
            LambdaInf = exp(-Ts / TauLambdaInf);

            //% Algorithm

            // Initialization

            // Prediction from time in TPredict
            // if TPredict is a fraction calculate TPredict
            if (TPredict < 1)
            {
                TPredict = round(TPredict * T._(end) / Ts) * Ts;
            }
            if (TPredict < TauLambdaInf)
            {
                warning(__[ "TK: Prediction time is so small that the estimator/predictor ", //...
                            "might not have converged yet" ]);
            }
            TPS = min_(find(T >= TPredict));            // Use time from measurements

            // Multi step prediction
            if (strncmpi(Method, "a", 1))
            {
                // ARX1 version;
                // Recursive parameter estimation
                RLSMARX1(out Theta, out Sigma, out Xh, out Lambda, out Time, out _ILArray_double, out _ILArray_double, out _ILArray_double, PWF, 1, LambdaInf);
                Res = __[ TimePlot[_(Time)], PWF[_(Time)], Xh, Sigma, Lambda ];
                Res = __[ nan(Time._(1) - 1, size(Res, 2)), ';', Res ];

                // Notice that length of Time is shorter than length of T if batch RLS
                // start is used so TPSEst < TPS in that case
                TPSEst = min_(find(_dbl(Time) * Ts >= TPredict));      // Use time from estimates
  
                // Parameter values must be taken for index TPSEst
                A = Theta._(TPSEst, 1);
                if (abs(A) > 1)
                {
                    warning(__[ "TK: Unstable pole, max(abs(eig)): ", num2str(abs(A)) ]);
                }
                B = Theta._(TPSEst, 2);
                xhms = zeros(NS - TPS, 1);
                covxhms = zeros(NS - TPS, 1);
                xhms._(1, '=', Xh._(TPSEst + 1));
                covxhms._(1, '=', Sigma._(TPSEst + 1));
                aux = Sigma._(TPSEst + 1);
                for (i = 2; i <= NS - TPS; i++)
                {
                    xhms._(i, '=', A * xhms._(i - 1) + B);
                    aux = A * aux * A.T();
                    covxhms._(i, '=', covxhms._(i - 1) + aux);
                }

                // Prepend xhms with the present measurement so the plot clearly indicates
                // the time for the measurement
            }
            else
            {
                // Persistence version;
                // Initialization
                Lambda = Lambda0;
                q = q0;
                xh = PWF._(1);
                Res = __[ T._(1), PWF._(1), xh, q, Lambda0 ];

                // Recursive estimation of incremental covariance
                for (i = 2; i <= NS; i++)
                {
                    xt = PWF._(i) - xh;
                    dt = T._(i) - T._(i - 1);
                    q = _m(Lambda, '*', q) + (1 - Lambda) * _p(xt, 2) / dt;
                    Res = __[ Res, ';', __[ T._(i), PWF._(i), xh, q, Lambda ] ];
                    Lambda = LambdaL * Lambda + (1 - LambdaL) * LambdaInf;
                    xh = PWF._(i);
                }
                Res[_(':'), _(1)] = Res[_(':'), _(1)] * TimeScaling;

                // Persistence version;
                xhms = Res._(TPS + 1, 3) * ones(NS - TPS, 1);
                covxhms = Res._(TPS + 1, 4) * Ts * ((_c(1, NS - TPS)).T);
            }

            if (IMT != 0)
            {
                xhms = __[ PWF._(TPS), ';', xhms ];
                covxhms = __[ 0, ';', covxhms ];
            }
            sigmaxhms = sqrt(covxhms);
            ConIntAWFP = _m(xhms, '*', __[ 1, 1, 1 ]) + _m(sigmaxhms, '*', __[ -2, 0, 2 ]);
            ConIntAWFP = min(max(ConIntAWFP, MinWFP), NomWFP); // Limit confidence limits

            // Plot results
            // Plot actual as black solid and lower, prediction and upper confidence
            // limits as red, green and blue solid for 10 min, dashed for one hour and
            // dotted for the rest.
            //figure;
            NS10Min = min(NS - TPS - 1, round_(600.0 / Ts));
            NSOneHour = min(NS - TPS - 1, round_(3600.0 / Ts));
            //set(gcf, "defaultaxescolororder", ILMath.eye(3, 3));
            outMethod = Method;
            outTime = TimePlot;
            outX = Res[_(':'), _(2)];
            outXhmsAll = ConIntAWFP;
            outXhmsAllTimeOffset = (TPS - IMT);
            outXhmsLLength = (NS10Min + IMT);
            outXhmsUOffset = (NSOneHour + IMT);
            //plot(
            //    TimePlot, Res[ILMath.full, 2 - 1], 'k',///...
            //    TimePlot[ILMath.r(TPS + 1 - IMT - 1, TPS + NS10Min - 1)], ConIntAWFP[ILMath.r(1 - 1, NS10Min + IMT - 1), ILMath.full],//...
            //    TimePlot[ILMath.r(TPS + NS10Min + 1 - 1, TPS + NSOneHour - 1)], ConIntAWFP[_a(NS10Min + 1, 1, NSOneHour) + IMT - 1, ILMath.full], "--",//...
            //    TimePlot[ILMath.r(TPS + NSOneHour + 1 - 1, ILMath.end)], ConIntAWFP[ILMath.r(NSOneHour + 1 + IMT - 1, ILMath.end), ILMath.full], ':');
            //title(TitleStr);
            //Legend= {'x' 'xhmsL' 'xhms' 'xhmsU'};
            //legend(Legend);
            //grid('on');
        }
    }
}
