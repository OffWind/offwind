using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2New
{
    public class VGeneralProperties : VWebPage
    {
        public string WindFarm { set; get; }
        //public double[,] Turbines { set; get; }

        [DisplayName("Enable Power Distribution?")]
        [Description("Enables wind farm control and not only constant power")]
        public bool EnablePowerDistribution { set; get; }
        [DisplayName("Enable Turbine Dynamics?")]
        [Description("Enable Dynamic Turbine Model")]
        public bool EnableTurbineDynamics { set; get; }
        [DisplayName("Enable Power Reference Interpolation")]
        [Description("Enable Power Reference Interpolation")]
        public bool PowerRefInterpolation { set; get; }
        [DisplayName("Enable a Varying Demand")]
        [Description("Enables a Power Demand that varies")]
        public bool EnableVaryingDemand { set; get; }

        [DisplayName("End Time")]
        [Description("end time of simulation")]
        [Range(0.1, 10800)]
        public decimal StopTime { set; get; }
        [DisplayName("Time Step")]
        [Description("time Step")]
        public decimal TimeStep { set; get; }
        [DisplayName("Update Interval, Control")]
        [Description("update time of controller")]
        public decimal ControlUpdateInterval { set; get; }
        [DisplayName("Update Interval, Power")]
        [Description("update time of the power reference")]
        public decimal PowerUpdateInterval { set; get; }
        [DisplayName("Initial Power Demand")]
        [Description("The initial Power demand, if the varying demand option is set to \"false\" this will remain constant throughout the simulation")]
        public decimal InitialPowerDemand { set; get; }

        public VGeneralProperties()
        {
            WindFarm = "";
            //Turbines = new double[0, 0];

            EnablePowerDistribution = true;
            EnableTurbineDynamics = true;
            PowerRefInterpolation = true;
            EnableVaryingDemand = true;

            StopTime = 100;
            TimeStep = 0.125m;
            ControlUpdateInterval = 5;
            PowerUpdateInterval = 1;
            InitialPowerDemand = (decimal)(50 * 5e6);
        }
    }
}
