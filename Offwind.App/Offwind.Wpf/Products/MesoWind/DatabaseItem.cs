using Offwind.Infrastructure.Models;

namespace Offwind.Products.MesoWind
{
    public class DatabaseItem : BaseViewModel
    {
        public decimal Longitude
        {
            get { return GetProperty<decimal>("Longitude"); }
            set { SetProperty("Longitude", value); }
        }

        public decimal Latitude
        {
            get { return GetProperty<decimal>("Latitude"); }
            set { SetProperty("Latitude", value); }
        }

        public double Distance
        {
            get { return GetProperty<double>("Distance"); }
            set { SetProperty("Distance", value); }
        }

        public string FileName
        {
            get { return GetProperty<string>("FileName"); }
            set { SetProperty("FileName", value); }
        }
    }
}
