using System;

namespace WakeFarmControlR
{
    public sealed class FarmControl
    {
        public static double[][] Simulation(WakeFarmControlConfig config)
        {
            return TranslatedCode.FarmControl(config);
        }
    }
}
