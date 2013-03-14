using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldU : VWebPage
    {
        public VFieldU()
        {
            InternalField = new VFieldVectorValue();
            BottomValue = new VFieldVectorValue();
            WestParams = new VAtmBoundaryLayerInletVelocity();
            SouthParams = new VAtmBoundaryLayerInletVelocity();
        }

        public VFieldVectorValue InternalField { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType BottomType { get; set; }

        [DisplayName("value")]
        public VFieldVectorValue BottomValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType TopType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType WestType { get; set; }

        public VAtmBoundaryLayerInletVelocity WestParams { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType EastType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType NorthType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType SouthType { get; set; }

        public VAtmBoundaryLayerInletVelocity SouthParams { get; set; }
    }
}