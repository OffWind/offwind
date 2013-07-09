using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

    public class VWindFarm : VWebPage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        [Display(Name = "Wikipedia URL")]
        public string UrlPublicWiki { get; set; }

        [Display(Name = "Official site URL")]
        public string UrlOfficial { get; set; }

        [Display(Name = "Latitude")]
        public decimal GeoLat { get; set; }

        [Display(Name = "Longitude")]
        public decimal GeoLng { get; set; }

        [Display(Name = "Total capacity (MW)")]
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