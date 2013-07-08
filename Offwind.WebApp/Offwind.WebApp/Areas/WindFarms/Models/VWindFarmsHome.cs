using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Models
{
    public class VWindFarmsHome : VWebPage
    {
        public List<VWindFarm> WindFarms { get; set; }
        public List<VTurbine> Turbines { get; set; }

        public VWindFarmsHome()
        {
            WindFarms = new List<VWindFarm>();
            Turbines = new List<VTurbine>();
        }
    }

    public class VWindFarm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string UrlWiki { get; set; }
        public string UrlOfficial { get; set; }
        public decimal GeoLat { get; set; }
        public decimal GeoLng { get; set; }
        public decimal TotalCapacity { get; set; }
        public string Description { get; set; }

        public List<VTurbine> Turbines { get; set; }

        public VWindFarm()
        {
            Turbines = new List<VTurbine>();
        }
    }

    public class VTurbine
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<VParameter> Parameters { get; set; }
        
    }

    public class VParameter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Group { get; set; }
        public string Type { get; set; }
        public string ValueTxt { get; set; }
        public decimal ValueNumeric { get; set; }
    }
}