namespace Offwind.WebApp.Areas.CFD.Models.BoundaryConditions
{
    public class VAtmBoundaryLayerInletVelocity
    {
        public decimal Uref { get; set; }
        public decimal Href { get; set; }
        public decimal n { get; set; }
        public decimal z { get; set; }
        public decimal z0 { get; set; }
        public VFieldVectorValue Value { get; set; }
        public decimal zGround { get; set; }
    }
}