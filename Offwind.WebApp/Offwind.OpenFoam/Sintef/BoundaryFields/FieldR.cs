using System.Collections.Generic;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldR
    {
        public List<decimal> InternalField { get; set; }

        public PatchType BottomType { get; set; }
        public PatchValueScalar BottomValue { get; set; }

        public PatchType TopType { get; set; }
        public PatchValueScalar TopValue { get; set; }

        public PatchType WestType { get; set; }
        public PatchValueScalar WestValue { get; set; }

        public PatchType EastType { get; set; }
        public PatchValueScalar EastValue { get; set; }

        public PatchType NorthType { get; set; }
        public PatchValueScalar NorthValue { get; set; }

        public PatchType SouthType { get; set; }
        public PatchValueScalar SouthValue { get; set; }

        public FieldR()
        {
            BottomValue = new PatchValueScalar();
            TopValue = new PatchValueScalar();
            WestValue = new PatchValueScalar();
            EastValue = new PatchValueScalar();
            NorthValue = new PatchValueScalar();
            SouthValue = new PatchValueScalar();
        }
    }
}
