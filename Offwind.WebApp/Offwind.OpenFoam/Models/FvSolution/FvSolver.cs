using System;
using System.Collections.Generic;
using System.Linq;
using Offwind.Products.OpenFoam.Models.FvSolution;

namespace Offwind.OpenFoam.Models.FvSolution
{
    public class FvSolver
    {
        public string Name { set; get; }
        public LinearSolver Solver { set; get; }
        public Preconditioner Preconditioner { set; get; }
        public decimal Tolerance { set; get; }
        public decimal RelTol { set; get; }
    }
}
