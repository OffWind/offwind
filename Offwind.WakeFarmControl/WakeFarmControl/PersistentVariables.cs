using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ILNumerics;

namespace WakeFarmControl
{
    public sealed class PersistentVariables
    {
        public ILArray<double> Table;
        public ILArray<double> Betavec;
        public ILArray<double> Lambdavec;
    }
}
