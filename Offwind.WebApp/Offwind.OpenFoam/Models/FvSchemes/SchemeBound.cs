namespace Offwind.Sowfa.System.FvSchemes
{
    public class SchemeBound
    {
        public BoundView view { set; get; }
        public decimal Lower { set; get; }
        public decimal Upper { set; get; }

        public SchemeBound()
        {
            view = BoundView.None;
            Lower = Upper = 0;
        }
    }
}
