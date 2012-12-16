using System.Collections.Generic;
using Offwind.Sowfa.Constant.TurbineArrayProperties;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public class VTurbineArray
    {
        public VTurbineArray()
        {
            turbine = new List<TurbineInstance>();
        }

        public OutputControl outputControl { get; set; }
        public decimal outputInterval { get; set; }
        public List<TurbineInstance> turbine { get; set; }
    }
}