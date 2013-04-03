using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.MesoWind
{
    public class VMesoWind
    {
        public DbType DbType { set; get; }
        public int TotalCount { set; get; }
        public int FnlCount { set; get; }
        public int MerraCount { set; get; }

        public bool UseSearchResults { set; get; }
        public List<SmallMesoscaleTabFile> InterestingPoints { set; get; }
        public List<MesoscaleTabFile> ImportedPoints { set; get; }

        public VMesoWind()
        {
            DbType = DbType.All;
            UseSearchResults = false;
            InterestingPoints = new List<SmallMesoscaleTabFile>();
            ImportedPoints = new List<MesoscaleTabFile>();
        }
    }
}
