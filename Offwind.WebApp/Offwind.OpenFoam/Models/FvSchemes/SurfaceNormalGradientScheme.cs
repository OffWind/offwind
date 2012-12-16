using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class SurfaceNormalGradientScheme : SchemeHeader
    {
        public SurfaceNormalGradientType type { set; get; }
        public decimal psi { set; get; }
        public SurfaceNormalGradientScheme()
        {
            type = SurfaceNormalGradientType.limited;
            psi = 0;
        }
    }
}
