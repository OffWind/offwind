using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.PolyMesh
{
    public sealed class BlockMeshDictData
    {
        public decimal convertToMeters { get; set; }
        public List<Vertice> vertices { get; set; }
        public MeshBlock MeshBlocks { get; set; }
        public List<MeshBoundary> boundaries { get; set; }

        public BlockMeshDictData()
        {
            vertices = new List<Vertice>();
            MeshBlocks = new MeshBlock();
            boundaries = new List<MeshBoundary>();
        }
    }
}
