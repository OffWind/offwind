
namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class LESPropertiesData
    {
        public string LESModel { set; get; }
        public string Delta { set; get; }
        public OnOffValue Turbulence { get; set; }
        public OnOffValue PrintCoeffs { get; set; }
    }
}
