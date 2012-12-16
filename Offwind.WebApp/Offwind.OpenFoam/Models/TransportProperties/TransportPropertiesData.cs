namespace Offwind.Sowfa.Constant.TransportProperties
{
    public sealed class TransportPropertiesData
    {
        public TransportModel transportModel { get; set; }
        public decimal MolecularViscosity { get; set; }

        // SOWFA Transport Properties
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


        /// OffwindSolver Transport Properties
        public decimal CplcNu0 { get; set; }
        public decimal CplcNuInf { get; set; }
        public decimal CplcM { get; set; } 
        public decimal CplcN { get; set; }

        public decimal BccNu0 { get; set; }
        public decimal BccNuInf { get; set; }
        public decimal BccM { get; set; }
        public decimal BccN { get; set; }

    }
}
