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
        public int GridX { get; set; }
        public int GridY { get; set; }
        public decimal RotationAngle { get; set; }
        public decimal MeanWind { get; set; }
        public decimal WakeExpansion { get; set; }
        public decimal StartTime { set; get; }
        public decimal StopTime { set; get; }
        public decimal TimeStep { set; get; }

        public decimal Cp { set; get; }
        public decimal Ct { set; get; }

        [DisplayName("Air density")]
        public decimal Rho { set; get; }

        public string WindFarm { set; get; }

        public double[,] Turbines { set; get; }
        public int NTurbines { set; get; }

        public VGeneralProperties()
        {
            GridX = 1000;
            GridY = 1000;
            MeanWind = 9;
            RotationAngle = -48.4m;
            WakeExpansion = 0.06m;
            StartTime = 0;
            StopTime = 100;
            TimeStep = 0.125m;
            Rho = 1.53m;
            WindFarm = "";
            Cp = 0.485m;
            Ct = 0.784m;
        }
    }
}
