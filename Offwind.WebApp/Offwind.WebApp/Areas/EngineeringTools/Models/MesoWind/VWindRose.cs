using System.Collections.Generic;

namespace MvcApplication1.Areas.EngineeringTools.Models.MesoWind
{
    public class VWindRose
    {
        public List<HPoint> FreqByDirs { get; set; }
        public List<HPoint> MeanVelocityPerDir { get; set; }

        public VWindRose()
        {
            FreqByDirs = new List<HPoint>();
            MeanVelocityPerDir = new List<HPoint>();
        }
    }
}