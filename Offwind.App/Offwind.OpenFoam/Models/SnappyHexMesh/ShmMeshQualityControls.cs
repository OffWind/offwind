namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmMeshQualityControls
    {
        public ShmMeshQualityControls()
        {
            maxNonOrtho = 65;
            maxBoundarySkewness = 20;
            maxInternalSkewness = 4;
            maxConcave = 80;
            minVol = 1e-13;
            minTetQuality = 1e-30;
            minArea = -1;
            minTwist = 0.05;
            minDeterminant = 0.001;
            minFaceWeight = 0.05;
            minVolRatio = 0.01;
            minTriangleTwist = -1;
            nSmoothScale = 4;
            errorReduction = 0.75;
            relaxed = true;
            relaxedMaxNonOrtho = 75;
        }

        public double maxNonOrtho { get; set; }
        public double maxBoundarySkewness { get; set; }
        public double maxInternalSkewness { get; set; }
        public double maxConcave { get; set; }
        public double minVol { get; set; }
        public double minTetQuality { get; set; }
        public double minArea { get; set; }
        public double minTwist { get; set; }
        public double minDeterminant { get; set; }
        public double minFaceWeight { get; set; }
        public double minVolRatio { get; set; }
        public double minTriangleTwist { get; set; }
        public double nSmoothScale { get; set; }
        public double errorReduction { get; set; }
        public bool relaxed { get; set; }
        public double relaxedMaxNonOrtho { get; set; }
    }
}
