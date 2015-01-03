using System;

namespace WakeFarmControlR
{
    public sealed class FarmControl
    {
        public static double[][] Simulation(WakeFarmControlConfig config, out double[][] dataOut)
        {
            return TranslatedCode.FarmControl(config, out dataOut);
        }
    }
}
