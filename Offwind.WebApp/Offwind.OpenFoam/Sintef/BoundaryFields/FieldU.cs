using System.Collections.Generic;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class AtmBoundaryLayerInletVelocity
    {
        public decimal Uref { get; set; }
        public decimal Href { get; set; }
        public Vertice n { get; set; }
        public Vertice z { get; set; }
        public PatchValueScalar z0 { get; set; }
        public PatchValueVector Value { get; set; }
        public PatchValueScalar zGround { get; set; }
        public AtmBoundaryLayerInletVelocity()
        {
            Value = new PatchValueVector();
        }
    }

    public class FieldU
    {
        public PatchValueVector InternalField { get; set; }

        public PatchType BottomType { get; set; }
        public AtmBoundaryLayerInletVelocity BottomValue { get; set; }

        public PatchType TopType { get; set; }

        public PatchType WestType { get; set; }
        public AtmBoundaryLayerInletVelocity WestValue { get; set; }
        

        public PatchType EastType { get; set; }

        public PatchType NorthType { get; set; }

        public PatchType SouthType { get; set; }
        public AtmBoundaryLayerInletVelocity SouthValue { get; set; }

        public FieldU()
        {
            InternalField = new PatchValueVector();
            BottomValue = new AtmBoundaryLayerInletVelocity();
            WestValue = new AtmBoundaryLayerInletVelocity();
            SouthValue = new AtmBoundaryLayerInletVelocity();
        }

    }
}
