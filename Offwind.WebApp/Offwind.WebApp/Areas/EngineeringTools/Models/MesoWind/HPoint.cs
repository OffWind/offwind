namespace MvcApplication1.Areas.EngineeringTools.Models.MesoWind
{
    public class HPoint
    {
        public int Dir { get; set; }
        public decimal Velocity { get; set; }
        public decimal Frequency { get; set; }

        public HPoint()
        {
        }

        public HPoint(int dir, decimal v, decimal f)
        {
            Dir = dir;
            Velocity = v;
            Frequency = f;
        }
    }
}