using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.System.FvSchemes
{
    public class FluxCalculation
    {
        public string flux { set; get; }
        public bool enable { set; get; }
        public FluxCalculation()
        {
            flux = null;
            enable = true;
        }
    }
}
