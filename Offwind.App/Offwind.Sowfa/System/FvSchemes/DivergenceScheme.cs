using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public sealed class DivergenceScheme : SchemeHeader
    {
        public DiscretisationType discretisation { set; get; }
        public InterpolationType interpolation { set; get; }
        public BoundView view { set; get; }
        public decimal lower_limit { set; get; }
        public decimal upper_limit { set; get; }
        public decimal psi { set; get; }
        public DivergenceScheme()
        {
            discretisation = DiscretisationType.Gauss;
            interpolation  = InterpolationType.none;
            view = BoundView.None;
            psi = 0;
            lower_limit = 0;
            upper_limit = 0;
        }
    }
}
