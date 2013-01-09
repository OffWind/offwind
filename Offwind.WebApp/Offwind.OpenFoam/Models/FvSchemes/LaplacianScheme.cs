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
        public LaplacianScheme(string[] array) : base(array[0])
        {
            discretisation = (DiscretisationType) Enum.Parse(typeof (DiscretisationType), array[1]);
            interpolation = (InterpolationType) Enum.Parse(typeof (InterpolationType), array[2]);
            snGradScheme = (SurfaceNormalGradientType) Enum.Parse(typeof (SurfaceNormalGradientType), array[3]);
        }
    }
}
