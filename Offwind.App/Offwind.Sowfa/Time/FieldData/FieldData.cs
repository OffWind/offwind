using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.Sowfa.Time.FieldData
{
    public class FieldData
    {
        public Format FieldFormat { get; set; }
        public FieldClass FieldClass { get; set; }
        public string FieldLocation { get; set; }
        public string FieldObject { get; set; }
        public List<BoundaryPatch> Patches { get; set; } 
        public Dimensions Dimensions { get; set; }
        public FieldType InternalFieldType { get; set; }
        public decimal[] InternalFieldValue { get; set; }

        public FieldData()
        {
            Dimensions = new Dimensions();
            Patches = new List<BoundaryPatch>();
        }
    }
}
