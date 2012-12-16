using System.Collections.Generic;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldR
    {
        public PatchValueVector2 InternalField { get; set; }

        public PatchType BottomType { get; set; }
        //public PatchValueScalar BottomValue { get; set; }

        public PatchType TopType { get; set; }
        //public PatchValueScalar TopValue { get; set; }

        public PatchType WestType { get; set; }
        public PatchValueVector2 WestValue { get; set; }

        public PatchType EastType { get; set; }
        //public PatchValueScalar EastValue { get; set; }

        public PatchType NorthType { get; set; }
        //public PatchValueScalar NorthValue { get; set; }

        public PatchType SouthType { get; set; }
        public PatchValueVector2 SouthValue { get; set; }

        public FieldR()
        {
            InternalField = new PatchValueVector2();
            //BottomValue = new PatchValueScalar();
            //TopValue = new PatchValueScalar();
            WestValue = new PatchValueVector2();
            //EastValue = new PatchValueScalar();
            //NorthValue = new PatchValueScalar();
            SouthValue = new PatchValueVector2();
        }
    }
}
