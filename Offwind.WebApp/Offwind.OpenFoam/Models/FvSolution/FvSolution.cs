using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.OpenFoam.Models.FvSolution
{
    public class FvSolution
    {
        public string Name { set; get; }
        public int nCorrectors { set; get; }
        public int nNonOrthogonalCorrectors { set; get; }
        public decimal pRefCell { set; get; }
        public decimal pRefValue { set; get; }
    }
}
