using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldK
    {
        public decimal InternalField { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType BottomType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue BottomValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType TopType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue TopValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType WestType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue WestValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType EastType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue EastValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType NorthType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue NorthValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType SouthType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue SouthValue { get; set; }
    }
}