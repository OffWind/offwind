using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmGeometry
    {
        public ShmGeometryType Type { get; set; }
        public string GlobalName { get; set; }
        public string Name { get; set; }

        public RealPoint SphereCenter { get; set; }
        public double SphereRadius { get; set; }

        public RealPoint BoxMin { get; set; }
        public RealPoint BoxMax { get; set; }

        public List<ShmRegion> Regions { get; set; }

        public ShmGeometry()
        {
            Regions = new List<ShmRegion>();
        }
    }
}
