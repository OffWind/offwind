namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation
{
    public class VTurbine
    {
        public decimal X { get; set; }

        public decimal Y { get; set; }

        public VTurbine()
        {
        }

        public VTurbine(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
    }
}