using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmCastellatedMeshControls
    {
        public int maxLocalCells { get; set; }
        public int maxGlobalCells { get; set; }
        public RealPoint locationInMesh { get; set; }
        public int minRefinementCells { get; set; }
        public int nCellsBetweenLevels { get; set; }
        public double resolveFeatureAngle { get; set; }
        public List<ShmFeature> Features { get; set; }
        public List<ShmRefinementSurface> Surfaces { get; set; }
        public List<ShmRefinementRegion> Regions { get; set; }
        public bool allowFreeStandingZoneFaces { get; set; }

        public ShmCastellatedMeshControls()
        {
            maxLocalCells = 1000000;
            maxGlobalCells = 2000000;
            minRefinementCells = 0;
            nCellsBetweenLevels = 1;
            resolveFeatureAngle = 30;
            Features = new List<ShmFeature>();
            Surfaces = new List<ShmRefinementSurface>();
            Regions = new List<ShmRefinementRegion>();
            allowFreeStandingZoneFaces = true;
        }
    }
}
