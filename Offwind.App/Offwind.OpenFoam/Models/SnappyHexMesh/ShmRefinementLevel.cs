namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public struct ShmRefinementLevel
    {
        private double _min;
        private double _max;

        public double Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public double Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public ShmRefinementLevel(double min, double max)
        {
            _min = min;
            _max = max;
        }
    }
}
