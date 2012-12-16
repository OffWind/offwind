 using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public enum InterpolationType
    {
        none,
        linear,
        cubicCorrection,
        midPoint,
        upwind,
        linearUpwind,
        skewLinear,
        filteredLinear2,
        limitedLinear,
        vanLeer,
        MUSCL,
        limitedCubic,
        SFCD,
        Gamma
    }

    public class InterolationExtension
    {
        public decimal lower_limit;
        public decimal upper_limit;
    }
}
