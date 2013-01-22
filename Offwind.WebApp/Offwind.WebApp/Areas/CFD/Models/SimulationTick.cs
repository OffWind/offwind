namespace Offwind.WebApp.Areas.CFD.Models
{
    public sealed class SimulationTick
    {
        public double time { set; get; }
        public double epsilon { set; get; }
        public double k { set; get; }
        public double p { set; get; }
        public double Ux { set; get; }
        public double Uy { set; get; }
        public double Uz { set; get; }
    }
}
