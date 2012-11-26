using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.Constant.AblProperties
{
    public sealed class AblPropertiesData
    {
        public bool turbineArrayOn { get; set; }
        public bool driveWindOn { get; set; }
        public DimensionedValue UWindSpeedDim { get; set; }
        public decimal UWindDir { get; set; }
        public DimensionedValue HWindDim { get; set; }
        public decimal alpha { get; set; }
        public string lowerBoundaryName { get; set; }
        public string upperBoundaryName { get; set; }
        public decimal meanAvgStartTime { get; set; }
        public decimal corrAvgStartTime { get; set; }
        public bool statisticsOn { get; set; }
        public decimal statisticsFrequency { get; set; }

        public AblPropertiesData()
        {
            UWindSpeedDim = new DimensionedValue();
            HWindDim = new DimensionedValue();
        }
    }
}
