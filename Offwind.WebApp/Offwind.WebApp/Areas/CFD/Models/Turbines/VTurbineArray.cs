using System.Collections.Generic;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public class VTurbineArray : VWebPage
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