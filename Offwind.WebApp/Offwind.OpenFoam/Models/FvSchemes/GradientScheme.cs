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

        public GradientScheme()
        {
            limited = LimitedType.none;
            discretisation = DiscretisationType.Gauss;
            interpolation = InterpolationType.none;
        }

        public GradientScheme(string[] array) : base(array[0])
        {
            discretisation = (DiscretisationType)Enum.Parse(typeof(DiscretisationType), array[1]);
            interpolation = (InterpolationType)Enum.Parse(typeof(InterpolationType), array[2]);
        }
    }
}
