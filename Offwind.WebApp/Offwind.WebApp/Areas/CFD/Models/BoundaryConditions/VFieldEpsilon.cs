using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldEpsilon
    {
        public decimal InternalField { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType BottomType { get; set; }
        public VEpsilonWallFunction BottomValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType TopType { get; set; }
        public VEpsilonWallFunction TopValue { get; set; }

        [DisplayName("type")]
        [ReadOnly(true)]
        public PatchType WestType { get; set; }

        [DisplayName("value")]
        public VFieldScalarValue WestValue { get; set; }

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
        public VFieldScalarValue SouthValue { get; set; }

        public VFieldEpsilon()
        {
            BottomValue = new VEpsilonWallFunction();
            TopValue = new VEpsilonWallFunction();
        }
    }
}