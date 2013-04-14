using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindFarm
{
    public sealed class VWindFarm : VWebPage
    {
        public decimal StartTime { set; get; }
        public decimal StopTime { set; get; }
        public decimal TimeStep { set; get; }

        [DisplayName("Wind farm scale")]
        public Double Scale { set; get; }

        public List<VWindTurbine> Turbines { set; get; }

        public VWindFarm()
        {
            Turbines = new List<VWindTurbine>();
        }
    }
}