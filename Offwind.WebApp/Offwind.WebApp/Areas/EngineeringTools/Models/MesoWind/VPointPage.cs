using System.Collections.Generic;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind
{
    public class VPointPage : VWebPage
    {
        public string Db { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
        public object[][] VelocityFreq { get; set; }
        public IEnumerable<object[]>[] WindRose { get; set; }

        public int sEcho { get; set; }
        public int iTotalRecords { get; set; }
        public int iTotalDisplayRecords { get; set; }
        public List<string[]> Data { get; set; }
    }
}