using System.Collections.Generic;
using Offwind.WebApp.Areas.EngineeringTools.Models.WindWave.Computations;

namespace Offwind.WebApp.Areas.EngineeringTools.Models.WindWave
{
    public sealed class VWindWave
    {
        public VWindWave()
        {
            PowerOutputItems = new List<VPowerOutput>();
            AdvancedCfdItems = new List<VAdvancedCfd>();
        }

        public List<VPowerOutput> PowerOutputItems { get; set; }
        public List<VAdvancedCfd> AdvancedCfdItems { get; set; }

        public decimal WindSpeed { get; set; }
        public decimal ReferenceHeight { get; set; }
        public decimal TurbineHubHeight { get; set; }
        public decimal TurbineDiameter { get; set; }
        public decimal TurbineEfficiency { get; set; }
        public decimal WaveSpeed { get; set; }

        public WindWaveInput GetInput()
        {
            return new WindWaveInput
                       {
                           Ug = (double) WindSpeed,
                           Zg = (double)ReferenceHeight,
                           Zhub = (double)TurbineHubHeight,
                           Td = (double)TurbineDiameter,
                           Ef = (double)TurbineEfficiency,
                           Cw = (double)WaveSpeed,
                       };
        }
    }
}