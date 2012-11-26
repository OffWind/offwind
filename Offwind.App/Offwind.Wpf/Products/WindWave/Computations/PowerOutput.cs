namespace Offwind.Products.WindWave.Computations
{
    public sealed class PowerOutput
    {
        public string Method { get; set; }
        public double Velocity { get; set; }
        public double Output { get; set; }
        public double Differences { get; set; }

        public PowerOutput(string m, double v, double o, double od)
        {
            Method = m;
            Velocity = v;
            Output = o;
            Differences = od;
        }
    }
}
