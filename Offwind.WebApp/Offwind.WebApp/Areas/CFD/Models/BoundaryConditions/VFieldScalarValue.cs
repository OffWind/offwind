using System.ComponentModel;
using Offwind.OpenFoam.Models.Fields;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public struct VFieldScalarValue
    {
        public PatchValueType Type { get; set; }
        public decimal Value { get; set; }
    }
}