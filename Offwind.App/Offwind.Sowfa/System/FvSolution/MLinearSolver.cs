using Offwind.Products.OpenFoam.Models.FvSolution;

namespace Offwind.Sowfa.System.FvSolution
{
    public sealed class MLinearSolver
    {
        public string Name { get; set; }
        public LinearSolver solver { get; set; }
        public Preconditioner preconditioner { get; set; }
        public decimal tolerance { get; set; }
        public decimal relTol { get; set; }
        public Smoother smoother { get; set; }
        public int nPreSweeps { get; set; }
        public int nPostSweeps { get; set; }
        public int nFinestSweeps { get; set; }
        public bool cacheAgglomeration { get; set; }
        public int nCellsInCoarsestLevel { get; set; }
        public Agglomerator agglomerator { get; set; }
        public int mergeLevels { get; set; }

        public MLinearSolver()
        {
            Name = "";
        }
    }
}
