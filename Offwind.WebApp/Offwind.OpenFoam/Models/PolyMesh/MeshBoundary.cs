using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.Products.OpenFoam.Models.PolyMesh
{
    public sealed class MeshBoundary
    {
        public string Name { get; set; }
        public PatchType Type { get; set; }
        public string NeighbourPatch { get; set; }
        public List<List<int>> Faces { get; set; }

        public MeshBoundary()
        {
            Faces =new List<List<int>>();
        }

        public MeshBoundary(string name1, PatchType type1, string neighbourPatch1, IEnumerable<int> faces1)
        {
            Faces = new List<List<int>>();
            Name = name1;
            Type = type1;
            NeighbourPatch = neighbourPatch1;
            Faces.Add(new List<int>(faces1));
        }

        public MeshBoundary(string name1, PatchType type1, string neighbourPatch1)
        {
            Faces = new List<List<int>>();
            Name = name1;
            Type = type1;
            NeighbourPatch = neighbourPatch1;
        }

        public MeshBoundary AddFaces(IEnumerable<int> faces1)
        {
            Faces.Add(new List<int>(faces1));
            return this;
        }
    }
}
