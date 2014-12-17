using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class WakeCalculation
    {
        //private void wakeCalculation(ILArray<double> Ct, int i, ILCell wind, out ILArray<double> v_nac)
        public static void Calculate(ILArray<double> Ct, int i, ILMatFile wind, out ILArray<double> v_nac)
        {
            //% v_nac = WAKECALCULATION(Ct,i,wind)
            //This function calculates the wake
            //Currently it is a very very simplified wake calculation. It just serves as
            //a placeholder for a correct wake calculation that will come later

            ILArray<double> scaling = ILMath.linspace(0.5, 0.9, Ct.Length);
            v_nac = scaling * wind.GetArray<double>("wind").GetValue(i - 1, 1);
        }
    }
}
