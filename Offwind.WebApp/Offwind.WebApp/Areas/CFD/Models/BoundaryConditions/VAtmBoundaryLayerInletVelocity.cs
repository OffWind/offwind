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
        public VVertice N { get; set; }

        [DisplayName("z")]
        public VVertice Z { get; set; }

        [DisplayName("z0")]
        public VFieldScalarValue Z0 { get; set; }

        [DisplayName("value")]
        public VFieldVectorValue Value { get; set; }

        [DisplayName("zGround")]
        public VFieldScalarValue ZGround { get; set; }

        public VAtmBoundaryLayerInletVelocity()
        {
            Value = new VFieldVectorValue();
            N = new VVertice();
            Z = new VVertice();
            Z0 = new VFieldScalarValue();
            ZGround = new VFieldScalarValue();
        }
    }
}