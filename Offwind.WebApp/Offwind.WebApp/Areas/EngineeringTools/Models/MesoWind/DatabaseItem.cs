namespace Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind
{
    public class DatabaseItem 
    {
        public int Id { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public double Distance { get; set; }
        public string Database { get; set; }
    }
}