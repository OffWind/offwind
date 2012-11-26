using System.Collections.Generic;

namespace Offwind.Sowfa.System.FvSolution
{
    public sealed class FvSolutionData
    {
        public List<MLinearSolver> Solvers { get; set; }
        public MOptions Options { get; set; }

        public FvSolutionData()
        {
            Solvers = new List<MLinearSolver>();
            Options = new MOptions();
        }
    }
}
