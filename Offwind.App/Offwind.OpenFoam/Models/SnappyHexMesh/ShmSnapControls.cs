namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmSnapControls
    {
        public int NSmoothPatch { get; set; }
        public double Tolerance { get; set; }
        public double NSolveIter { get; set; }
        public double NRelaxIter { get; set; }
        public double NFeatureSnapIter { get; set; }

        public ShmSnapControls()
        {
            NSmoothPatch = 3;
            Tolerance = 1.0;
            NSolveIter = 30;
            NRelaxIter = 5;
            NFeatureSnapIter = 10;
        }
    }
}
