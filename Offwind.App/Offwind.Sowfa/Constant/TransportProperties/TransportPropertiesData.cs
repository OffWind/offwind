namespace Offwind.Sowfa.Constant.TransportProperties
{
    public sealed class TransportPropertiesData
    {
        public TransportModel transportModel { get; set; }
        public decimal nu { get; set; }
        public decimal TRef { get; set; }
        public LesModel LESModel { get; set; }
        public decimal Cs { get; set; }
        public decimal deltaLESCoeff { get; set; }
        public decimal kappa { get; set; }
        public decimal betaM { get; set; }
        public decimal gammM { get; set; }
        public decimal z0 { get; set; }
        public decimal q0 { get; set; }
        public SurfaceStressModel surfaceStressModel { get; set; }
        public decimal betaSurfaceStress { get; set; }
    }
}
