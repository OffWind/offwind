using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class WakeCalculation : MatlabCode
    {
        //private void wakeCalculation(ILArray<double> Ct, int i, ILCell wind, out ILArray<double> v_nac)
        public static void Calculate(out ILArray<double> v_nac, ILArray<double> Ct, int i, ILArray<double> wind)
        {
            //% v_nac = WAKECALCULATION(Ct,i,wind)
            //This function calculates the wake
            //Currently it is a very very simplified wake calculation. It just serves as
            //a placeholder for a correct wake calculation that will come later

            ILArray<double> scaling = ILMath.linspace(0.5, 0.9, length(Ct));
            v_nac = scaling * wind._(i, 2);
        }
    }
}
