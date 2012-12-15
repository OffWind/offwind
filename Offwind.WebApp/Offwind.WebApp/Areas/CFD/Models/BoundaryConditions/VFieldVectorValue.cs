using Offwind.Products.OpenFoam.Models;

namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VFieldVectorValue
    {
        private PatchValueType Type { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public decimal Value3 { get; set; }
    }
}