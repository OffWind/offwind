using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2
{
    public class VGeneralProperties : VWebPage
    {
        public decimal StartTime { set; get; }
        public decimal StopTime { set; get; }
        public Double TimeStep { set; get; }

        [DisplayName("Air density")]
        public Double Rho { set; get; }

        public string WindFarm { set; get; }

        public VGeneralProperties()
        {
            StartTime = 0;
            StopTime = 100;
            TimeStep = 0.125;
            Rho = 1.53;
            WindFarm = "";
        }
    }
}
