using System.Collections.Generic;

namespace Offwind.Sowfa.Constant.TurbineArrayProperties
{
    public enum OutputControl
    {
        timeStep,
        runTime
    }

    public sealed class TurbineArrayPropData
    {
        public OutputControl outputControl { get; set; }
        public decimal outputInterval { get; set; }
        public List<TurbineInstance> turbine { get; set; }
        public TurbineArrayPropData()
        {
            turbine = new List<TurbineInstance>();
        }
    }
}
