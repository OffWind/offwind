using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.Sowfa.Constant.TurbineArrayPropertiesFAST
{
    public sealed class TurbineArrayPropFASTGeneral
    {
        public decimal yawAngle { set; get; }
        public int numberofBld { set; get; }
        public int numberofBldPts { set; get; }
        public decimal rotorDiameter { set; get; }
        public decimal epsilon { set; get; }
        public decimal smearRadius { set; get; }
        public decimal effectiveRadiusFactor { set; get; }
        public int pointInterpType { set; get; }
    }
}
