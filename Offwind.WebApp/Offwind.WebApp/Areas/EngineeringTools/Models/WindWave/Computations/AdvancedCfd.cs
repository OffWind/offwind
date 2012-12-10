namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindWave.Computations
{
    public sealed class AdvancedCfd
    {
        public string Method { get; set; }
        public double FrictionVelocity { get; set; }
        public double RoughnessHeight { get; set; }

        public AdvancedCfd()
        {
        }

        public AdvancedCfd(string m, double fv, double rh)
        {
            Method = m;
            FrictionVelocity = fv;
            RoughnessHeight = rh;
        }
    }
}
