using System;
using Offwind.Sowfa.Constant.TransportProperties;

namespace Offwind.WebApp.Areas.CFD.Models.Preprocessing
{
    public class VTransportProperties
    {
        public TransportModel TransportModel { get; set; }
        public Decimal MolecularViscosity { get; set; }
        public Decimal TRef { get; set; }
        public LesModel LESModel { get; set; }
        public Decimal SmagorinskyConstant { get; set; }
        public Decimal DeltaLESCoeff { get; set; }
        public Decimal VonKarmanConstant { get; set; }
        public Decimal BetaM { get; set; }
        public Decimal GammM { get; set; }
        public Decimal RoughnessHeight { get; set; }
        public Decimal SurfaceTemperatureFlux { get; set; }
        public SurfaceStressModel SurfaceStressModel { get; set; }
        public Decimal BetaSurfaceStress { get; set; }
    }
}