using Offwind.OpenFoam.Models.Fields;
using Offwind.OpenFoam.Models.RasProperties;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Sowfa.Constant.AirfoilProperties;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.Sowfa.Constant.TurbineProperties;

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
        }

        public static SolverData GetDefaultModel()
        {
            var model = new SolverData();

            return model;
        }
    }
}