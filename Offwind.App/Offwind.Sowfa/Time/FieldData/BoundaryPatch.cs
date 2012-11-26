using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.Sowfa.Time.FieldData
{
    public sealed class BoundaryPatch
    {
        public string Name { get; set; }
        public string Rho { get; set; }
        public PatchType PatchType { get; set; }
        public FieldType GradientFieldType { get; set; }
        public FieldType ValueFieldType { get; set; }
        public decimal[] GradientValue { get; set; }
        public decimal[] ValueValue { get; set; }

        public BoundaryPatch()
        {
            GradientValue = new decimal[1];
            ValueValue = new decimal[1];
        }
    }
}
