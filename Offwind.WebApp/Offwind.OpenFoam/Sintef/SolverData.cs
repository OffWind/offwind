using System;
using System.IO;
using Offwind.OpenFoam.Models.Fields;
using Offwind.OpenFoam.Models.RasProperties;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Sowfa.Constant.AblProperties;
using Offwind.Sowfa.Constant.AirfoilProperties;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.Sowfa.Constant.TurbineProperties;
using Offwind.Sowfa.System.ControlDict;

namespace Offwind.OpenFoam.Sintef
{
    public class SolverData
    {
        public string DefaultArchName = Path.Combine(Path.GetTempPath(), "solver.zip");

        public BoundaryField FieldEpsilon { get; set; }
        public BoundaryField FieldK { get; set; }
        public BoundaryField FieldNut { get; set; }
        public BoundaryField FieldP { get; set; }
        public BoundaryField FieldR { get; set; }
        public BoundaryField FieldU { get; set; }

        public TurbulencePropertiesData TurbulenceProperties { get; set; }
        public RasPropertiesData RasProperties { get; set; }
        public TransportPropertiesData TransportProperties { get; set; }

        public TurbinePropertiesData TurbineProperties { get; set; }
        public TurbineArrayPropData TurbineArrayProperties { get; set; }

        public BlockMeshDictData BlockMeshDict { get; set; }
        public ControlDictData ControlDict { get; set; }
        public AirfoilPropertiesData AirfoilProperties { get; set; }
        public ProcessingSettings ProcessingSettings { get; set; }

        private string fsPath = null;

        public SolverData()
        {
            FieldEpsilon = new BoundaryField();
            FieldK = new BoundaryField();
            FieldNut = new BoundaryField();
            FieldP = new BoundaryField();
            FieldR = new BoundaryField();
            FieldU = new BoundaryField();

            TurbulenceProperties = new TurbulencePropertiesData();
            RasProperties = new RasPropertiesData();
            TransportProperties = new TransportPropertiesData();
            TurbineProperties = new TurbinePropertiesData();
            BlockMeshDict = new BlockMeshDictData();
            AirfoilProperties = new AirfoilPropertiesData();

            ProcessingSettings = new ProcessingSettings();

            ControlDict = new ControlDictData();
        }

        public static SolverData GetDefaultModel()
        {
            var m = new SolverData();

            var bm = m.BlockMeshDict;
            bm.convertToMeters = 1;
            bm.vertices.AddRange(new[]
            {
                new Vertice(-500, -500, 0),
                new Vertice(6000, -500, 0),
                new Vertice(6000, 6000, 0),
                new Vertice(-500, 6000, 0),
                new Vertice(-500, -500, 1000),
                new Vertice(6000, -500, 1000),
                new Vertice(6000, 6000, 1000),
                new Vertice(-500, 6000, 1000),
            });

            bm.MeshBlocks.vertexNumbers.AddRange(new[] { 0, 1, 2, 3, 4, 5, 6, 7 });
            bm.MeshBlocks.numberOfCells.AddRange(new[] { 100, 100, 30 });
            bm.MeshBlocks.grading = Grading.simpleGrading;
            bm.MeshBlocks.gradingNumbers.AddRange(new[] { 1, 1, 1 });

            var tp = m.TransportProperties;
            tp.nu = 0.00001m;

            tp.CplcNu0 = 0.000001m;
            tp.CplcNuInf = 0.000001m;
            tp.CplcM = 1m;
            tp.CplcN = 1m;

            tp.BccNu0 = 0.000001m;
            tp.BccNuInf = 0.000001m;
            tp.BccM = 0m;
            tp.BccN = 1m;
            return m;
        }

        public string MakeFS()
        {
            fsPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(fsPath);

            /*
            var ablHandler = new AblPropertiesHandler();
            ablHandler.Write(fsPath, AblProperties);
             */
            var controlDictHandler = new ControlDictHandler();
            controlDictHandler.Write(controlDictHandler.GetPath(fsPath), ControlDict);

            var transportPropHandler = new TransportPropertiesHandler();
            transportPropHandler.Write(transportPropHandler.GetPath(fsPath), TransportProperties);

            var blockMeshHandler = new BlockMeshDictHandler();
            blockMeshHandler.Write(blockMeshHandler.GetPath(fsPath), BlockMeshDict);

            return fsPath;
        }

        public string ArchName(string key)
        {
            return Path.Combine(Path.GetTempPath(), key + ".zip");
        }
    }
}
