using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Offwind.Sowfa.Constant.TurbineProperties;

namespace Offwind.WebApp.Areas.CFD.Models.Turbines
{
    public struct VAirfoilBlade
    {
        [DisplayName("AirfoilName")]
        public String AirfoilName { get; set; }
        public List<VVertice> Blade { get; set; }

    }

    public class VTurbineType
    {
        public VTurbineType()
        {
            PreCone = new VVertice();
            torqueControllerParams = new VTorqueControllerParams();
            pitchControllerParams = new VPitchControllerParams();
        }

        public int NumBl { get; set; }
        public decimal TipRad { get; set; }
        public decimal HubRad { get; set; }
        public decimal UndSling { get; set; }
        public decimal OverHang { get; set; }
        public decimal TowerHt { get; set; }
        public decimal Twr2Shft { get; set; }
        public decimal ShftTilt { get; set; }
        public VVertice PreCone { get; set; }
        public decimal GBRatio { get; set; }
        public decimal GenIner { get; set; }
        public decimal HubIner { get; set; }
        public decimal BladeIner { get; set; }
        public ControllerType TorqueControllerType { get; set; }
        public ControllerType YawControllerType { get; set; }
        public ControllerType PitchControllerType { get; set; }

        public List<VAirfoilBlade> airfoilBlade { get; set; }


        public VTorqueControllerParams torqueControllerParams { get; set; }
        public VPitchControllerParams pitchControllerParams { get; set; }
    }
}