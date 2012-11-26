using Offwind.Infrastructure.Models;
using Offwind.Sowfa.Constant.TransportProperties;

namespace Offwind.Products.OpenFoam.UI.TransportProperties
{
    public class VTransportProperties : BaseViewModel
    {
        public string MolecularViscosityPreview { get { return string.Format("nu [0 2 -1 0 0 0 0] {0}", MolecularViscosity); } }
        public string TRefPreview { get { return string.Format("TRef [0 0 0 1 0 0 0] {0}", TRef); } }
        public string RoughnessHeightPreview { get { return string.Format("z0 [0 1 0 0 0 0 0] {0}", RoughnessHeight); } }
        public string SurfaceTemperatureFluxPreview { get { return string.Format("q0 [0 1 -1 1 0 0 0] {0}", SurfaceTemperatureFlux); } }

        public TransportModel TransportModel
        {
            get { return GetProperty<TransportModel>("TransportModel"); }
            set
            {
                SetPropertyEnum("TransportModel", value);
            }
        }

        public decimal MolecularViscosity
        {
            get { return GetProperty<decimal>("MolecularViscosity"); }
            set
            {
                SetProperty("MolecularViscosity", value);
                NotifyPropertyChanged("MolecularViscosityPreview");
            }
        }

        public decimal TRef
        {
            get { return GetProperty<decimal>("TRef"); }
            set
            {
                SetProperty("TRef", value);
                NotifyPropertyChanged("TRefPreview");
            }
        }

        public LesModel LESModel
        {
            get { return GetProperty<LesModel>("LESModel"); }
            set
            {
                SetPropertyEnum("LESModel", value);
            }
        }

        public decimal SmagorinskyConstant
        {
            get { return GetProperty<decimal>("SmagorinskyConstant"); }
            set { SetProperty("SmagorinskyConstant", value); }
        }


        public decimal DeltaLESCoeff
        {
            get { return GetProperty<decimal>("DeltaLESCoeff"); }
            set { SetProperty("DeltaLESCoeff", value); }
        }


        public decimal VonKarmanConstant
        {
            get { return GetProperty<decimal>("VonKarmanConstant"); }
            set { SetProperty("VonKarmanConstant", value); }
        }


        public decimal BetaM
        {
            get { return GetProperty<decimal>("BetaM"); }
            set { SetProperty("BetaM", value); }
        }


        public decimal GammM
        {
            get { return GetProperty<decimal>("GammM"); }
            set { SetProperty("GammM", value); }
        }


        public decimal RoughnessHeight
        {
            get { return GetProperty<decimal>("RoughnessHeight"); }
            set
            {
                SetProperty("RoughnessHeight", value);
                NotifyPropertyChanged("RoughnessHeightPreview");
            }
        }

        public decimal SurfaceTemperatureFlux
        {
            get { return GetProperty<decimal>("SurfaceTemperatureFlux"); }
            set
            {
                SetProperty("SurfaceTemperatureFlux", value);
                NotifyPropertyChanged("SurfaceTemperatureFluxPreview");
            }
        }

        public SurfaceStressModel SurfaceStressModel
        {
            get { return GetProperty<SurfaceStressModel>("SurfaceStressModel"); }
            set
            {
                SetPropertyEnum("SurfaceStressModel", value);
            }
        }

        public decimal BetaSurfaceStress
        {
            get { return GetProperty<decimal>("BetaSurfaceStress"); }
            set
            {
                SetPropertyEnum("BetaSurfaceStress", value);
            }
        }

    }
}
