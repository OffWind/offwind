using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class WindTurbineParameters
    {
        public ILArray<double> wf;
        public double rotA;
        public double kWake;

        public int N;
        public double rho;
        public ILArray<double> radius;
        public ILArray<double> rated;
        public double ratedSpeed;
        public ILArray<double> Cp;
        public ILArray<double> Ct;
    }
}
