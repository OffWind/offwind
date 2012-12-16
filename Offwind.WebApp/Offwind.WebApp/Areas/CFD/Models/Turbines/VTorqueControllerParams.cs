using System.ComponentModel;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public class VTorqueControllerParams
    {
        [DisplayName("CutInGenSpeed (rpm)")]
        public decimal CutInGenSpeed { get; set; }

        [DisplayName("RatedGenSpeed (rpm)")]
        public decimal RatedGenSpeed { get; set; }

        [DisplayName("Region2StartGenSpeed (rpm)")]
        public decimal Region2StartGenSpeed { get; set; }

        [DisplayName("Region2EndGenSpeed (rpm)")]
        public decimal Region2EndGenSpeed { get; set; }

        [DisplayName("CutInGenTorque (N-m)")]
        public decimal CutInGenTorque { get; set; }

        [DisplayName("RatedGenTorque (N-m)")]
        public decimal RatedGenTorque { get; set; }

        [DisplayName("RateLimitGenTorque (N-m/s)")]
        public decimal RateLimitGenTorque { get; set; }

        [DisplayName("KGen")]
        public decimal KGen { get; set; }

        [DisplayName("TorqueControllerRelax")]
        public decimal TorqueControllerRelax { get; set; }
    }
}