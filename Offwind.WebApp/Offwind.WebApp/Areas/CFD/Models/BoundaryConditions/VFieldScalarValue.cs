using System.ComponentModel;
using Offwind.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public struct VFieldScalarValue
    {
        [DisplayName("type")]
        public PatchValueType Type { get; set; }

        [DisplayName("value")]
        public decimal Value { get; set; }
    }
}