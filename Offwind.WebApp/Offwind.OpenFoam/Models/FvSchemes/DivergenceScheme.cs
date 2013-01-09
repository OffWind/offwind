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

        public DivergenceScheme(string[] array) : base(array[0])
        {
            discretisation = (DiscretisationType)Enum.Parse(typeof(DiscretisationType), array[1]);
            interpolation = (InterpolationType)Enum.Parse(typeof(InterpolationType), array[2]);
            psi = Convert.ToDecimal(array[3]);
        }
    }
}
