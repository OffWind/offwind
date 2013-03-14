using System;
using System.ComponentModel;
using Offwind.OpenFoam.Models;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.AirfoilAndTurbulence
{
    public sealed class VLESProperties
    {
        public String LESModel { set; get; }
        public String Delta { set; get; }
        public OnOffValue Turbulence { get; set; }
        public OnOffValue PrintCoeffs { get; set; }
    }

    public sealed class VRASProperties
    {
        [DisplayName("RAS turbulence model")]
        public String RasModelName { get; set; }
        [DisplayName("Turbulence modelling on/off")]
        public OnOffValue Turbulence { get; set; }
        [DisplayName("Print model coeffs")]
        public OnOffValue PrintCoeffs { get; set; }
    }

    public class VTurbulenceProperties : VWebPage
    {
        [DisplayName("Turbulence model")]
        [ReadOnly(true)]
        public TurbulenceModel SimulationType { get; set; }
        public VRASProperties RASProperties { get; set; }
        public VLESProperties LESProperties { get; set; }

        public VTurbulenceProperties()
        {
            SimulationType = TurbulenceModel.RASModel;
            RASProperties = new VRASProperties();
            LESProperties = new VLESProperties();
        }
    }
}
