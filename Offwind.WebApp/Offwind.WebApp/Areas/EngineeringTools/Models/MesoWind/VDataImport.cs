using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind
{
    public class VDataImport
    {
        public ObservableCollection<DatabaseItem> DatabaseItems { get; set; }
        public List<decimal> FreqByDirs { get; set; }
        public List<decimal[]> FreqByBins { get; set; }
        public List<decimal> MeanVelocityPerDir { get; set; }
        public List<HPoint> VelocityFreq { get; set; }

        public int NDirs { get; set; }
        public int NBins { get; set; }

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
