using System.Collections.ObjectModel;
using Offwind.Infrastructure.Models;
using Offwind.Sowfa.System.FvSchemes;

namespace Offwind.Products.OpenFoam.UI.fvSchemes
{
    public class VSchemeHead : BaseViewModel
    {
        public string Scheme
        {
            get { return GetProperty<string>("Scheme"); }
            set { SetProperty("Scheme", value); }
        }

        public string Function
        {
            get { return GetProperty<string>("Function"); }
            set { SetProperty("Function", value); }
        }        
    }


    public sealed class VInterpolationScheme : VSchemeHead
    {
        public InterpolationType InterpolationType
        {
            get { return GetProperty<InterpolationType>("InterpolationType"); }
            set { SetPropertyEnum("InterpolationType", value); }
        }


        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }


        public BoundView BoundView
        {
            get { return GetProperty<BoundView>("BoundView"); }
            set { SetPropertyEnum("BoundView", value); }
        }


        public decimal LowerLimit
        {
            get { return GetProperty<decimal>("LowerLimit"); }
            set { SetProperty("LowerLimit", value); }
        }


        public decimal UpperLimit
        {
            get { return GetProperty<decimal>("UpperLimit"); }
            set { SetProperty("UpperLimit", value); }
        }


        public string Flux
        {
            get { return GetProperty<string>("Flux"); }
            set { SetProperty("Flux", value); }
        }

    }

    public sealed class VSurfaceNormalGradientScheme : VSchemeHead
    {
        public SurfaceNormalGradientType SurfaceNoramGradientType
        {
            get { return GetProperty<SurfaceNormalGradientType>("SurfaceNoramGradientType"); }
            set { SetPropertyEnum("SurfaceNoramGradientType", value); }
        }

        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }
    }

    public sealed class VDivergenceScheme : VSchemeHead
    {
        public DiscretisationType DiscretisationType
        {
            get { return GetProperty<DiscretisationType>("DiscretisationType"); }
            set { SetPropertyEnum("DiscretisationType", value); }            
        }

        public InterpolationType InterpolationType
        {
            get { return GetProperty<InterpolationType>("InterpolationType"); }
            set { SetPropertyEnum("InterpolationType", value); }
        }


        public BoundView BoundView
        {
            get { return GetProperty<BoundView>("BoundView"); }
            set { SetPropertyEnum("BoundView", value); }
        }


        public decimal LowerLimit
        {
            get { return GetProperty<decimal>("LowerLimit"); }
            set { SetProperty("LowerLimit", value); }
        }


        public decimal UpperLimit
        {
            get { return GetProperty<decimal>("UpperLimit"); }
            set { SetProperty("UpperLimit", value); }
        }


        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }

    }

    public sealed class VGradientScheme : VSchemeHead
    {
        public LimitedType LimitedType
        {
            get { return GetProperty<LimitedType>("LimitedType"); }
            set { SetPropertyEnum("LimitedType", value); }
        }


        public DiscretisationType DiscretisationType
        {
            get { return GetProperty<DiscretisationType>("DiscretisationType"); }
            set { SetPropertyEnum("DiscretisationType", value); }
        }


        public InterpolationType InterpolationType
        {
            get { return GetProperty<InterpolationType>("InterpolationType"); }
            set { SetPropertyEnum("InterpolationType", value); }
        }


        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }

    }

    public sealed class VLaplacianScheme : VSchemeHead
    {
        public DiscretisationType DiscretisationType
        {
            get { return GetProperty<DiscretisationType>("DiscretisationType"); }
            set { SetPropertyEnum("DiscretisationType", value); }
        }


        public InterpolationType InterpolationType
        {
            get { return GetProperty<InterpolationType>("InterpolationType"); }
            set { SetPropertyEnum("InterpolationType", value); }
        }


        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }


        public SurfaceNormalGradientType SurfaceNoramGradientType
        {
            get { return GetProperty<SurfaceNormalGradientType>("SurfaceNoramGradientType"); }
            set { SetPropertyEnum("SurfaceNoramGradientType", value); }
        }


    }

    public sealed class VTimeScheme : VSchemeHead
    {
        public decimal Psi
        {
            get { return GetProperty<decimal>("Psi"); }
            set { SetProperty("Psi", value); }
        }


        public TimeSchemeType TimeSchemeType
        {
            get { return GetProperty<TimeSchemeType>("TimeSchemeType"); }
            set { SetPropertyEnum("TimeSchemeType", value); }
        }

    }

    public sealed class VFluxControl : BaseViewModel
    {
        public string Flux
        {
            get { return GetProperty<string>("Flux"); }
            set { SetProperty("Flux", value); }
        }


        public bool Enable
        {
            get { return GetProperty<bool>("Enable"); }
            set { SetProperty("Enable", value); }
        }

    }

    public sealed class VSchemesCollection : BaseViewModel
    {
        public ObservableCollection<VInterpolationScheme> cInterpolation { get; set; }
        public ObservableCollection<VSurfaceNormalGradientScheme> cSnGrad { get; set; }
        public ObservableCollection<VGradientScheme> cGradient { get; set; }
        public ObservableCollection<VLaplacianScheme> cLaplacian { get; set; }
        public ObservableCollection<VDivergenceScheme> cDivergence { get; set; }
        public ObservableCollection<VTimeScheme> cTime { get; set; }
        public ObservableCollection<VFluxControl> cFlux { get; set; }

        public VSchemesCollection()
        {
            cInterpolation = new ObservableCollection<VInterpolationScheme>();
            cSnGrad = new ObservableCollection<VSurfaceNormalGradientScheme>();
            cGradient = new ObservableCollection<VGradientScheme>();
            cDivergence = new ObservableCollection<VDivergenceScheme>();
            cLaplacian = new ObservableCollection<VLaplacianScheme>();
            cFlux = new ObservableCollection<VFluxControl>();
            cTime = new ObservableCollection<VTimeScheme>();
        }
    }

}
