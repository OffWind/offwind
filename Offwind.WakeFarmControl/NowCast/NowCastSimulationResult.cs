using System;

namespace WakeFarmControl.NowCast
{
    public class NowCastSimulationResult
    {
        public string Method;
        public int[] Time;
        public double[] X;
        public double[][] XhmsAll;
        public int XhmsAllTimeOffset;
        public int XhmsLLength;
        public int XhmsUOffset;
    }
}
