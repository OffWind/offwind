using System.ComponentModel;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VAtmBoundaryLayerInletVelocity
    {
        [DisplayName("Uref")]
        public decimal Uref { get; set; }

        [DisplayName("Href")]
        public decimal Href { get; set; }

        [DisplayName("n")]
        public decimal N { get; set; }

        [DisplayName("z")]
        public decimal Z { get; set; }

        [DisplayName("z0")]
        public decimal Z0 { get; set; }

        [DisplayName("value")]
        public VFieldVectorValue Value { get; set; }

        [DisplayName("Uref")]
        public decimal ZGround { get; set; }

        public VAtmBoundaryLayerInletVelocity()
        {
            Value = new VFieldVectorValue();
        }
    }
}