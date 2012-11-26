using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Sowfa.System.TopoSetDict;

namespace Offwind.Products.Sowfa.UI.AblGeometry
{
    public sealed class AblGeometryHandler : FoamFileHandler
    {
        /// <summary>
        /// Reads two files - blockMeshDict and topoSetDict
        /// </summary>
        /// <param name="path">Project directory</param>
        /// <returns></returns>
        public override object Read(string path)
        {
            var d = new AblGeometryData();
            
            var bHandler = new BlockMeshDictHandler();
            var bPath = bHandler.GetPath(path);
            d.BlockMesh = (BlockMeshDictData)bHandler.Read(bPath);

            var tHandler = new TopoSetDictHandler();
            var tPath = tHandler.GetPath(path);
            d.TopoSet = (TopoSetDictData)tHandler.Read(tPath);
            
            return d;
        }

        /// <summary>
        /// Writes two files - blockMeshDict and topoSetDict
        /// </summary>
        /// <param name="path">Project directory</param>
        /// <returns></returns>
        public override void Write(string path, object data)
        {
            var d = (AblGeometryData)data;

            var bHandler = new BlockMeshDictHandler();
            var bPath = bHandler.GetPath(path);
            bHandler.Write(bPath, d.BlockMesh);

            var tHandler = new TopoSetDictHandler();
            var tPath = tHandler.GetPath(path);
            tHandler.Write(tPath, d.TopoSet);
        }

        public override void WriteDefault(string solverDir, object data)
        {
            new BlockMeshDictHandler().WriteDefault(solverDir, null);
            new TopoSetDictHandler().WriteDefault(solverDir, null);
        }
    }
}
