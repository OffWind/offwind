using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.Constant.TurbineArrayProperties
{
    public enum PointInterpType
    {
        linear,
        cellCenter
    }

    public enum BladeUpdateType
    {
        oldPosition,
        newPosition
    }

    public enum TipRootLossCorrType
    {
        none,
        Glauert
    }

    public sealed class TurbineInstance
    {
        public string turbineType      { set; get; }
        public Vertice baseLocation     { set; get; }
        public decimal numBladePoints  { set; get; }
        public string pointDistType    { set; get; }
        public PointInterpType pointInterpType  { set; get; }
        public BladeUpdateType bladeUpdateType { set; get; }
        public decimal epsilon { set; get; }
        public TipRootLossCorrType tipRootLossCorrType { set; get; }
        public string rotationDir { set; get; }
        public decimal azimuth { set; get; }
        public decimal rotSpeed { set; get; }
        public decimal pitch    { set; get; }
        public decimal nacYaw   { set; get; }
        public decimal fluidDensity { set; get; }

        public TurbineInstance()
        {
            baseLocation = new Vertice();
        }
    }
}
