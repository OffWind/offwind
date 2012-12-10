using System;
using System.Collections.Generic;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindWave.Computations
{
    public sealed class Calculator
    {
        private List<AdvancedCfd> _cfd;
        private List<PowerOutput> _out;

        public IEnumerable<AdvancedCfd> AdvancedCfdItems
        {
            get { return _cfd; }
        }

        public IEnumerable<PowerOutput> PowerOutput
        {
            get { return _out; }
        }

        public void Do(WindWaveInput input)
        {
            _cfd = new List<AdvancedCfd>();
            _out = new List<PowerOutput>();

            const double k = 0.4; 	//Von karman konstant
            const double pi = 3.14159265359; //pi
            double Ufriction = 0; 	//Friction velocity
            double uf; 			//Temporarily friction velocity
            double Zo;			//Aerodynamic roughness
            double Vhub;		//Turbine hub height velocity
            double P;			//Turbine power output
            double Area;		//Turbine swept area

            double Ug = input.Ug;			//Wind speed measured at the reference height 
            double Zg = input.Zg;			//Reference height
            double Zhub = input.Zhub;		//Turbine hub height
            double Td = input.Td;			//Turbine diameter
            double Ef = input.Ef;			//turbine efficiency
            double Cw = input.Cw;			//wave speed

            double residual = 1; Zo = 2E-04;
            while (residual > 1E-07)
            {
                uf = Ug * k / (Math.Log(Zg / Zo));
                Zo = 0.012 * Math.Pow(uf, 2) / 9.81;
                Ufriction = Ug * k / (Math.Log(Zg / Zo));
                residual = uf - Ufriction;
            }
            Vhub = Ufriction * (Math.Log(Zhub / Zo)) / k;
            Area = pi * Math.Pow(Td, 2) / 4;
            P = 0.5 * 1.225 * 1.91 * Math.Pow(Vhub, 3) * Ef * Area / 1E+08;

            var Ps = new double[5];
            var Vhubs = new double[5];
            var UfrictionS = new double[5];
            var Zos = new[] { 2E-04, 2E-04, 2E-04, 2E-04, 2E-04 };
            var A = new[] { 0.02, 0.02, 0.48, 1.89, 1.7 };
            var B = new[] { 0.5, 0.7, -1.0, -1.59, -1.7 };
            var arC = new string[5];
            arC[0] = "Toba     ";
            arC[1] = "Sugimori ";
            arC[2] = "Smith    ";
            arC[3] = "Johnson  ";
            arC[4] = "Drennan  ";

            _cfd.Add(new AdvancedCfd("Charnok", Ufriction, Zo));
            for (int n = 0; n < 5; n++)
            {
                double Error = 1;
                while (Error > 1E-07)
                {
                    uf = Ug * k / (Math.Log(Zg / Zos[n]));
                    Zos[n] = (A[n] * Math.Pow(uf, 2) / 9.81) * Math.Pow(Cw / uf, B[n]);
                    UfrictionS[n] = Ug * k / (Math.Log(Zg / Zos[n]));
                    Error = Math.Abs(uf - UfrictionS[n]);
                }
                Vhubs[n] = UfrictionS[n] * (Math.Log(Zhub / Zos[n])) / k;
                Ps[n] = (0.5 * 1.225 * 1.91 * Math.Pow(Vhubs[n], 3) * Ef * Area / 1E+08);
                _cfd.Add(new AdvancedCfd(arC[n], UfrictionS[n], Zos[n]));
            }

            _out.Add(new PowerOutput("Charnok", Vhub, P, 0));
            for (int m = 0; m < 5; m++)
            {
                var d = (Math.Abs(P - Ps[m]) / P) * 100;
                _out.Add(new PowerOutput(arC[m], Vhubs[m], Ps[m], d));
            }
        }
    }
}
