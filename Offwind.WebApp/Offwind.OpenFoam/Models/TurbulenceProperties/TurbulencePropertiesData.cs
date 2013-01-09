using Offwind.OpenFoam.Models.TurbulenceModels;

namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class TurbulencePropertiesData
    {
        public TurbulenceModel SimulationType { get; set; }
        public RASPropertiesData RasProperties { set; get; }
        public LESPropertiesData LesProperties { set; get; }
    }
}
