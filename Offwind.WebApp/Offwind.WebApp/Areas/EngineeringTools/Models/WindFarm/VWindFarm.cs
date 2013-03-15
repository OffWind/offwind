using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindFarm
{
    public sealed class VWindFarm
    {
        public Double StartTime { set; get; }
        public Double StopTime { set; get; }
        public Double TimeStep { set; get; }
        [DisplayName("Wind farm scale")]
        public Double Scale { set; get; }

        public List<VWindTurbine> Turbines { set; get; }

        public VWindFarm()
        {
            StartTime = 0;
            StopTime = 1000;
            TimeStep = 0.01;
            Scale = 5000000;
            Turbines = new List<VWindTurbine>();
        }
    }
}