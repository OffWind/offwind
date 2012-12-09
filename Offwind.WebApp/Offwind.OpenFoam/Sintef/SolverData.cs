using System.Collections.Generic;
using Offwind.OpenFoam.Models.Fields;
using Offwind.OpenFoam.Models.RasProperties;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Sowfa.Constant.AirfoilProperties;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.Sowfa.Constant.TurbineProperties;
using Offwind.Sowfa.System.ControlDict;

namespace Offwind.OpenFoam.Sintef
{
    public class SolverData
    {
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

            m.BlockMeshDict.convertToMeters = 1;
            m.BlockMeshDict.vertices.AddRange(new []
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

            m.BlockMeshDict.MeshBlocks.vertexNumbers.AddRange(new [] { 0, 1, 2, 3, 4, 5, 6, 7 });
            m.BlockMeshDict.MeshBlocks.numberOfCells.AddRange(new [] { 100, 100, 30 });
            m.BlockMeshDict.MeshBlocks.grading = Grading.simpleGrading;
            m.BlockMeshDict.MeshBlocks.gradingNumbers.AddRange(new [] { 1, 1, 1 });
            return m;
        }
    }
}