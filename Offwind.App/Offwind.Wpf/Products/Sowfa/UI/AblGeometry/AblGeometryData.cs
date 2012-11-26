using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Sowfa.System.TopoSetDict;

namespace Offwind.Products.Sowfa.UI.AblGeometry
{
    public sealed class AblGeometryData
    {
        public BlockMeshDictData BlockMesh { get; set; }
        public TopoSetDictData TopoSet { get; set; }

        public AblGeometryData()
        {
            BlockMesh = new BlockMeshDictData();
            TopoSet = new TopoSetDictData();
        }
    }
}
