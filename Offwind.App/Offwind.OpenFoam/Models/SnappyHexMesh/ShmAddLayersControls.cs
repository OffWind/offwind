namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmAddLayersControls
    {
        public ShmAddLayersControls()
        {
            relativeSizes = true;
            expansionRatio = 1.0;
            finalLayerThickness = 0.3;
            minThickness = 0.25;
            nGrow = 0;
            featureAngle = 30;
            nRelaxIter = 5;
            nSmoothSurfaceNormals = 1;
            nSmoothNormals = 3;
            nSmoothThickness = 10;
            maxFaceThicknessRatio = 0.5;
            maxThicknessToMedialRatio = 0.3;
            minMedianAxisAngle = 90;
            nBufferCellsNoExtrude = 0;
            nLayerIter = 50;
            nRelaxedIter = 20;
        }

        public bool relativeSizes { get; set; }
        public double expansionRatio { get; set; }
        public double finalLayerThickness { get; set; }
        public double minThickness { get; set; }
        public double nGrow { get; set; }
        public double featureAngle { get; set; }
        public int nRelaxIter { get; set; }
        public int nSmoothSurfaceNormals { get; set; }
        public int nSmoothNormals { get; set; }
        public int nSmoothThickness { get; set; }
        public double maxFaceThicknessRatio { get; set; }
        public double maxThicknessToMedialRatio { get; set; }
        public double minMedianAxisAngle { get; set; }
        public int nBufferCellsNoExtrude { get; set; }
        public int nLayerIter { get; set; }
        public int nRelaxedIter { get; set; }
    }
}
