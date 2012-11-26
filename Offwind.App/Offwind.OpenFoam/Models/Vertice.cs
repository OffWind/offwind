namespace Offwind.Products.OpenFoam.Models
{
    public sealed class Vertice
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Z { get; set; }

        public Vertice()
        {
        }

        public Vertice(decimal x, decimal y, decimal z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
