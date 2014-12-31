using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeFarmControl.NowCast
{
    public sealed class NowCastConfig
    {
        //ILArray<double> Data;
        public double TPredict = TranslatedCode.TPredictDef;
        public string Method = TranslatedCode.MethodDef;
        public int r = TranslatedCode.rDef;
        public double Ts = TranslatedCode.TsDef;
    }
}
