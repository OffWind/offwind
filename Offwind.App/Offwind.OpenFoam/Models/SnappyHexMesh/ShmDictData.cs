using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public class ShmDictData
    {
        public bool castellatedMesh { get; set; }
        public bool snap { get; set; }
        public bool addLayers { get; set; }
        public List<ShmGeometry> Geometries { get; set; }
        public ShmCastellatedMeshControls CastellatedMeshControls { get; set; }
        public ShmSnapControls SnapControls { get; set; }
        public ShmAddLayersControls AddLayersControls { get; set; }
        public ShmMeshQualityControls MeshQualityControls { get; set; }
        public double mergeTolerance { get; set; }
        public int debug { get; set; }

        public ShmDictData()
        {
            castellatedMesh = true;
            snap = true;
            addLayers = false;
            mergeTolerance = 1e-06d;
            debug = 0;

            Geometries = new List<ShmGeometry>();
            CastellatedMeshControls = new ShmCastellatedMeshControls();
            SnapControls = new ShmSnapControls();
            AddLayersControls = new ShmAddLayersControls();
            MeshQualityControls = new ShmMeshQualityControls();
        }
    }
}
