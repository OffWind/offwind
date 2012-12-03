using System.Collections.Generic;

namespace MvcApplication1.Areas.EngineeringTools.Models.WakeSimulation
{
    public class VTurbineProperties
    {
        public List<VTurbine> Turbines { get; set; }

        public VTurbineProperties()
        {
            Turbines = new List<VTurbine>();
        }
    }
}