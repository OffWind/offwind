using System;
using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models.ControlDict;

namespace Offwind.WebApp.Areas.CFD.Models.SystemControls
{
    public class VControlDict
    {
        public ApplicationSolver Application { get; set; }
        public StartFrom StartFrom { get; set; }
        public Decimal StartTime { get; set; }
        public StopAt StopAt { get; set; }
        public Decimal EndTime { get; set; }
        public Decimal DeltaT { get; set; }
        public WriteControl WriteControl { get; set; }
        public Decimal WriteInterval { get; set; }
        public Decimal PurgeWrite { get; set; }
        public WriteFormat WriteFormat { get; set; }
        public Decimal WritePrecision { get; set; }
        public WriteCompression WriteCompression { get; set; }
        public TimeFormat TimeFormat { get; set; }
        public Decimal TimePrecision { get; set; }
        public Boolean IsRunTimeModifiable { get; set; }
        public Boolean AdjustTimeStep { get; set; }
        public Decimal MaxCo { get; set; }
        public Decimal MaxDeltaT { get; set; }
        public List<string> Libs { get; set; }

        public VControlDict()
        {
            Libs = new List<string>();
        }
    }
}