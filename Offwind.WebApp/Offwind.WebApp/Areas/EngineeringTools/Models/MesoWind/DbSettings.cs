using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind
{
    public enum DbType
    {
        All,
        FNL,
        MERRA
    };

    public enum ShowAll
    {
        yes,
        no
    };

    public class DbSettings : VWebPage
    {
        [DisplayName("Latitude")]
        [Display(Description = "Numeric value [-90; 90]")]
        [Range(-90, 90)]
        public decimal startLat { set; get; }

        [DisplayName("Longitude")]
        [Display(Description = "Numeric value [-180; 180]")]
        [Range(-180, 180)]
        public decimal startLng { set; get; }

        [DisplayName("Show all points")]
        public ShowAll showAll { set; get; }

        [DisplayName("Select database")]
        public DbType DbType { set; get; }

        [DisplayName("Search in area (km)")]
        [Display(Description = "Numeric value [100; 1000]")]
        [Range(100, 1000)]
        public decimal distance { set; get; }

        public DbSettings()
        {
            startLat = 512;
        }
    }
}