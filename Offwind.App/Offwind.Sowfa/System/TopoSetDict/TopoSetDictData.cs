namespace Offwind.Sowfa.System.TopoSetDict
{
    public sealed class TopoSetDictData
    {
        public decimal X1 { get; set; }
        public decimal Y1 { get; set; }
        public decimal Z1 { get; set; }
        public decimal X2 { get; set; }
        public decimal Y2 { get; set; }
        public decimal Z2 { get; set; }

        public TopoSetDictData()
        {
            X1 = -0.1m;
            Y1 = -0.1m;
            Z1 = -0.1m;
        }
    }
}
