using System;
using System.Collections.Generic;
using Offwind.Sowfa.System.FvSchemes;

namespace Offwind.WebApp.Areas.CFD.Models.SystemControls
{
    public class VSchemeHeader
    {
        public Boolean isDefault { set; get; }
        public String scheme { set; get; }
        public String function { set; get; }
        public Decimal psi { set; get; }
    }

    public class VSchemeBound
    {
        public BoundView view { set; get; }
        public Decimal Lower { set; get; }
        public Decimal Upper { set; get; }
    }

    public sealed class VTimeScheme : VSchemeHeader
    {
        public TimeSchemeType type { set; get; }
    }

    public sealed class VGradientScheme : VSchemeHeader
    {
        public LimitedType limited { set; get; }
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
    }

    public sealed class VInterpolationScheme : VSchemeHeader
    {
        public VSchemeBound bound;
        public InterpolationType interpolation { set; get; }
        public String flux { set; get; }
    }

    public sealed class VDivergenceScheme : VSchemeHeader
    {
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public VSchemeBound bound { set; get; }
    }

    public sealed class VLaplacianScheme : VSchemeHeader
    {
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public SurfaceNormalGradientType snGradScheme { set; get; }
    }

    public sealed class VSurfaceNormalGradientScheme : VSchemeHeader
    {
        public SurfaceNormalGradientType type { set; get; }
    }

    public class VFluxCalculation
    {
        public String flux { set; get; }
        public Boolean enable { set; get; }
    }

    public class VSchemes
    {
        public List<VTimeScheme> ddtSchemes { set; get; }
        public List<VGradientScheme> gradSchemes { set; get; }
        public List<VInterpolationScheme> interpolationSchemes { set; get; }
        public List<VLaplacianScheme> laplacianSchemes { set; get; }
        public List<VDivergenceScheme> divSchemes { set; get; }
        public List<VSurfaceNormalGradientScheme> snGradSchemes { set; get; }
        public List<VFluxCalculation> fluxCalculation { set; get; }

        public VSchemes()
        {
            ddtSchemes = new List<VTimeScheme>();
            gradSchemes = new List<VGradientScheme>();
            interpolationSchemes = new List<VInterpolationScheme>();
            laplacianSchemes = new List<VLaplacianScheme>();
            divSchemes = new List<VDivergenceScheme>();
            snGradSchemes = new List<VSurfaceNormalGradientScheme>();
            fluxCalculation = new List<VFluxCalculation>();
        }
    }
}
