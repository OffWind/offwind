using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class LaplacianScheme : SchemeHeader
    {
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public SurfaceNormalGradientType  snGradScheme { set; get; }
        public LaplacianScheme()
        {
            discretisation = DiscretisationType.Gauss;
            interpolation  = InterpolationType.none;
            snGradScheme   = SurfaceNormalGradientType.none;
        }
    }
}
