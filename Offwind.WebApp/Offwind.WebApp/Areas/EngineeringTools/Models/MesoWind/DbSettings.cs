using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        public decimal startLat { set; get; }

        [DisplayName("Longitude")]
        public decimal startLng { set; get; }

        [DisplayName("Show all points")]
        public ShowAll showAll { set; get; }

        [DisplayName("Select database")]
        public DbType DbType { set; get; }

        [DisplayName("Search in area (km)")]
        public decimal distance { set; get; }

        public DbSettings()
        {
            startLat = 512;
        }
    }
}