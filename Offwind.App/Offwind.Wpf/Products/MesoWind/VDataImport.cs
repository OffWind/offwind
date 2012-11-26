using System.Collections.Generic;
using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;

namespace Offwind.Products.MesoWind
{
    public class VDataImport : BaseViewModel
    {
        public ObservableCollection<DatabaseItem> DatabaseItems { get; set; }
        public List<decimal> FreqByDirs { get; set; }
        public List<decimal[]> FreqByBins { get; set; }
        public List<decimal> MeanVelocityPerDir { get; set; }
        public List<HPoint> VelocityFreq { get; set; }

        public decimal CurrentLatitude
        {
            get { return GetProperty<decimal>("CurrentLatitude"); }
            set { SetProperty("CurrentLatitude", value); }
        }

        public decimal CurrentLongitude
        {
            get { return GetProperty<decimal>("CurrentLongitude"); }
            set { SetProperty("CurrentLongitude", value); }
        }

        public decimal FilterLatitude
        {
            get { return GetProperty<decimal>("FilterLatitude"); }
            set { SetProperty("FilterLatitude", value); }
        }

        public decimal FilterLongitude
        {
            get { return GetProperty<decimal>("FilterLongitude"); }
            set { SetProperty("FilterLongitude", value); }
        }

        public int NBins
        {
            get { return GetProperty<int>("NBins"); }
            set { SetProperty("NBins", value); }
        }

        public int NDirs
        {
            get { return GetProperty<int>("NDirs"); }
            set { SetProperty("NDirs", value); }
        }

        public VDataImport()
        {
            DatabaseItems = new ObservableCollection<DatabaseItem>();
            FreqByDirs = new List<decimal>();
            FreqByBins = new List<decimal[]>();
            MeanVelocityPerDir = new List<decimal>();
            VelocityFreq = new List<HPoint>();
        }
    }
}
