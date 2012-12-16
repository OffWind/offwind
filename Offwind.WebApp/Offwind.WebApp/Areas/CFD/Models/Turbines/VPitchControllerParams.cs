using System.ComponentModel;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public class VPitchControllerParams
    {
        [DisplayName("PitchControlStartPitch")]
        public decimal PitchControlStartPitch { get; set; }

        [DisplayName("PitchControlEndPitch")]
        public decimal PitchControlEndPitch { get; set; }

        [DisplayName("PitchControlStartSpeed")]
        public decimal PitchControlStartSpeed { get; set; }

        [DisplayName("PitchControlEndSpeed")]
        public decimal PitchControlEndSpeed { get; set; }

        [DisplayName("RateLimitPitch")]
        public decimal RateLimitPitch { get; set; }
    }
}