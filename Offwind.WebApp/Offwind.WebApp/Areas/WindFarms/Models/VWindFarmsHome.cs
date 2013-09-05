using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Web;
using EmitMapper;
using Offwind.Web.Core;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Models
{
    public class VWindFarmsHome : VWebPage
    {
        public int TotalPublicWindFarms { get; set; }
        public int TotalPublicTurbines { get; set; }
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

        [Required]
        public string Name { get; set; }

        [Required]
        public string Country { get; set; }

        [Display(Name = "Wikipedia URL", Description = "Ex.: \"http://offwind.eu\"")]
        [DataType(DataType.Url)]
        public string UrlPublicWiki { get; set; }

        [Display(Name = "Official site URL", Description = "Ex.: \"http://offwind.eu\"")]
        [DataType(DataType.Url)]
        public string UrlOfficial { get; set; }

        [Display(Name = "Latitude", Description = "-90 .. 90")]
        [Range(-90f, 90f)]
        public decimal GeoLat { get; set; }

        [Display(Name = "Longitude", Description = "-90 .. 90")]
        [Range(-90f, 90f)]
        public decimal GeoLng { get; set; }

        [Required]
        [Display(Name = "Total capacity (MW)")]
        public decimal TotalCapacity { get; set; }

        [Display(Name = "Turbine type", Description = "Turbine of this type will be used in this wind-farm")]
        public Guid? TurbineTypeId { get; set; }

        public string Description { get; set; }

        [Display(Name = "Created by")]
        public string Author { get; set; }

        public List<VTurbine> Turbines { get; set; }

        public bool HasWikiLink()
        {
            return UrlPublicWiki != null && UrlPublicWiki.Trim().Length > 0;
        }

        public bool HasOfficialLink()
        {
            return UrlOfficial != null && UrlOfficial.Trim().Length > 0;
        }

        public bool HasLinks()
        {
            return HasWikiLink() || HasOfficialLink();
        }

        public VWindFarm()
        {
            Turbines = new List<VTurbine>();
        }

        public static VWindFarm MapFromDb(DWindFarm db, IPrincipal user)
        {
            var model = new VWindFarm();
            MapFromDb(model, db, user);
            return model;
        }

        public static void MapFromDb(VWindFarm model, DWindFarm db, IPrincipal user)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<DWindFarm, VWindFarm>();
            mapper.Map(db, model);
            model.Turbines.AddRange(new VTurbine[db.DWindFarmTurbines.Count]);
            model.CanEdit = db.Author == user.Identity.Name;
        }
    }
    
    public class VTurbine : VWebPage
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }
        public string Manufacturer { get; set; }

        [Required]
        [Display(Name = "Rated power (MW)", Description = "Designed max power production for the turbine")]
        public decimal RatedPower { get; set; }

        [Required]
        [Display(Name = "Rotor diameter (m)", Description = "The diameter of the rotor plane")]
        public decimal RotorDiameter { get; set; }

        public string RotorOrientation { get; set; }
        public string RotorConfiguration { get; set; }
        public string Control { get; set; }
        public decimal HubHeight { get; set; }
        public decimal HubDiameter { get; set; }
        public decimal WindSpeedCutIn { get; set; }
        public decimal WindSpeedRated { get; set; }
        public decimal WindSpeedCutOut { get; set; }
        public decimal RotorSpeedCutIn { get; set; }
        public decimal RotorSpeedRated { get; set; }
        public decimal TipSpeedRated { get; set; }
        public decimal RotorMass { get; set; }
        public decimal NacelleMass { get; set; }
        public decimal TowerMass { get; set; }

        public static VTurbine MapFromDb(DTurbine db, IPrincipal user)
        {
            var model = new VTurbine();
            MapFromDb(model, db, user);
            return model;
        }

        public static void MapFromDb(VTurbine model, DTurbine db, IPrincipal user)
        {
            model.Id = db.Id;
            model.Author = db.Author ?? "";
            model.Name = db.Name ?? "";
            model.Description = db.Description ?? "";
            model.Manufacturer = db.Manufacturer ?? "";
            model.RatedPower = db.RatedPower ?? 0;
            model.RotorDiameter = db.RotorDiameter ?? 0;
            model.RotorOrientation = db.RotorOrientation ?? "";
            model.RotorConfiguration = db.RotorConfiguration ?? "";
            model.Control = db.Control ?? "";
            model.HubHeight = db.HubHeight ?? 0;
            model.HubDiameter = db.HubDiameter ?? 0;
            model.WindSpeedCutIn = db.WindSpeedCutIn ?? 0;
            model.WindSpeedRated = db.WindSpeedRated ?? 0;
            model.WindSpeedCutOut = db.WindSpeedCutOut ?? 0;
            model.RotorSpeedCutIn = db.RotorSpeedCutIn ?? 0;
            model.RotorSpeedRated = db.RotorSpeedRated ?? 0;
            model.TipSpeedRated = db.TipSpeedRated ?? 0;
            model.RotorMass = db.RotorMass ?? 0;
            model.NacelleMass = db.NacelleMass ?? 0;
            model.TowerMass = db.TowerMass ?? 0;

            model.CanEdit = db.Author == user.Identity.Name;
        }
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