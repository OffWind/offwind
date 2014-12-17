using ILNumerics;

namespace WakeFarmControlR
{
    public sealed class Turb_centr_coord
    {
        // Wake Code - Matlab
        // Rasmus Christensen
        // Control and Automation, Aalborg University
        public static void Calculate(int nTurb, int iMax, ILArray<double> x, ILArray<double> xTurb, int gridRes, out ILArray<int> output)
        {
            ILArray<int> xxcTurb = ILMath.zeros<int>(1, nTurb);

            for (var i = 1; i <= nTurb; i++)
            {
                for (var ii = 1; ii <= iMax - 1; ii++)
                {
                    if (ILMath.abs(x.GetValue(ii - 1)) <= ILMath.abs(xTurb.GetValue(i - 1)) && ILMath.abs(xTurb.GetValue(i - 1)) < ILMath.abs(x.GetValue(ii)))
                    {
                        xxcTurb.SetValue(ii * ((int)(ILMath.sign(xTurb.GetValue(i - 1)))), i - 1);
                        break;
                    }
                }
            }
            output = xxcTurb;
        }
    }
}
