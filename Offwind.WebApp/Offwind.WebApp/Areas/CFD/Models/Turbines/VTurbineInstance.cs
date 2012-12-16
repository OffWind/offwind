using Offwind.Products.OpenFoam.Models;
using Offwind.Sowfa.Constant.TurbineArrayProperties;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public class VTurbineInstance
    {
        public string turbineType { set; get; }
        public Vertice baseLocation { set; get; }
        public decimal numBladePoints { set; get; }
        public string pointDistType { set; get; }
        public PointInterpType pointInterpType { set; get; }
        public BladeUpdateType bladeUpdateType { set; get; }
        public decimal epsilon { set; get; }
        public TipRootLossCorrType tipRootLossCorrType { set; get; }
        public string rotationDir { set; get; }
        public decimal azimuth { set; get; }
        public decimal rotSpeed { set; get; }
        public decimal pitch { set; get; }
        public decimal nacYaw { set; get; }
        public decimal fluidDensity { set; get; }

        public VTurbineInstance()
        {
            baseLocation = new Vertice();
        }
    }
}