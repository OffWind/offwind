using System;
using System.Collections.Generic;

namespace WakeFarmControlR
{
    public sealed class FarmControl
    {
        public static double[][] Simulation(WakeFarmControlConfig config, out double[][] dataOut, out List<string> informationMessages)
        {
            var result = TranslatedCode.FarmControl(config, out dataOut);
            informationMessages = new List<string>();
            return result;
        }
    }
}
