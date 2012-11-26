using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Offwind.Infrastructure.Models;

namespace Offwind.Products.MesoWind
{
    public delegate void ProductTargetEventHandler(ProductTargets target);

    public class VMesoWind : BaseViewModel
    {
        public event ProductTargetEventHandler TargetNotified;
        public ObservableCollection<decimal> FreqByDirs { get; set; }
        public ObservableCollection<decimal[]> FreqByBins { get; set; }
        public ObservableCollection<decimal> MeanVelocityPerDir { get; set; }
        public ObservableCollection<HPoint> VelocityFreq { get; set; }

        public void NotifyTargets(ProductTargets target)
        {
            if (TargetNotified != null)
            {
                try
                {
                    TargetNotified(target);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }

        public VMesoWind()
        {
            FreqByDirs = new ObservableCollection<decimal>();
            FreqByBins = new ObservableCollection<decimal[]>();
            VelocityFreq = new ObservableCollection<HPoint>();
            MeanVelocityPerDir = new ObservableCollection<decimal>();
        }

        public decimal Latitude
        {
            get { return GetProperty<decimal>("Latitude"); }
            set { SetProperty("Latitude", value); }
        }

        public decimal Longitude
        {
            get { return GetProperty<decimal>("Longitude"); }
            set { SetProperty("Longitude", value); }
        }

        public int NDirs
        {
            get { return GetProperty<int>("NDirs"); }
            set { SetProperty("NDirs", value); }
        }

        public int NBins
        {
            get { return GetProperty<int>("NBins"); }
            set { SetProperty("NBins", value); }
        }
    }
}
