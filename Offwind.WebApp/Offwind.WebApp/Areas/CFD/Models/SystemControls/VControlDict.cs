using System;
using System.Collections.Generic;
using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.ControlDict;

namespace Offwind.WebApp.Areas.CFD.Models.SystemControls
{
    public class VControlDict
    {
        [ReadOnly(true)]
        public ApplicationSolver application { get; set; }

        public StartFrom startFrom { get; set; }
        public Decimal startTime { get; set; }
        public StopAt stopAt { get; set; }
        public Decimal endTime { get; set; }
        public Decimal deltaT { get; set; }
        public WriteControl writeControl { get; set; }
        public Decimal writeInterval { get; set; }
        public Decimal purgeWrite { get; set; }
        public WriteFormat writeFormat { get; set; }
        public Decimal writePrecision { get; set; }
        public WriteCompression writeCompression { get; set; }
        public TimeFormat timeFormat { get; set; }
        public Decimal timePrecision { get; set; }
        public Boolean runTimeModifiable { get; set; }
        public Boolean adjustTimeStep { get; set; }
        public Decimal maxCo { get; set; }
        public Decimal maxDeltaT { get; set; }
        public List<string> Libs { get; set; }

        public VControlDict()
        {
            Libs = new List<string>();
        }
    }
}