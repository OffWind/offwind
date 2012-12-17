using System;
using System.Collections.Generic;
using System.IO;
using Offwind.OpenFoam.Models.Fields;
using Offwind.OpenFoam.Models.RasProperties;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.OpenFoam.Sintef.BoundaryFields;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;
using Offwind.Products.OpenFoam.Models.Fields;
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

        public FieldEpsilon FieldEpsilon { get; set; }
        public FieldK FieldK { get; set; }
        public FieldP FieldP { get; set; }
        public FieldR FieldR { get; set; }
        public FieldU FieldU { get; set; }
        public BoundaryField FieldNut { get; set; }

        public TurbulencePropertiesData TurbulenceProperties { get; set; }
        public RasPropertiesData RasProperties { get; set; }
        public TransportPropertiesData TransportProperties { get; set; }

        public TurbinePropertiesData TurbineProperties { get; set; }
        public TurbineArrayPropData TurbineArrayProperties { get; set; }

        public BlockMeshDictData BlockMeshDict { get; set; }
        public ControlDictData ControlDict { get; set; }
        public AirfoilPropertiesData AirfoilProperties { get; set; }
        public ProcessingSettings ProcessingSettings { get; set; }

        private readonly ControlDictHandler controlDictHandler;
        private readonly TransportPropertiesHandler transportPropHandler;
        private readonly BlockMeshDictHandler blockMeshHandler;
        private readonly TurbineArrayPropHandler turbineArrayPropHandler;
        private readonly FieldEpsilonHandler fieldEpsilonHandler;
        private readonly FieldKHandler fieldKHandler;
        private readonly FieldPHandler fieldPHandler;
        private readonly FieldUHandler fieldUHandler;
        private readonly FieldRHandler fieldRHandler;
        private readonly TurbineProperiesHandler turbineProperiesHandler;

        public SolverData()
        {
            FieldEpsilon = new FieldEpsilon();
            FieldK = new FieldK();
            FieldNut = new BoundaryField();
            FieldP = new FieldP();
            FieldR = new FieldR();
            FieldU = new FieldU();

            controlDictHandler = new ControlDictHandler();
            transportPropHandler = new TransportPropertiesHandler();
            blockMeshHandler = new BlockMeshDictHandler();
            turbineArrayPropHandler = new TurbineArrayPropHandler();
            turbineProperiesHandler = new TurbineProperiesHandler("NREL5MWRef", true);
            fieldEpsilonHandler = new FieldEpsilonHandler();
            fieldKHandler = new FieldKHandler();
            fieldPHandler = new FieldPHandler();
            fieldUHandler = new FieldUHandler();
            fieldRHandler = new FieldRHandler();

            BlockMeshDict = (BlockMeshDictData) blockMeshHandler.Read(null);
            ControlDict = (ControlDictData) controlDictHandler.Read(null);
            TransportProperties = (TransportPropertiesData) transportPropHandler.Read(null);
            TurbineArrayProperties = (TurbineArrayPropData) turbineArrayPropHandler.Read(null);
            TurbineProperties = (TurbinePropertiesData) turbineProperiesHandler.Read(null);
            
            
            /* extra post-initialize calls */
            InitTransportProperties(TransportProperties);
            InitFieldK(FieldK);
            InitFieldEpsilon(FieldEpsilon);
            InitFieldP(FieldP);
            InitFieldR(FieldR);
            InitFieldU(FieldU);

            TurbulenceProperties = new TurbulencePropertiesData();
            RasProperties = new RasPropertiesData();
            AirfoilProperties = new AirfoilPropertiesData();
            ProcessingSettings = new ProcessingSettings();
        }

        public static SolverData GetDefaultModel()
        {
            return new SolverData();
        }

        public void MakeJobFS(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            controlDictHandler.Write(controlDictHandler.GetPath(path), ControlDict);            
            blockMeshHandler.Write(blockMeshHandler.GetPath(path), BlockMeshDict);
            turbineArrayPropHandler.Write(turbineArrayPropHandler.GetPath(path), TurbineArrayProperties);
            turbineProperiesHandler.Write(turbineProperiesHandler.GetPath(path), TurbineProperties);
            fieldEpsilonHandler.Write(fieldEpsilonHandler.GetPath(path), FieldEpsilon);
            fieldKHandler.Write(fieldKHandler.GetPath(path), FieldK);
            fieldPHandler.Write(fieldPHandler.GetPath(path), FieldP);
            fieldUHandler.Write(fieldUHandler.GetPath(path), FieldU);
            fieldRHandler.Write(fieldRHandler.GetPath(path), FieldR);

           
            /* TODO: extra write handlres */

            transportPropHandler.Write(transportPropHandler.GetPath(path), TransportProperties);

        }

        private static void InitTransportProperties(TransportPropertiesData tp)
        {
            tp.MolecularViscosity = 0.00001m;

            tp.CplcNu0 = 0.000001m;
            tp.CplcNuInf = 0.000001m;
            tp.CplcM = 1m;
            tp.CplcN = 1m;

            tp.BccNu0 = 0.000001m;
            tp.BccNuInf = 0.000001m;
            tp.BccM = 0m;
            tp.BccN = 1m;
        }

        private static void InitFieldK(FieldK f)
        {
            f.BottomType = PatchType.kqRWallFunction;
            f.BottomValue.Type = PatchValueType.Uniform;
            f.BottomValue.Value = 1.5m;

            f.TopType = PatchType.kqRWallFunction;
            f.TopValue.Type = PatchValueType.Uniform;
            f.TopValue.Value = 1.5m;

            f.WestType = PatchType.fixedValue;
            f.WestValue.Type = PatchValueType.Uniform;
            f.WestValue.Value = 1.5m;

            f.EastType = PatchType.zeroGradient;

            f.NorthType = PatchType.zeroGradient;

            f.SouthType = PatchType.fixedValue;
            f.SouthValue.Type = PatchValueType.Uniform;
            f.SouthValue.Value = 1.5m;
        }

        private static void InitFieldEpsilon(FieldEpsilon f)
        {
            f.BottomType = PatchType.epsilonWallFunction;
            f.BottomValue.Cmu = 0.09m;
            f.BottomValue.kappa = 0.41m;
            f.BottomValue.E = 9.8m;
            f.BottomValue.value.Type = PatchValueType.Uniform;
            f.BottomValue.value.Value = 34.4993m;

            f.TopType = PatchType.epsilonWallFunction;
            f.TopValue.Cmu = 0.09m;
            f.TopValue.kappa = 0.41m;
            f.TopValue.E = 9.8m;
            f.TopValue.value.Type = PatchValueType.Uniform;
            f.TopValue.value.Value = 34.4993m;

            f.WestType = PatchType.fixedValue;
            f.WestValue.Type = PatchValueType.Uniform;
            f.WestValue.Value = 34.4993m;

            f.EastType = PatchType.zeroGradient;

            f.NorthType = PatchType.zeroGradient;

            f.SouthType = PatchType.fixedValue;
            f.SouthValue.Type = PatchValueType.Uniform;
            f.SouthValue.Value = 34.4993m;
        }

        private static void InitFieldP(FieldP f)
        {
            f.BottomType = PatchType.zeroGradient;
            f.TopType = PatchType.zeroGradient;
            f.WestType = PatchType.zeroGradient;

            f.EastType = PatchType.fixedValue;
            f.EastValue.Type = PatchValueType.Uniform;
            f.EastValue.Value = 0m;

            f.NorthType = PatchType.zeroGradient;

            f.SouthType = PatchType.fixedValue;
            f.SouthValue.Type = PatchValueType.Uniform;
            f.SouthValue.Value = 0m;
        }

        private static void InitFieldR(FieldR f)
        {
            f.InternalField.Type = PatchValueType.Uniform;
            f.InternalField.Value1 = 0;
            f.InternalField.Value2 = 0;
            f.InternalField.Value3 = 0;
            f.InternalField.Value4 = 0;
            f.InternalField.Value5 = 0;
            f.InternalField.Value6 = 0;

            f.BottomType = PatchType.zeroGradient;
            f.TopType = PatchType.zeroGradient;

            f.WestType = PatchType.fixedValue;
            f.WestValue.Type = PatchValueType.Uniform;
            f.WestValue.Value1 = 0;
            f.WestValue.Value2 = 0;
            f.WestValue.Value3 = 0;
            f.WestValue.Value4 = 0;
            f.WestValue.Value5 = 0;
            f.WestValue.Value6 = 0;

            f.EastType = PatchType.zeroGradient;
            f.NorthType = PatchType.zeroGradient;

            f.SouthType = PatchType.fixedValue;
            f.SouthValue.Type = PatchValueType.Uniform;
            f.SouthValue.Value1 = 0;
            f.SouthValue.Value2 = 0;
            f.SouthValue.Value3 = 0;
            f.SouthValue.Value4 = 0;
            f.SouthValue.Value5 = 0;
            f.SouthValue.Value6 = 0;
        }

        private static void InitFieldU(FieldU f)
        {
            f.InternalField.Type = PatchValueType.Uniform;
            f.InternalField.Value1 = 7.304m;
            f.InternalField.Value2 = 8.226m;
            f.InternalField.Value3 = 0;

            f.TopType = PatchType.slip;

            f.WestType = PatchType.atmBoundaryLayerInletVelocity;
            f.WestParams.Uref = 11;
            f.WestParams.Href = 520;
            f.WestParams.N = new Vertice()
                                   {
                                       X = 0.664m,
                                       Y = 0.7478m,
                                       Z = 0
                                   };
            f.WestParams.Z = new Vertice()
                                   {
                                       X = 0,
                                       Y = 0,
                                       Z = 1
                                   };
            f.WestParams.Z0 = new PatchValueScalar()
                                    {
                                        Type = PatchValueType.Uniform,
                                        Value = 0.014m
                                    };
            f.WestParams.ZGround = new PatchValueScalar()
                                         {
                                             Type = PatchValueType.Uniform,
                                             Value = 0
                                         };
            f.WestParams.Value.Type = PatchValueType.Uniform;
            f.WestParams.Value.Value1 = 0;
            f.WestParams.Value.Value2 = 0;
            f.WestParams.Value.Value3 = 0;

            f.EastType = PatchType.zeroGradient;
            f.NorthType = PatchType.zeroGradient;
            f.BottomType = PatchType.fixedValue;
            f.BottomValue.Type = PatchValueType.Uniform;
            f.BottomValue.Value1 = 0;
            f.BottomValue.Value2 = 0;
            f.BottomValue.Value3 = 0;


            f.SouthType = PatchType.atmBoundaryLayerInletVelocity;
            f.SouthParams.Uref = 11;
            f.SouthParams.Href = 520;
            f.SouthParams.N = new Vertice()
            {
                X = 0.664m,
                Y = 0.7478m,
                Z = 0
            };
            f.SouthParams.Z = new Vertice()
            {
                X = 0,
                Y = 0,
                Z = 1
            };
            f.SouthParams.Z0 = new PatchValueScalar()
            {
                Type = PatchValueType.Uniform,
                Value = 0.014m
            };
            f.SouthParams.ZGround = new PatchValueScalar()
            {
                Type = PatchValueType.Uniform,
                Value = 0
            };
            f.SouthParams.Value.Type = PatchValueType.Uniform;
            f.SouthParams.Value.Value1 = 0;
            f.SouthParams.Value.Value2 = 0;
            f.SouthParams.Value.Value3 = 0;
        }

        #region Unused manualy init

        private static void InitTimeControl(ControlDictData cd)
        {
            cd.application = ApplicationSolver.pisoFoam;
            cd.startFrom = StartFrom.latestTime;
            cd.startTime = 10m;
            cd.stopAt = StopAt.endTime;
            cd.endTime = 200m;
            cd.deltaT = 0.005m;
            cd.writeControl = WriteControl.timeStep;
            cd.writeInterval = 1000m;
            cd.purgeWrite = 0;
            cd.writeFormat = WriteFormat.ascii;
            cd.writePrecision = 6m;
            cd.writeCompression = WriteCompression.off;
            cd.timeFormat = TimeFormat.general;
            cd.timePrecision = 6m;
            cd.runTimeModifiable = true;
        }

        private static void InitBlockMeshDict(BlockMeshDictData bm)
        {
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
        }

        private static void InitTurbineArray(TurbineArrayPropData ta)
        {
            ta.outputControl = OutputControl.timeStep;
            ta.outputInterval = 1;

            var t0 = new TurbineInstance();
            t0.turbineType = "NREL5MWRef";
            t0.numBladePoints = 40;
            t0.pointDistType = "uniform";
            t0.pointInterpType = PointInterpType.linear;
            t0.bladeUpdateType = BladeUpdateType.oldPosition;
            t0.epsilon = 3.5m;
            t0.tipRootLossCorrType = TipRootLossCorrType.none;
            t0.rotationDir = "cw";
            t0.azimuth = 232.0105m;
            t0.rotSpeed = 10.9874m;
            t0.pitch = 0m;
            t0.nacYaw = 221.617m;
            t0.fluidDensity = 1.23m;
            t0.baseLocation.X = 3396.9131m;
            t0.baseLocation.Y = 2696.6644m;
            t0.baseLocation.Z = 0m;
            ta.turbine.Add(t0);

            var t1 = new TurbineInstance();
            t1.turbineType = "NREL5MWRef";
            t1.numBladePoints = 40;
            t1.pointDistType = "uniform";
            t1.pointInterpType = PointInterpType.linear;
            t1.bladeUpdateType = BladeUpdateType.oldPosition;
            t1.epsilon = 3.5m;
            t1.tipRootLossCorrType = TipRootLossCorrType.none;
            t1.rotationDir = "cw";
            t1.azimuth = 20.6292m;
            t1.rotSpeed = 12.9765m;
            t1.pitch = 0m;
            t1.nacYaw = 221.617m;
            t1.fluidDensity = 1.23m;
            t1.baseLocation.X = 3132.8249m;
            t1.baseLocation.Y = 2393.3441m;
            t1.baseLocation.Z = 0m;
            ta.turbine.Add(t1);

            var t2 = new TurbineInstance();
            t2.turbineType = "NREL5MWRef";
            t2.numBladePoints = 40;
            t2.pointDistType = "uniform";
            t2.pointInterpType = PointInterpType.linear;
            t2.bladeUpdateType = BladeUpdateType.oldPosition;
            t2.epsilon = 3.5m;
            t2.tipRootLossCorrType = TipRootLossCorrType.none;
            t2.rotationDir = "cw";
            t2.azimuth = 93.6725m;
            t2.rotSpeed = 9.9608m;
            t2.pitch = 0m;
            t2.nacYaw = 221.617m;
            t2.fluidDensity = 1.23m;
            t2.baseLocation.X = 2870.7135m;
            t2.baseLocation.Y = 2090.0238m;
            t2.baseLocation.Z = 0m;
            ta.turbine.Add(t2);

            var t3 = new TurbineInstance();
            t3.turbineType = "NREL5MWRef";
            t3.numBladePoints = 40;
            t3.pointDistType = "uniform";
            t3.pointInterpType = PointInterpType.linear;
            t3.bladeUpdateType = BladeUpdateType.oldPosition;
            t3.epsilon = 3.5m;
            t3.tipRootLossCorrType = TipRootLossCorrType.none;
            t3.rotationDir = "cw";
            t3.azimuth = 0.04096m;
            t3.rotSpeed = 10.1165m;
            t3.pitch = 0m;
            t3.nacYaw = 221.617m;
            t3.fluidDensity = 1.23m;
            t3.baseLocation.X = 2605.3661m;
            t3.baseLocation.Y = 1796.488m;
            t3.baseLocation.Z = 0m;
            ta.turbine.Add(t3);

            var t4 = new TurbineInstance();
            t4.turbineType = "NREL5MWRef";
            t4.numBladePoints = 40;
            t4.pointDistType = "uniform";
            t4.pointInterpType = PointInterpType.linear;
            t4.bladeUpdateType = BladeUpdateType.oldPosition;
            t4.epsilon = 3.5m;
            t4.tipRootLossCorrType = TipRootLossCorrType.none;
            t4.rotationDir = "cw";
            t4.azimuth = 191.385m;
            t4.rotSpeed = 10.8197m;
            t4.pitch = 0m;
            t4.nacYaw = 221.617m;
            t4.fluidDensity = 1.23m;
            t4.baseLocation.X = 2343.2546m;
            t4.baseLocation.Y = 1493.1677m;
            t4.baseLocation.Z = 0m;
            ta.turbine.Add(t4);

            var t5 = new TurbineInstance();
            t5.turbineType = "NREL5MWRef";
            t5.numBladePoints = 40;
            t5.pointDistType = "uniform";
            t5.pointInterpType = PointInterpType.linear;
            t5.bladeUpdateType = BladeUpdateType.oldPosition;
            t5.epsilon = 3.5m;
            t5.tipRootLossCorrType = TipRootLossCorrType.none;
            t5.rotationDir = "cw";
            t5.azimuth = 58.5761m;
            t5.rotSpeed = 10.5913m;
            t5.pitch = 0m;
            t5.nacYaw = 221.617m;
            t5.fluidDensity = 1.23m;
            t5.baseLocation.X = 2077.9073m;
            t5.baseLocation.Y = 1199.6319m;
            t5.baseLocation.Z = 0m;
            ta.turbine.Add(t5);

            var t6 = new TurbineInstance();
            t6.turbineType = "NREL5MWRef";
            t6.numBladePoints = 40;
            t6.pointDistType = "uniform";
            t6.pointInterpType = PointInterpType.linear;
            t6.bladeUpdateType = BladeUpdateType.oldPosition;
            t6.epsilon = 3.5m;
            t6.tipRootLossCorrType = TipRootLossCorrType.none;
            t6.rotationDir = "cw";
            t6.azimuth = 348.6736m;
            t6.rotSpeed = 15.7277m;
            t6.pitch = 0m;
            t6.nacYaw = 221.617m;
            t6.fluidDensity = 1.23m;
            t6.baseLocation.X = 1806.088m;
            t6.baseLocation.Y = 896.31156m;
            t6.baseLocation.Z = 0m;
            ta.turbine.Add(t6);

            var t7 = new TurbineInstance();
            t7.turbineType = "NREL5MWRef";
            t7.numBladePoints = 40;
            t7.pointDistType = "uniform";
            t7.pointInterpType = PointInterpType.linear;
            t7.bladeUpdateType = BladeUpdateType.oldPosition;
            t7.epsilon = 3.5m;
            t7.tipRootLossCorrType = TipRootLossCorrType.none;
            t7.rotationDir = "cw";
            t7.azimuth = 299.6255m;
            t7.rotSpeed = 11.3648m;
            t7.pitch = 0m;
            t7.nacYaw = 221.617m;
            t7.fluidDensity = 1.23m;
            t7.baseLocation.X = 3132.8249m;
            t7.baseLocation.Y = 2843.4323m;
            t7.baseLocation.Z = 0m;
            ta.turbine.Add(t7);

            var t8 = new TurbineInstance();
            t8.turbineType = "NREL5MWRef";
            t8.numBladePoints = 40;
            t8.pointDistType = "uniform";
            t8.pointInterpType = PointInterpType.linear;
            t8.bladeUpdateType = BladeUpdateType.oldPosition;
            t8.epsilon = 3.5m;
            t8.tipRootLossCorrType = TipRootLossCorrType.none;
            t8.rotationDir = "cw";
            t8.azimuth = 140.3665m;
            t8.rotSpeed = 11.7127m;
            t8.pitch = 0m;
            t8.nacYaw = 221.617m;
            t8.fluidDensity = 1.23m;
            t8.baseLocation.X = 2867.4775m;
            t8.baseLocation.Y = 2553.158m;
            t8.baseLocation.Z = 0m;
            ta.turbine.Add(t8);

            var t9 = new TurbineInstance();
            t9.turbineType = "NREL5MWRef";
            t9.numBladePoints = 40;
            t9.pointDistType = "uniform";
            t9.pointInterpType = PointInterpType.linear;
            t9.bladeUpdateType = BladeUpdateType.oldPosition;
            t9.epsilon = 3.5m;
            t9.tipRootLossCorrType = TipRootLossCorrType.none;
            t9.rotationDir = "cw";
            t9.azimuth = 69.9494m;
            t9.rotSpeed = 11.491m;
            t9.pitch = 0m;
            t9.nacYaw = 221.617m;
            t9.fluidDensity = 1.23m;
            t9.baseLocation.X = 2605.3661m;
            t9.baseLocation.Y = 2249.8377m;
            t9.baseLocation.Z = 0m;
            ta.turbine.Add(t9);

            var t10 = new TurbineInstance();
            t10.turbineType = "NREL5MWRef";
            t10.numBladePoints = 40;
            t10.pointDistType = "uniform";
            t10.pointInterpType = PointInterpType.linear;
            t10.bladeUpdateType = BladeUpdateType.oldPosition;
            t10.epsilon = 3.5m;
            t10.tipRootLossCorrType = TipRootLossCorrType.none;
            t10.rotationDir = "cw";
            t10.azimuth = 305.0639m;
            t10.rotSpeed = 10.0846m;
            t10.pitch = 0m;
            t10.nacYaw = 221.617m;
            t10.fluidDensity = 1.23m;
            t10.baseLocation.X = 2343.2546m;
            t10.baseLocation.Y = 1943.2559m;
            t10.baseLocation.Z = 0m;
            ta.turbine.Add(t10);

            var t11 = new TurbineInstance();
            t11.turbineType = "NREL5MWRef";
            t11.numBladePoints = 40;
            t11.pointDistType = "uniform";
            t11.pointInterpType = PointInterpType.linear;
            t11.bladeUpdateType = BladeUpdateType.oldPosition;
            t11.epsilon = 3.5m;
            t11.tipRootLossCorrType = TipRootLossCorrType.none;
            t11.rotationDir = "cw";
            t11.azimuth = 116.408m;
            t11.rotSpeed = 11.5416m;
            t11.pitch = 0m;
            t11.nacYaw = 221.617m;
            t11.fluidDensity = 1.23m;
            t11.baseLocation.X = 2077.9073m;
            t11.baseLocation.Y = 1649.7201m;
            t11.baseLocation.Z = 0m;
            ta.turbine.Add(t11);

            var t12 = new TurbineInstance();
            t12.turbineType = "NREL5MWRef";
            t12.numBladePoints = 40;
            t12.pointDistType = "uniform";
            t12.pointInterpType = PointInterpType.linear;
            t12.bladeUpdateType = BladeUpdateType.oldPosition;
            t12.epsilon = 3.5m;
            t12.tipRootLossCorrType = TipRootLossCorrType.none;
            t12.rotationDir = "cw";
            t12.azimuth = 152.2903m;
            t12.rotSpeed = 10.9733m;
            t12.pitch = 0m;
            t12.nacYaw = 221.617m;
            t12.fluidDensity = 1.23m;
            t12.baseLocation.X = 1802.852m;
            t12.baseLocation.Y = 1346.3998m;
            t12.baseLocation.Z = 0m;
            ta.turbine.Add(t12);

            var t13 = new TurbineInstance();
            t13.turbineType = "NREL5MWRef";
            t13.numBladePoints = 40;
            t13.pointDistType = "uniform";
            t13.pointInterpType = PointInterpType.linear;
            t13.bladeUpdateType = BladeUpdateType.oldPosition;
            t13.epsilon = 3.5m;
            t13.tipRootLossCorrType = TipRootLossCorrType.none;
            t13.rotationDir = "cw";
            t13.azimuth = 133.0254m;
            t13.rotSpeed = 10.384m;
            t13.pitch = 0m;
            t13.nacYaw = 221.617m;
            t13.fluidDensity = 1.23m;
            t13.baseLocation.X = 1543.9765m;
            t13.baseLocation.Y = 1052.864m;
            t13.baseLocation.Z = 0m;
            ta.turbine.Add(t13);

            var t14 = new TurbineInstance();
            t14.turbineType = "NREL5MWRef";
            t14.numBladePoints = 40;
            t14.pointDistType = "uniform";
            t14.pointInterpType = PointInterpType.linear;
            t14.bladeUpdateType = BladeUpdateType.oldPosition;
            t14.epsilon = 3.5m;
            t14.tipRootLossCorrType = TipRootLossCorrType.none;
            t14.rotationDir = "cw";
            t14.azimuth = 337.447m;
            t14.rotSpeed = 15.0844m;
            t14.pitch = 0m;
            t14.nacYaw = 221.617m;
            t14.fluidDensity = 1.23m;
            t14.baseLocation.X = 1278.6292m;
            t14.baseLocation.Y = 750m;
            t14.baseLocation.Z = 0m;
            ta.turbine.Add(t14);

            var t15 = new TurbineInstance();
            t15.turbineType = "NREL5MWRef";
            t15.numBladePoints = 40;
            t15.pointDistType = "uniform";
            t15.pointInterpType = PointInterpType.linear;
            t15.bladeUpdateType = BladeUpdateType.oldPosition;
            t15.epsilon = 3.5m;
            t15.tipRootLossCorrType = TipRootLossCorrType.none;
            t15.rotationDir = "cw";
            t15.azimuth = 191.845m;
            t15.rotSpeed = 11.0135m;
            t15.pitch = 0m;
            t15.nacYaw = 221.617m;
            t15.fluidDensity = 1.23m;
            t15.baseLocation.X = 2870.7135m;
            t15.baseLocation.Y = 3003.2462m;
            t15.baseLocation.Z = 0m;
            ta.turbine.Add(t15);

            var t16 = new TurbineInstance();
            t16.turbineType = "NREL5MWRef";
            t16.numBladePoints = 40;
            t16.pointDistType = "uniform";
            t16.pointInterpType = PointInterpType.linear;
            t16.bladeUpdateType = BladeUpdateType.oldPosition;
            t16.epsilon = 3.5m;
            t16.tipRootLossCorrType = TipRootLossCorrType.none;
            t16.rotationDir = "cw";
            t16.azimuth = 321.3286m;
            t16.rotSpeed = 11.5914m;
            t16.pitch = 0m;
            t16.nacYaw = 221.617m;
            t16.fluidDensity = 1.23m;
            t16.baseLocation.X = 2605.3661m;
            t16.baseLocation.Y = 2699.9259m;
            t16.baseLocation.Z = 0m;
            ta.turbine.Add(t16);

            var t17 = new TurbineInstance();
            t17.turbineType = "NREL5MWRef";
            t17.numBladePoints = 40;
            t17.pointDistType = "uniform";
            t17.pointInterpType = PointInterpType.linear;
            t17.bladeUpdateType = BladeUpdateType.oldPosition;
            t17.epsilon = 3.5m;
            t17.tipRootLossCorrType = TipRootLossCorrType.none;
            t17.rotationDir = "cw";
            t17.azimuth = 49.0444m;
            t17.rotSpeed = 10.1874m;
            t17.pitch = 0m;
            t17.nacYaw = 221.617m;
            t17.fluidDensity = 1.23m;
            t17.baseLocation.X = 2346.4906m;
            t17.baseLocation.Y = 2393.3441m;
            t17.baseLocation.Z = 0m;
            ta.turbine.Add(t17);

            var t18 = new TurbineInstance();
            t18.turbineType = "NREL5MWRef";
            t18.numBladePoints = 40;
            t18.pointDistType = "uniform";
            t18.pointInterpType = PointInterpType.linear;
            t18.bladeUpdateType = BladeUpdateType.oldPosition;
            t18.epsilon = 3.5m;
            t18.tipRootLossCorrType = TipRootLossCorrType.none;
            t18.rotationDir = "cw";
            t18.azimuth = 130.7909m;
            t18.rotSpeed = 11.2965m;
            t18.pitch = 0m;
            t18.nacYaw = 221.617m;
            t18.fluidDensity = 1.23m;
            t18.baseLocation.X = 2081.1432m;
            t18.baseLocation.Y = 2103.0698m;
            t18.baseLocation.Z = 0m;
            ta.turbine.Add(t18);

            var t19 = new TurbineInstance();
            t19.turbineType = "NREL5MWRef";
            t19.numBladePoints = 40;
            t19.pointDistType = "uniform";
            t19.pointInterpType = PointInterpType.linear;
            t19.bladeUpdateType = BladeUpdateType.oldPosition;
            t19.epsilon = 3.5m;
            t19.tipRootLossCorrType = TipRootLossCorrType.none;
            t19.rotationDir = "cw";
            t19.azimuth = 266.1913m;
            t19.rotSpeed = 10.4342m;
            t19.pitch = 0m;
            t19.nacYaw = 221.617m;
            t19.fluidDensity = 1.23m;
            t19.baseLocation.X = 1802.852m;
            t19.baseLocation.Y = 1796.488m;
            t19.baseLocation.Z = 0m;
            ta.turbine.Add(t19);

            var t20 = new TurbineInstance();
            t20.turbineType = "NREL5MWRef";
            t20.numBladePoints = 40;
            t20.pointDistType = "uniform";
            t20.pointInterpType = PointInterpType.linear;
            t20.bladeUpdateType = BladeUpdateType.oldPosition;
            t20.epsilon = 3.5m;
            t20.tipRootLossCorrType = TipRootLossCorrType.none;
            t20.rotationDir = "cw";
            t20.azimuth = 336.4014m;
            t20.rotSpeed = 12.368m;
            t20.pitch = 0m;
            t20.nacYaw = 221.617m;
            t20.fluidDensity = 1.23m;
            t20.baseLocation.X = 1540.7406m;
            t20.baseLocation.Y = 1506.2137m;
            t20.baseLocation.Z = 0m;
            ta.turbine.Add(t20);

            var t21 = new TurbineInstance();
            t21.turbineType = "NREL5MWRef";
            t21.numBladePoints = 40;
            t21.pointDistType = "uniform";
            t21.pointInterpType = PointInterpType.linear;
            t21.bladeUpdateType = BladeUpdateType.oldPosition;
            t21.epsilon = 3.5m;
            t21.tipRootLossCorrType = TipRootLossCorrType.none;
            t21.rotationDir = "cw";
            t21.azimuth = 134.2256m;
            t21.rotSpeed = 11.2468m;
            t21.pitch = 0m;
            t21.nacYaw = 221.617m;
            t21.fluidDensity = 1.23m;
            t21.baseLocation.X = 1275.3932m;
            t21.baseLocation.Y = 1199.6319m;
            t21.baseLocation.Z = 0m;
            ta.turbine.Add(t21);

            var t22 = new TurbineInstance();
            t22.turbineType = "NREL5MWRef";
            t22.numBladePoints = 40;
            t22.pointDistType = "uniform";
            t22.pointInterpType = PointInterpType.linear;
            t22.bladeUpdateType = BladeUpdateType.oldPosition;
            t22.epsilon = 3.5m;
            t22.tipRootLossCorrType = TipRootLossCorrType.none;
            t22.rotationDir = "cw";
            t22.azimuth = 151.862m;
            t22.rotSpeed = 15.7575m;
            t22.pitch = 0m;
            t22.nacYaw = 221.617m;
            t22.fluidDensity = 1.23m;
            t22.baseLocation.X = 1016.5177m;
            t22.baseLocation.Y = 906.09608m;
            t22.baseLocation.Z = 0m;
            ta.turbine.Add(t22);

            var t23 = new TurbineInstance();
            t23.turbineType = "NREL5MWRef";
            t23.numBladePoints = 40;
            t23.pointDistType = "uniform";
            t23.pointInterpType = PointInterpType.linear;
            t23.bladeUpdateType = BladeUpdateType.oldPosition;
            t23.epsilon = 3.5m;
            t23.tipRootLossCorrType = TipRootLossCorrType.none;
            t23.rotationDir = "cw";
            t23.azimuth = 67.1255m;
            t23.rotSpeed = 11.6953m;
            t23.pitch = 0m;
            t23.nacYaw = 221.617m;
            t23.fluidDensity = 1.23m;
            t23.baseLocation.X = 2605.3661m;
            t23.baseLocation.Y = 3150.0141m;
            t23.baseLocation.Z = 0m;
            ta.turbine.Add(t23);

            var t24 = new TurbineInstance();
            t24.turbineType = "NREL5MWRef";
            t24.numBladePoints = 40;
            t24.pointDistType = "uniform";
            t24.pointInterpType = PointInterpType.linear;
            t24.bladeUpdateType = BladeUpdateType.oldPosition;
            t24.epsilon = 3.5m;
            t24.tipRootLossCorrType = TipRootLossCorrType.none;
            t24.rotationDir = "cw";
            t24.azimuth = 221.4024m;
            t24.rotSpeed = 11.1548m;
            t24.pitch = 0m;
            t24.nacYaw = 221.617m;
            t24.fluidDensity = 1.23m;
            t24.baseLocation.X = 2343.2546m;
            t24.baseLocation.Y = 2846.6938m;
            t24.baseLocation.Z = 0m;
            ta.turbine.Add(t24);

            var t25 = new TurbineInstance();
            t25.turbineType = "NREL5MWRef";
            t25.numBladePoints = 40;
            t25.pointDistType = "uniform";
            t25.pointInterpType = PointInterpType.linear;
            t25.bladeUpdateType = BladeUpdateType.oldPosition;
            t25.epsilon = 3.5m;
            t25.tipRootLossCorrType = TipRootLossCorrType.none;
            t25.rotationDir = "cw";
            t25.azimuth = 120.357m;
            t25.rotSpeed = 11.3188m;
            t25.pitch = 0m;
            t25.nacYaw = 221.617m;
            t25.fluidDensity = 1.23m;
            t25.baseLocation.X = 2081.1432m;
            t25.baseLocation.Y = 2553.158m;
            t25.baseLocation.Z = 0m;
            ta.turbine.Add(t25);

            var t26 = new TurbineInstance();
            t26.turbineType = "NREL5MWRef";
            t26.numBladePoints = 40;
            t26.pointDistType = "uniform";
            t26.pointInterpType = PointInterpType.linear;
            t26.bladeUpdateType = BladeUpdateType.oldPosition;
            t26.epsilon = 3.5m;
            t26.tipRootLossCorrType = TipRootLossCorrType.none;
            t26.rotationDir = "cw";
            t26.azimuth = 310.0741m;
            t26.rotSpeed = 11.4439m;
            t26.pitch = 0m;
            t26.nacYaw = 221.617m;
            t26.fluidDensity = 1.23m;
            t26.baseLocation.X = 1806.088m;
            t26.baseLocation.Y = 2246.5762m;
            t26.baseLocation.Z = 0m;
            ta.turbine.Add(t26);

            var t27 = new TurbineInstance();
            t27.turbineType = "NREL5MWRef";
            t27.numBladePoints = 40;
            t27.pointDistType = "uniform";
            t27.pointInterpType = PointInterpType.linear;
            t27.bladeUpdateType = BladeUpdateType.oldPosition;
            t27.epsilon = 3.5m;
            t27.tipRootLossCorrType = TipRootLossCorrType.none;
            t27.rotationDir = "cw";
            t27.azimuth = 306.1727m;
            t27.rotSpeed = 11.5522m;
            t27.pitch = 0m;
            t27.nacYaw = 221.617m;
            t27.fluidDensity = 1.23m;
            t27.baseLocation.X = 1278.6292m;
            t27.baseLocation.Y = 1649.7201m;
            t27.baseLocation.Z = 0m;
            ta.turbine.Add(t27);

            var t28 = new TurbineInstance();
            t28.turbineType = "NREL5MWRef";
            t28.numBladePoints = 40;
            t28.pointDistType = "uniform";
            t28.pointInterpType = PointInterpType.linear;
            t28.bladeUpdateType = BladeUpdateType.oldPosition;
            t28.epsilon = 3.5m;
            t28.tipRootLossCorrType = TipRootLossCorrType.none;
            t28.rotationDir = "cw";
            t28.azimuth = 121.3798m;
            t28.rotSpeed = 11.4347m;
            t28.pitch = 0m;
            t28.nacYaw = 221.617m;
            t28.fluidDensity = 1.23m;
            t28.baseLocation.X = 1016.5177m;
            t28.baseLocation.Y = 1359.4458m;
            t28.baseLocation.Z = 0m;
            ta.turbine.Add(t28);

            var t29 = new TurbineInstance();
            t29.turbineType = "NREL5MWRef";
            t29.numBladePoints = 40;
            t29.pointDistType = "uniform";
            t29.pointInterpType = PointInterpType.linear;
            t29.bladeUpdateType = BladeUpdateType.oldPosition;
            t29.epsilon = 3.5m;
            t29.tipRootLossCorrType = TipRootLossCorrType.none;
            t29.rotationDir = "cw";
            t29.azimuth = 182.5907m;
            t29.rotSpeed = 15.7796m;
            t29.pitch = 0.31695m;
            t29.nacYaw = 221.617m;
            t29.fluidDensity = 1.23m;
            t29.baseLocation.X = 750m;
            t29.baseLocation.Y = 1052.864m;
            t29.baseLocation.Z = 0m;
            ta.turbine.Add(t29);

            var t30 = new TurbineInstance();
            t30.turbineType = "NREL5MWRef";
            t30.numBladePoints = 40;
            t30.pointDistType = "uniform";
            t30.pointInterpType = PointInterpType.linear;
            t30.bladeUpdateType = BladeUpdateType.oldPosition;
            t30.epsilon = 3.5m;
            t30.tipRootLossCorrType = TipRootLossCorrType.none;
            t30.rotationDir = "cw";
            t30.azimuth = 155.4715m;
            t30.rotSpeed = 11.3964m;
            t30.pitch = 0m;
            t30.nacYaw = 221.617m;
            t30.fluidDensity = 1.23m;
            t30.baseLocation.X = 2330.3109m;
            t30.baseLocation.Y = 3280.4744m;
            t30.baseLocation.Z = 0m;
            ta.turbine.Add(t30);

            var t31 = new TurbineInstance();
            t31.turbineType = "NREL5MWRef";
            t31.numBladePoints = 40;
            t31.pointDistType = "uniform";
            t31.pointInterpType = PointInterpType.linear;
            t31.bladeUpdateType = BladeUpdateType.oldPosition;
            t31.epsilon = 3.5m;
            t31.tipRootLossCorrType = TipRootLossCorrType.none;
            t31.rotationDir = "cw";
            t31.azimuth = 29.6371m;
            t31.rotSpeed = 11.092m;
            t31.pitch = 0m;
            t31.nacYaw = 221.617m;
            t31.fluidDensity = 1.23m;
            t31.baseLocation.X = 2081.1432m;
            t31.baseLocation.Y = 3003.2462m;
            t31.baseLocation.Z = 0m;
            ta.turbine.Add(t31);

            var t32 = new TurbineInstance();
            t32.turbineType = "NREL5MWRef";
            t32.numBladePoints = 40;
            t32.pointDistType = "uniform";
            t32.pointInterpType = PointInterpType.linear;
            t32.bladeUpdateType = BladeUpdateType.oldPosition;
            t32.epsilon = 3.5m;
            t32.tipRootLossCorrType = TipRootLossCorrType.none;
            t32.rotationDir = "cw";
            t32.azimuth = 145.9968m;
            t32.rotSpeed = 11.3337m;
            t32.pitch = 0m;
            t32.nacYaw = 221.617m;
            t32.fluidDensity = 1.23m;
            t32.baseLocation.X = 1806.088m;
            t32.baseLocation.Y = 2699.9259m;
            t32.baseLocation.Z = 0m;
            ta.turbine.Add(t32);

            var t33 = new TurbineInstance();
            t33.turbineType = "NREL5MWRef";
            t33.numBladePoints = 40;
            t33.pointDistType = "uniform";
            t33.pointInterpType = PointInterpType.linear;
            t33.bladeUpdateType = BladeUpdateType.oldPosition;
            t33.epsilon = 3.5m;
            t33.tipRootLossCorrType = TipRootLossCorrType.none;
            t33.rotationDir = "cw";
            t33.azimuth = 313.0166m;
            t33.rotSpeed = 12.9448m;
            t33.pitch = 0m;
            t33.nacYaw = 221.617m;
            t33.fluidDensity = 1.23m;
            t33.baseLocation.X = 1540.7406m;
            t33.baseLocation.Y = 2406.3901m;
            t33.baseLocation.Z = 0m;
            ta.turbine.Add(t33);

            var t34 = new TurbineInstance();
            t34.turbineType = "NREL5MWRef";
            t34.numBladePoints = 40;
            t34.pointDistType = "uniform";
            t34.pointInterpType = PointInterpType.linear;
            t34.bladeUpdateType = BladeUpdateType.oldPosition;
            t34.epsilon = 3.5m;
            t34.tipRootLossCorrType = TipRootLossCorrType.none;
            t34.rotationDir = "cw";
            t34.azimuth = 286.4719m;
            t34.rotSpeed = 11.3434m;
            t34.pitch = 0m;
            t34.nacYaw = 221.617m;
            t34.fluidDensity = 1.23m;
            t34.baseLocation.X = 1016.5177m;
            t34.baseLocation.Y = 1809.534m;
            t34.baseLocation.Z = 0m;
            ta.turbine.Add(t34);

            var t35 = new TurbineInstance();
            t35.turbineType = "NREL5MWRef";
            t35.numBladePoints = 40;
            t35.pointDistType = "uniform";
            t35.pointInterpType = PointInterpType.linear;
            t35.bladeUpdateType = BladeUpdateType.oldPosition;
            t35.epsilon = 3.5m;
            t35.tipRootLossCorrType = TipRootLossCorrType.none;
            t35.rotationDir = "cw";
            t35.azimuth = 253.5192m;
            t35.rotSpeed = 15.1643m;
            t35.pitch = 0m;
            t35.nacYaw = 221.617m;
            t35.fluidDensity = 1.23m;
            t35.baseLocation.X = 750m;
            t35.baseLocation.Y = 1506.2137m;
            t35.baseLocation.Z = 0m;
            ta.turbine.Add(t35);

            var t36 = new TurbineInstance();
            t36.turbineType = "NREL5MWRef";
            t36.numBladePoints = 40;
            t36.pointDistType = "uniform";
            t36.pointInterpType = PointInterpType.linear;
            t36.bladeUpdateType = BladeUpdateType.oldPosition;
            t36.epsilon = 3.5m;
            t36.tipRootLossCorrType = TipRootLossCorrType.none;
            t36.rotationDir = "cw";
            t36.azimuth = 225.9283m;
            t36.rotSpeed = 10.318m;
            t36.pitch = 0m;
            t36.nacYaw = 221.617m;
            t36.fluidDensity = 1.23m;
            t36.baseLocation.X = 1806.088m;
            t36.baseLocation.Y = 3150.0141m;
            t36.baseLocation.Z = 0m;
            ta.turbine.Add(t36);

            var t37 = new TurbineInstance();
            t37.turbineType = "NREL5MWRef";
            t37.numBladePoints = 40;
            t37.pointDistType = "uniform";
            t37.pointInterpType = PointInterpType.linear;
            t37.bladeUpdateType = BladeUpdateType.oldPosition;
            t37.epsilon = 3.5m;
            t37.tipRootLossCorrType = TipRootLossCorrType.none;
            t37.rotationDir = "cw";
            t37.azimuth = 324.4871m;
            t37.rotSpeed = 10.8428m;
            t37.pitch = 0m;
            t37.nacYaw = 221.617m;
            t37.fluidDensity = 1.23m;
            t37.baseLocation.X = 1543.9765m;
            t37.baseLocation.Y = 2859.7398m;
            t37.baseLocation.Z = 0m;
            ta.turbine.Add(t37);

            var t38 = new TurbineInstance();
            t38.turbineType = "NREL5MWRef";
            t38.numBladePoints = 40;
            t38.pointDistType = "uniform";
            t38.pointInterpType = PointInterpType.linear;
            t38.bladeUpdateType = BladeUpdateType.oldPosition;
            t38.epsilon = 3.5m;
            t38.tipRootLossCorrType = TipRootLossCorrType.none;
            t38.rotationDir = "cw";
            t38.azimuth = 108.4885m;
            t38.rotSpeed = 10.7088m;
            t38.pitch = 0m;
            t38.nacYaw = 221.617m;
            t38.fluidDensity = 1.23m;
            t38.baseLocation.X = 1278.6292m;
            t38.baseLocation.Y = 2553.158m;
            t38.baseLocation.Z = 0m;
            ta.turbine.Add(t38);

            var t39 = new TurbineInstance();
            t39.turbineType = "NREL5MWRef";
            t39.numBladePoints = 40;
            t39.pointDistType = "uniform";
            t39.pointInterpType = PointInterpType.linear;
            t39.bladeUpdateType = BladeUpdateType.oldPosition;
            t39.epsilon = 3.5m;
            t39.tipRootLossCorrType = TipRootLossCorrType.none;
            t39.rotationDir = "cw";
            t39.azimuth = 181.2178m;
            t39.rotSpeed = 10.8782m;
            t39.pitch = 0m;
            t39.nacYaw = 221.617m;
            t39.fluidDensity = 1.23m;
            t39.baseLocation.X = 1016.5177m;
            t39.baseLocation.Y = 2262.8837m;
            t39.baseLocation.Z = 0m;
            ta.turbine.Add(t39);

            var t40 = new TurbineInstance();
            t40.turbineType = "NREL5MWRef";
            t40.numBladePoints = 40;
            t40.pointDistType = "uniform";
            t40.pointInterpType = PointInterpType.linear;
            t40.bladeUpdateType = BladeUpdateType.oldPosition;
            t40.epsilon = 3.5m;
            t40.tipRootLossCorrType = TipRootLossCorrType.none;
            t40.rotationDir = "cw";
            t40.azimuth = 196.9757m;
            t40.rotSpeed = 15.6061m;
            t40.pitch = 0m;
            t40.nacYaw = 221.617m;
            t40.fluidDensity = 1.23m;
            t40.baseLocation.X = 750m;
            t40.baseLocation.Y = 1956.3019m;
            t40.baseLocation.Z = 0m;
            ta.turbine.Add(t40);

            var t41 = new TurbineInstance();
            t41.turbineType = "NREL5MWRef";
            t41.numBladePoints = 40;
            t41.pointDistType = "uniform";
            t41.pointInterpType = PointInterpType.linear;
            t41.bladeUpdateType = BladeUpdateType.oldPosition;
            t41.epsilon = 3.5m;
            t41.tipRootLossCorrType = TipRootLossCorrType.none;
            t41.rotationDir = "cw";
            t41.azimuth = 170.3878m;
            t41.rotSpeed = 11.2228m;
            t41.pitch = 0m;
            t41.nacYaw = 221.617m;
            t41.fluidDensity = 1.23m;
            t41.baseLocation.X = 1540.7406m;
            t41.baseLocation.Y = 3309.828m;
            t41.baseLocation.Z = 0m;
            ta.turbine.Add(t41);

            var t42 = new TurbineInstance();
            t42.turbineType = "NREL5MWRef";
            t42.numBladePoints = 40;
            t42.pointDistType = "uniform";
            t42.pointInterpType = PointInterpType.linear;
            t42.bladeUpdateType = BladeUpdateType.oldPosition;
            t42.epsilon = 3.5m;
            t42.tipRootLossCorrType = TipRootLossCorrType.none;
            t42.rotationDir = "cw";
            t42.azimuth = 37.3263m;
            t42.rotSpeed = 11.5605m;
            t42.pitch = 0m;
            t42.nacYaw = 221.617m;
            t42.fluidDensity = 1.23m;
            t42.baseLocation.X = 1278.6292m;
            t42.baseLocation.Y = 3003.2462m;
            t42.baseLocation.Z = 0m;
            ta.turbine.Add(t42);

            var t43 = new TurbineInstance();
            t43.turbineType = "NREL5MWRef";
            t43.numBladePoints = 40;
            t43.pointDistType = "uniform";
            t43.pointInterpType = PointInterpType.linear;
            t43.bladeUpdateType = BladeUpdateType.oldPosition;
            t43.epsilon = 3.5m;
            t43.tipRootLossCorrType = TipRootLossCorrType.none;
            t43.rotationDir = "cw";
            t43.azimuth = 285.8292m;
            t43.rotSpeed = 10.5842m;
            t43.pitch = 0m;
            t43.nacYaw = 221.617m;
            t43.fluidDensity = 1.23m;
            t43.baseLocation.X = 1016.5177m;
            t43.baseLocation.Y = 2709.7104m;
            t43.baseLocation.Z = 0m;
            ta.turbine.Add(t43);

            var t44 = new TurbineInstance();
            t44.turbineType = "NREL5MWRef";
            t44.numBladePoints = 40;
            t44.pointDistType = "uniform";
            t44.pointInterpType = PointInterpType.linear;
            t44.bladeUpdateType = BladeUpdateType.oldPosition;
            t44.epsilon = 3.5m;
            t44.tipRootLossCorrType = TipRootLossCorrType.none;
            t44.rotationDir = "cw";
            t44.azimuth = 324.9537m;
            t44.rotSpeed = 15.6893m;
            t44.pitch = 0m;
            t44.nacYaw = 221.617m;
            t44.fluidDensity = 1.23m;
            t44.baseLocation.X = 750m;
            t44.baseLocation.Y = 2406.3901m;
            t44.baseLocation.Z = 0m;
            ta.turbine.Add(t44);

            var t45 = new TurbineInstance();
            t45.turbineType = "NREL5MWRef";
            t45.numBladePoints = 40;
            t45.pointDistType = "uniform";
            t45.pointInterpType = PointInterpType.linear;
            t45.bladeUpdateType = BladeUpdateType.oldPosition;
            t45.epsilon = 3.5m;
            t45.tipRootLossCorrType = TipRootLossCorrType.none;
            t45.rotationDir = "cw";
            t45.azimuth = 6.9192m;
            t45.rotSpeed = 12.1541m;
            t45.pitch = 0m;
            t45.nacYaw = 221.617m;
            t45.fluidDensity = 1.23m;
            t45.baseLocation.X = 1278.6292m;
            t45.baseLocation.Y = 3455.9364m;
            t45.baseLocation.Z = 0m;
            ta.turbine.Add(t45);

            var t46 = new TurbineInstance();
            t46.turbineType = "NREL5MWRef";
            t46.numBladePoints = 40;
            t46.pointDistType = "uniform";
            t46.pointInterpType = PointInterpType.linear;
            t46.bladeUpdateType = BladeUpdateType.oldPosition;
            t46.epsilon = 3.5m;
            t46.tipRootLossCorrType = TipRootLossCorrType.none;
            t46.rotationDir = "cw";
            t46.azimuth = 265.2855m;
            t46.rotSpeed = 11.5378m;
            t46.pitch = 0m;
            t46.nacYaw = 221.617m;
            t46.fluidDensity = 1.23m;
            t46.baseLocation.X = 1013.2818m;
            t46.baseLocation.Y = 3166.3216m;
            t46.baseLocation.Z = 0m;
            ta.turbine.Add(t46);

            var t47 = new TurbineInstance();
            t47.turbineType = "NREL5MWRef";
            t47.numBladePoints = 40;
            t47.pointDistType = "uniform";
            t47.pointInterpType = PointInterpType.linear;
            t47.bladeUpdateType = BladeUpdateType.oldPosition;
            t47.epsilon = 3.5m;
            t47.tipRootLossCorrType = TipRootLossCorrType.none;
            t47.rotationDir = "cw";
            t47.azimuth = 3.1977m;
            t47.rotSpeed = 15.4079m;
            t47.pitch = 0m;
            t47.nacYaw = 221.617m;
            t47.fluidDensity = 1.23m;
            t47.baseLocation.X = 750m;
            t47.baseLocation.Y = 2859.7398m;
            t47.baseLocation.Z = 0m;
            ta.turbine.Add(t47);
        }
        #endregion
       
    }
}
