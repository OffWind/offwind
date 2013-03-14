using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldR : VWebPage
    {
        public VFieldVectorValue2 InternalField { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType BottomType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType TopType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType WestType { get; set; }

        [DisplayName("value")]
        public VFieldVectorValue2 WestValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType EastType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType NorthType { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType SouthType { get; set; }

        [DisplayName("value")]
        public VFieldVectorValue2 SouthValue { get; set; }

        public VFieldR()
        {
            InternalField = new VFieldVectorValue2();
            WestValue = new VFieldVectorValue2();
            SouthValue = new VFieldVectorValue2();
        }
    }
}