using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2New
{
    public enum NowcastingSimulationMethod
    {
        [Display(Name = "AR(1)")]
        a,
        [Display(Name = "Persistence")]
        p
    }

    public class VNowcastingProperties : VWebPage
    {
        public bool WasWakeSimulationPerformed = false;

        public NowcastingSimulationMethod Method { set; get; }

        [DisplayName("Time for starting")]
        [Description("Time for starting multi step prediction. If < 1 it is assumed  a fraction of the end time")]
        public decimal TimeForStarting { set; get; }
        [DisplayName("Decimation")]
        [Description("Decimation with a moving average of order")]
        public int Decimation { set; get; }
        [DisplayName("Sampling time")]
        [Description("Sampling time")]
        public decimal SamplingTime { set; get; }

        public VNowcastingProperties()
        {
        }
    }
}
