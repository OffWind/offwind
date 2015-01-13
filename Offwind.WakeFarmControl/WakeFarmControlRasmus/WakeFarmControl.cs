using System;
using System.Collections.Generic;

namespace WakeFarmControlR
{
    public sealed class FarmControl
    {
        public static double[][] Simulation(WakeFarmControlConfig config, out double[][] dataOut, out List<string> informationMessages)
        {
            TranslatedCode.Timers.ClearTimers();
            TranslatedCode.Timers.StartTimer("#0_FarmControl");
            var result = TranslatedCode.FarmControl(config, out dataOut);
            TranslatedCode.Timers.StopTimer("#0_FarmControl");
            informationMessages = new List<string>();
            informationMessages.Add(TranslatedCode.Timers.MeasuredTime);
            return result;
        }
    }
}
