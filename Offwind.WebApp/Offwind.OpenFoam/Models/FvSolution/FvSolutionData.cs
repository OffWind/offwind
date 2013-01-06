
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offwind.OpenFoam.Models.FvSolution
{
    public class FvSolutionData
    {
        public List<FvSolver> Solvers { set; get; }
        public FvSolution Solution { set; get; }
    }
}
