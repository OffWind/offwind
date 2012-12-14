using System.Collections.Generic;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation
{
    public class VTurbineCoordinates
    {
        public List<VTurbine> Turbines { get; set; }

        public VTurbineCoordinates()
        {
            Turbines = new List<VTurbine>();
        }
    }
}