using System;

namespace WakeFarmControl.NowCast
{
    public class NowCastSimulationResult
    {
        public string Method;
        public double[] Time;
        public double[] X;
        public double[][] XhmsAll;
        public int XhmsAllTimeOffset;
        public int XhmsLLength;
        public int XhmsUOffset;
    }
}
