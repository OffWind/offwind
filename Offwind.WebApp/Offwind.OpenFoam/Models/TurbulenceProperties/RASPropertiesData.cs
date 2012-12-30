namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class RASPropertiesData
    {
        public string RasModelName { get; set; }
        public OnOffValue Turbulence { get; set; }
        public OnOffValue PrintCoeffs { get; set; }
    }
}