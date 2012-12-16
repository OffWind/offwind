using System.Collections.Generic;

namespace Offwind.OpenFoam.Models.Fields
{
    public class PatchValueScalar
    {
        public PatchValueType Type { get; set; }
        public decimal Value { get; set; }
        public List<decimal> Array { get; set; }
    }
}
