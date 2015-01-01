using System;
using System.Linq;
using ILNumerics;
using MatlabInterpreter;

namespace WakeFarmControl.NowCast
{
    public sealed class NowCast
    {
        private static ILArray<double> ILArrayFromArray(double[][] array)
        {
            int dim1 = array.GetLength(0);
            int dim2 = (dim1 >= 1 ? array[0].Length : 0);
            ILArray<double> ilArray = (dim1 == 0 || dim2 == 0 ? ILMath.empty(dim1, dim2) : ILMath.zeros(dim1, dim2));
            for (int i = 0; i <= dim1 - 1; i++)
            {
                for (int j = 0; j <= dim2 - 1; j++)
                {
                    ilArray.SetValue(array[i][j], i, j);
                }
            }

            return ilArray;
        }

        public static NowCastSimulationResult Simulation(double[][] wakeFarmControlDataOut, NowCastConfig config)
        {
            ILArray<double> Data = ILArrayFromArray(wakeFarmControlDataOut);
            string outMethod;
            ILArray<double> outTime;
            ILArray<double> outX;
            ILArray<double> outXhmsAll;
            int outXhmsAllTimeOffset;
            int outXhmsLLength;
            int outXhmsUOffset;
            TranslatedCode.NowCastWFPFunc(out outMethod, out outTime, out outX, out outXhmsAll, out outXhmsAllTimeOffset, out outXhmsLLength, out outXhmsUOffset, Data, config.TPredict, config.Method, config.r, config.Ts);
            NowCastSimulationResult nowCastSimulationResult = new NowCastSimulationResult();
            nowCastSimulationResult.Method = outMethod;
            nowCastSimulationResult.Time = outTime.ToArray();
            nowCastSimulationResult.X = outX.ToArray();
            nowCastSimulationResult.XhmsAll = outXhmsAll.ToDoubleArray();
            nowCastSimulationResult.XhmsAllTimeOffset = outXhmsAllTimeOffset;
            nowCastSimulationResult.XhmsLLength = outXhmsLLength;
            nowCastSimulationResult.XhmsUOffset = outXhmsUOffset;
            return nowCastSimulationResult;
        }
    }
}
