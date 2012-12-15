using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldR
    {
        public decimal InternalField1 { get; set; }
        public decimal InternalField2 { get; set; }
        public decimal InternalField3 { get; set; }
        public decimal InternalField4 { get; set; }
        public decimal InternalField5 { get; set; }
        public decimal InternalField6 { get; set; }

        public PatchType BottomType { get; set; }

        public PatchType TopType { get; set; }

        public PatchType WestType { get; set; }
        public decimal WestValue1 { get; set; }
        public decimal WestValue2 { get; set; }
        public decimal WestValue3 { get; set; }
        public decimal WestValue4 { get; set; }
        public decimal WestValue5 { get; set; }
        public decimal WestValue6 { get; set; }

        public PatchType EastType { get; set; }

        public PatchType NorthType { get; set; }

        public PatchType SouthType { get; set; }
        public decimal SouthValue1 { get; set; }
        public decimal SouthValue2 { get; set; }
        public decimal SouthValue3 { get; set; }
        public decimal SouthValue4 { get; set; }
        public decimal SouthValue5 { get; set; }
        public decimal SouthValue6 { get; set; }
    }
}