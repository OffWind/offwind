using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.Constant.TurbineProperties
{
    public enum ControllerType
    {
        Undefined,
        none,
        fiveRegion,
    }

    public sealed class TorqueControllerParams
    {
        public decimal CutInGenSpeed { get; set; }
        public decimal RatedGenSpeed { get; set; }
        public decimal Region2StartGenSpeed { get; set; }
        public decimal Region2EndGenSpeed { get; set; }
        public decimal CutInGenTorque { get; set; }
        public decimal RatedGenTorque { get; set; }
        public decimal RateLimitGenTorque { get; set; }
        public decimal KGen { get; set; }
        public decimal TorqueControllerRelax { get; set; }
    }

    public sealed class PitchControllerParams
    {
        public decimal PitchControlStartPitch { get; set; }
        public decimal PitchControlEndPitch { get; set; }
        public decimal PitchControlStartSpeed { get; set; }
        public decimal PitchControlEndSpeed { get; set; }
        public decimal RateLimitPitch { get; set; }
    }

    public struct AirfoilBlade
    {
        public string AirfoilName;
        public List<Vertice> Blade;
    }
    
    public sealed class TurbinePropertiesData
    {
        public int NumBl  { get; set; }
        public decimal TipRad { get; set; }
        public decimal HubRad { get; set; }
        public decimal UndSling { get; set; }
        public decimal OverHang { get; set; }
        public decimal TowerHt { get; set; }
        public decimal Twr2Shft { get; set; }
        public decimal ShftTilt { get; set; }
        public Vertice PreCone   { get; set; }
        public decimal GBRatio { get; set; }
        public decimal GenIner { get; set; }
        public decimal HubIner { get; set; }
        public decimal BladeIner { get; set; }
        public ControllerType TorqueControllerType { get; set; }
        public ControllerType YawControllerType { get; set; }
        public ControllerType PitchControllerType { get; set; }
        public TorqueControllerParams torqueControllerParams { get; set; }
        public PitchControllerParams pitchControllerParams { get; set; }
        public List<AirfoilBlade> airfoilBlade { get; set; }

        public TurbinePropertiesData()
        {
            NumBl = 2;
            TorqueControllerType = ControllerType.none;
            YawControllerType = ControllerType.none;
            PitchControllerType = ControllerType.none;
            airfoilBlade = new List<AirfoilBlade>();

            torqueControllerParams = new TorqueControllerParams();
            pitchControllerParams = new PitchControllerParams();
            airfoilBlade = new List<AirfoilBlade>();
            PreCone = new Vertice();
        }
    }
}
