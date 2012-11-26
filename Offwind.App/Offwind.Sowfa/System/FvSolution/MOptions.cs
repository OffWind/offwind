namespace Offwind.Sowfa.System.FvSolution
{
    public sealed class MOptions
    {
        public int nCorrectors { get; set; }
        public int nNonOrthogonalCorrectors { get; set; }
        public bool pdRefOn { get; set; }
        public int pdRefCell { get; set; }
        public int pdRefValue { get; set; }
        public bool tempEqnOn { get; set; }
    }
}
