using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmRefinementRegion
    {
        public string Name { get; set; }
        public ShmRefinementRegionMode Mode { get; set; }
        public List<ShmRefinementLevel> Levels { get; set; }

        public ShmRefinementRegion()
        {
            Levels = new List<ShmRefinementLevel>();
        }
    }
}
