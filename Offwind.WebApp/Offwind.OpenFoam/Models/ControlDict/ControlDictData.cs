using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;

namespace Offwind.Sowfa.System.ControlDict
{
    public sealed class ControlDictData
    {
        public ApplicationSolver application { get; set; }
        public StartFrom startFrom { get; set; }
        public decimal startTime { get; set; }
        public StopAt stopAt { get; set; }
        public decimal endTime { get; set; }
        public decimal deltaT { get; set; }
        public WriteControl writeControl { get; set; }
        public decimal writeInterval { get; set; }
        public decimal purgeWrite { get; set; }
        public WriteFormat writeFormat { get; set; }
        public decimal writePrecision { get; set; }
        public WriteCompression writeCompression { get; set; }
        public TimeFormat timeFormat { get; set; }
        public decimal timePrecision { get; set; }
        public bool runTimeModifiable { get; set; }
        public FlagYesNo adjustTimeStep { get; set; }
        public decimal maxCo { get; set; }
        public decimal maxDeltaT { get; set; }
    }
}
