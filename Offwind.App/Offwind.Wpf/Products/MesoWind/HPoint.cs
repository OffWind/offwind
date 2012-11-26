namespace Offwind.Products.MesoWind
{
    public class HPoint
    {
        public decimal Velocity { get; set; }
        public decimal Frequency { get; set; }

        public HPoint()
        {
        }

        public HPoint(decimal v, decimal f)
        {
            Velocity = v;
            Frequency = f;
        }
    }
}