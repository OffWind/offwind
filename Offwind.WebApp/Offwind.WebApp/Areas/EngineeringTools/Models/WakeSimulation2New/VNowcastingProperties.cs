using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2New
{
    public enum NowcastingSimulationMethod
    {
        [System.ComponentModel.DataAnnotations.Display(Name = "AR(1)", ShortName = "AR(1)", Description = "AR(1)")]
        [Description("AR(1)")]
        a,
        [System.ComponentModel.DataAnnotations.Display(Name = "Persistence", ShortName = "Persistence", Description = "Persistence")]
        [Description("Persistence")]
        p
    }

    public class VNowcastingProperties : VWebPage
    {
        public NowcastingSimulationMethod Method { set; get; }

        [DisplayName("Time for starting")]
        [Description("Time for starting multi step prediction. If < 1 it is assumed  a fraction of the end time")]
        public double TimeForStarting { set; get; }
        [DisplayName("Decimation")]
        [Description("Decimation with a moving average of order")]
        public double Decimation { set; get; }
        [DisplayName("Sampling time")]
        [Description("Sampling time")]
        public double SamplingTime { set; get; }

        public VNowcastingProperties()
        {
        }
    }
}
