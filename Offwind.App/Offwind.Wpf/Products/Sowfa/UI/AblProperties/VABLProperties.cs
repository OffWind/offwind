using Offwind.Infrastructure.Models;

namespace Offwind.Products.Sowfa.UI.AblProperties
{
    public class VABLProperties : BaseViewModel
    {
        public string UWindSpeedPreview { get { return string.Format("UWindSpeed [0 1 -1 0 0 0 0] {0}", UWindSpeed); } }
        public string HWindPreview { get { return string.Format("hWind [0 1 0 0 0 0 0] {0}", HWind); } }

        public bool TurbineArrayOn
        {
            get { return GetProperty<bool>("TurbineArrayOn"); }
            set { SetProperty("TurbineArrayOn", value); }
        }


        public bool DriveWindOn
        {
            get { return GetProperty<bool>("DriveWindOn"); }
            set { SetProperty("DriveWindOn", value); }
        }


        public decimal UWindSpeed
        {
            get { return GetProperty<decimal>("UWindSpeed"); }
            set { SetProperty("UWindSpeed", value); }
        }



        public decimal UWindDir
        {
            get { return GetProperty<decimal>("UWindDir"); }
            set { SetProperty("UWindDir", value); }
        }


        public decimal HWind
        {
            get { return GetProperty<decimal>("HWind"); }
            set { SetProperty("HWind", value); }
        }


        public decimal Alpha
        {
            get { return GetProperty<decimal>("Alpha"); }
            set { SetProperty("Alpha", value); }
        }


        public string LowerBoundaryName
        {
            get { return GetProperty<string>("LowerBoundaryName"); }
            set { SetProperty("LowerBoundaryName", value); }
        }


        public string UpperBoundaryName
        {
            get { return GetProperty<string>("UpperBoundaryName"); }
            set { SetProperty("UpperBoundaryName", value); }
        }


        public decimal MeanAvgStartTime
        {
            get { return GetProperty<decimal>("MeanAvgStartTime"); }
            set { SetProperty("MeanAvgStartTime", value); }
        }


        public decimal CorrAvgStartTime
        {
            get { return GetProperty<decimal>("CorrAvgStartTime"); }
            set { SetProperty("CorrAvgStartTime", value); }
        }


        public bool StatisticsOn
        {
            get { return GetProperty<bool>("StatisticsOn"); }
            set { SetProperty("StatisticsOn", value); }
        }


        public decimal StatisticsFrequency
        {
            get { return GetProperty<decimal>("StatisticsFrequency"); }
            set { SetProperty("StatisticsFrequency", value); }
        }


    }
}
