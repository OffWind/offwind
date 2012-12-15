using Offwind.Products.OpenFoam.Models;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public struct VFieldScalarValue
    {
        private PatchValueType Type { get; set; }
        public decimal Value { get; set; }
    }
}