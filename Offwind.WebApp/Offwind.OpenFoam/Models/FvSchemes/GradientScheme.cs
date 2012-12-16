using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public enum LimitedType
    {
        none,
        cellLimited,
        faceLimited
    }

    public sealed class GradientScheme : SchemeHeader
    {
        public LimitedType limited { set; get; }
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public decimal psi { set; get; }

        public GradientScheme()
        {
            limited = LimitedType.none;
            discretisation = DiscretisationType.Gauss;
            interpolation = InterpolationType.none;
            psi = 0;
        }
    }
}
