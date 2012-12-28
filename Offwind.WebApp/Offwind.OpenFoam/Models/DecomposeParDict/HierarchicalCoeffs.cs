using System;
using System.Collections.Generic;
using System.Linq;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public sealed class HierarchicalCoeffs
    {
        public Vertice n { set; get; }
        public decimal delta { set; get; }
        public DecompositionOrder order { set; get; } 
    }
}
