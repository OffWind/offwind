using System.ComponentModel;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VEpsilonWallFunction
    {
        [DisplayName("Cmu")]
        public decimal Cmu { get; set; }

        [DisplayName("kappa")]
        public decimal kappa { get; set; }

        [DisplayName("E")]
        public decimal E { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue value { get; set; }

        public VEpsilonWallFunction()
        {
            value = new VFieldScalarValue();
        }
    }
}