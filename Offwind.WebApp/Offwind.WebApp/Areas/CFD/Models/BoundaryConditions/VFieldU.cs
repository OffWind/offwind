using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldU
    {
        public VFieldVectorValue InternalField { get; set; }

        [DisplayName("type")]
        public PatchType BottomType { get; set; }
        public VFieldVectorValue BottomValue { get; set; }

        [DisplayName("type")]
        public PatchType TopType { get; set; }

        [DisplayName("type")]
        public PatchType WestType { get; set; }
        public VAtmBoundaryLayerInletVelocity WestParams { get; set; }

        [DisplayName("type")]
        public PatchType EastType { get; set; }

        [DisplayName("type")]
        public PatchType NorthType { get; set; }

        [DisplayName("type")]
        public PatchType SouthType { get; set; }
        public VAtmBoundaryLayerInletVelocity SouthParams { get; set; }

        public VFieldU()
        {
            InternalField = new VFieldVectorValue();
            BottomValue = new VFieldVectorValue();
            WestParams = new VAtmBoundaryLayerInletVelocity();
            SouthParams = new VAtmBoundaryLayerInletVelocity();
        }
    }
}