using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class DivergenceScheme : SchemeHeader
    {
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public SchemeBound bound { set; get; }
        public DivergenceScheme()
        {
            discretisation = DiscretisationType.Gauss;
            interpolation  = InterpolationType.none;
            bound = new SchemeBound();
        }
    }
}
