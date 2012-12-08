using System.Collections.Generic;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation
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