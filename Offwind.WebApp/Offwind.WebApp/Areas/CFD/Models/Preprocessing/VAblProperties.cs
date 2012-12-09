namespace Offwind.WebApp.Areas.CFD.Models.Preprocessing
{
    public class VAblProperties
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }

        public int GridX { get; set; }
        public int GridY { get; set; }
        public int GridZ { get; set; }
    }
}