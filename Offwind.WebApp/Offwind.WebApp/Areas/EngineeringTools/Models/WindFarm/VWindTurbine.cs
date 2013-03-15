using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindFarm
{
    public sealed class VWindTurbine
    {
        public Double rho { set; get; }
        public Double radius { set; get; }
        public Double rated { set; get; }
        public Double Cp { set; get; }
        public Double speed { set; get; }

        public VWindTurbine()
        {
            
        }

        public VWindTurbine(string[] array)
        {
            rho = Convert.ToDouble(array[0]);
            radius = Convert.ToDouble(array[1]);
            rated = Convert.ToDouble(array[2]);
            Cp = Convert.ToDouble(array[3]);
            speed = Convert.ToDouble(array[4]);
        }
    }
}
