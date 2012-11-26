using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.Products.OpenFoam.Models.PolyMesh;
using Offwind.Products.OpenFoam.Models.SnappyHexMesh;
using Offwind.Products.WindWave;
using Offwind.Sowfa;
using Offwind.Sowfa.Constant.AblProperties;
using Offwind.Sowfa.Constant.AirfoilProperties;
using Offwind.Sowfa.Constant.Gravitation;
using Offwind.Sowfa.Constant.Omega;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
using Offwind.Sowfa.Constant.TurbineArrayPropertiesFAST;
using Offwind.Sowfa.Constant.TurbineProperties;
using Offwind.Sowfa.System.ControlDict;
using Offwind.Sowfa.System.FvSchemes;
using Offwind.Sowfa.System.FvSolution;
using Offwind.Sowfa.System.RefineMeshDict;
using Offwind.Sowfa.System.SetFieldsAblDict;
using Offwind.Sowfa.System.TopoSetDict;
using Offwind.Sowfa.Time.FieldData;

namespace Offwind.Tests
{
    [TestFixture]
    public class TestFileHandlers
    {
        private string _path;
        private Random _rnd;

        [SetUp]
        public void Init()
        {
            _path = Path.GetTempFileName();
            _rnd = new Random();
        }

        [TearDown]
        public void Cleanup()
        {
            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        private string RandomString(bool allowEmpty = false)
        {
            if (allowEmpty && _rnd.Next(2) == 2) return "";
            return "s" + _rnd.Next().ToString();
        }

        private bool RandomBool()
        {
            return _rnd.Next(2) == 1;
        }

        private T RandomEnum<T>(params object[] restricted)
        {
            int max = Enum.GetNames(typeof (T)).Length;
            while (true)
            {
                var sv = (T)((object)_rnd.Next(max));
                var isRestricted = false;
                foreach (var r in restricted)
                {
                    if (r.Equals(sv))
                    {
                        isRestricted = true;
                        break;
                    }
                }
                if (isRestricted) continue;
                return sv;
            }
        }

        private List<T> RandomArray<T>(int minItems, int maxItems, Func<T> valf)
        {
            var n = _rnd.Next(minItems, maxItems);
            var result = new List<T>(n);
            for (int i = 0; i < n; i++)
            {
                result.Add(valf());
            }
            return result;
        }

        private Vertice RandomVector()
        {
            var v = new Vertice();
            v.X = (decimal)_rnd.NextDouble();
            v.Y = (decimal)_rnd.NextDouble();
            v.Z = (decimal)_rnd.NextDouble();
            return v;
        }

        [Test]
        [Ignore]
        public void ToolGenerate()
        {
            var type = typeof(Dimensions);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                if (info.PropertyType == typeof(bool))
                    Console.WriteLine("t.Replace(\"({{[[{0}]]}})\", d.{0}.ToString().ToLowerInvariant());", info.Name);
                else
                    Console.WriteLine("t.Replace(\"({{[[{0}]]}})\", d.{0}.ToString());", info.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.WriteLine("case \"{0}\":", info.Name);
                if (info.PropertyType == typeof(decimal))
                    Console.Write("rawData.{0} = rootEntryNode.GetBasicValDecimal();", info.Name);
                else if (info.PropertyType == typeof(bool))
                    Console.Write("rawData.{0} = rootEntryNode.GetBasicValBool();", info.Name);
                Console.WriteLine("");
                Console.WriteLine("break;");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.Write("d1.{0} = ", info.Name);
                if (info.PropertyType == typeof(decimal))
                    Console.Write("(decimal)_rnd.NextDouble();");
                else if (info.PropertyType == typeof(int))
                    Console.Write("_rnd.Next();");
                else if (info.PropertyType == typeof(bool))
                    Console.Write("RandomBool();");
                else if (info.PropertyType == typeof(string))
                    Console.Write("RandomString();");
                else if (info.PropertyType.IsEnum)
                    Console.Write("RandomEnum<{0}>();", info.PropertyType.Name);
                Console.WriteLine("");
            }
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.WriteLine("Assert.AreEqual(d1.{0}, d2.{0});", info.Name);
            }
        }

        [Test]
        [Ignore]
        public void ToolGenerateModelUpdate()
        {
            var type = typeof(VWindWave);
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.WriteLine("_model.{0} = d.{0};", info.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.WriteLine("d.{0} = _model.{0};", info.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                Console.WriteLine("txt{0}.DataBindings.Add(\"EditValue\", _model, \"{0}\", true, DataSourceUpdateMode.OnPropertyChanged);", info.Name);
            }
        }

        [Test]
        public void AblHandler()
        {
            var handler = new AblPropertiesHandler();

            var d1 = new AblPropertiesData();
            d1.alpha = _rnd.Next();
            d1.corrAvgStartTime = _rnd.Next();
            d1.driveWindOn = RandomBool();
            d1.HWindDim.ScalarValue = _rnd.Next();
            d1.lowerBoundaryName = RandomString();
            d1.meanAvgStartTime = _rnd.Next();
            d1.statisticsFrequency = _rnd.Next();
            d1.statisticsOn = RandomBool();
            d1.turbineArrayOn = RandomBool();
            d1.UWindDir = _rnd.Next();
            d1.UWindSpeedDim.ScalarValue = _rnd.Next();
            d1.upperBoundaryName = RandomString();
            handler.Write(_path, d1);

            var d2 = (AblPropertiesData)handler.Read(_path);
            Assert.AreEqual(d1.alpha, d2.alpha);
            Assert.AreEqual(d1.corrAvgStartTime, d2.corrAvgStartTime);
            Assert.AreEqual(d1.driveWindOn, d2.driveWindOn);
            Assert.AreEqual(d1.HWindDim.ScalarValue, d2.HWindDim.ScalarValue);
            Assert.AreEqual(d1.lowerBoundaryName, d2.lowerBoundaryName);
            Assert.AreEqual(d1.meanAvgStartTime, d2.meanAvgStartTime);
            Assert.AreEqual(d1.statisticsFrequency, d2.statisticsFrequency);
            Assert.AreEqual(d1.statisticsOn, d2.statisticsOn);
            Assert.AreEqual(d1.turbineArrayOn, d2.turbineArrayOn);
            Assert.AreEqual(d1.UWindDir, d2.UWindDir);
            Assert.AreEqual(d1.UWindSpeedDim.ScalarValue, d2.UWindSpeedDim.ScalarValue);
            Assert.AreEqual(d1.upperBoundaryName, d2.upperBoundaryName);
        }

        [Test]
        public void GravitationHandler()
        {
            var handler = new GravitationHandler();

            var d1 = new Vertice();
            d1.X = (decimal)_rnd.NextDouble();
            d1.Y = (decimal)_rnd.NextDouble();
            d1.Z = (decimal)_rnd.NextDouble();
            handler.Write(_path, d1);
            var d2 = (Vertice)handler.Read(_path);
            Assert.AreEqual(d1.X, d2.X);
            Assert.AreEqual(d1.Y, d2.Y);
            Assert.AreEqual(d1.Z, d2.Z);
        }

        [Test]
        public void OmegaHandler()
        {
            var handler = new OmegaHandler();

            var d1 = new Vertice();
            d1.X = (decimal)_rnd.NextDouble();
            d1.Y = (decimal)_rnd.NextDouble();
            d1.Z = (decimal)_rnd.NextDouble();
            handler.Write(_path, d1);
            var d2 = (Vertice)handler.Read(_path);
            Assert.AreEqual(d1.X, d2.X);
            Assert.AreEqual(d1.Y, d2.Y);
            Assert.AreEqual(d1.Z, d2.Z);
        }

        [Test]
        public void TransportPropertiesHandler()
        {
            var handler = new TransportPropertiesHandler();

            var d1 = new TransportPropertiesData();
            d1.transportModel = RandomEnum<TransportModel>();
            d1.nu = (decimal)_rnd.NextDouble();
            d1.TRef = (decimal)_rnd.NextDouble();
            d1.LESModel = RandomEnum<LesModel>();
            d1.Cs = (decimal)_rnd.NextDouble();
            d1.deltaLESCoeff = (decimal)_rnd.NextDouble();
            d1.kappa = (decimal)_rnd.NextDouble();
            d1.betaM = (decimal)_rnd.NextDouble();
            d1.gammM = (decimal)_rnd.NextDouble();
            d1.z0 = (decimal)_rnd.NextDouble();
            d1.q0 = (decimal)_rnd.NextDouble();
            d1.surfaceStressModel = RandomEnum<SurfaceStressModel>();
            d1.betaSurfaceStress = (decimal)_rnd.NextDouble();
            handler.Write(_path, d1);

            var d2 = (TransportPropertiesData)handler.Read(_path);
            Assert.AreEqual(d1.transportModel, d2.transportModel);
            Assert.AreEqual(d1.nu, d2.nu);
            Assert.AreEqual(d1.TRef, d2.TRef);
            Assert.AreEqual(d1.LESModel, d2.LESModel);
            Assert.AreEqual(d1.Cs, d2.Cs);
            Assert.AreEqual(d1.deltaLESCoeff, d2.deltaLESCoeff);
            Assert.AreEqual(d1.kappa, d2.kappa);
            Assert.AreEqual(d1.betaM, d2.betaM);
            Assert.AreEqual(d1.gammM, d2.gammM);
            Assert.AreEqual(d1.z0, d2.z0);
            Assert.AreEqual(d1.q0, d2.q0);
            Assert.AreEqual(d1.surfaceStressModel, d2.surfaceStressModel);
            Assert.AreEqual(d1.betaSurfaceStress, d2.betaSurfaceStress);

            Trace.WriteLine(d1.transportModel);
            Trace.WriteLine(d1.LESModel);
            Trace.WriteLine(d1.surfaceStressModel);
        }

        [Test]
        public void ControlDictHandler()
        {
            var handler = new ControlDictHandler();

            var d1 = new ControlDictData();
            d1.application = RandomEnum<ApplicationSolver>();
            d1.startFrom = RandomEnum<StartFrom>();
            d1.startTime = (decimal)_rnd.NextDouble();
            d1.stopAt = RandomEnum<StopAt>();
            d1.endTime = (decimal)_rnd.NextDouble();
            d1.deltaT = (decimal)_rnd.NextDouble();
            d1.writeControl = RandomEnum<WriteControl>();
            d1.writeInterval = (decimal)_rnd.NextDouble();
            d1.purgeWrite = (decimal)_rnd.NextDouble();
            d1.writeFormat = RandomEnum<WriteFormat>();
            d1.writePrecision = (decimal)_rnd.NextDouble();
            d1.writeCompression = RandomEnum<WriteCompression>();
            d1.timeFormat = RandomEnum<TimeFormat>();
            d1.timePrecision = (decimal)_rnd.NextDouble();
            d1.runTimeModifiable = RandomEnum<FlagYesNo>();
            d1.adjustTimeStep = RandomEnum<FlagYesNo>();
            d1.maxCo = (decimal)_rnd.NextDouble();
            d1.maxDeltaT = (decimal)_rnd.NextDouble();
            handler.Write(_path, d1);

            var d2 = (ControlDictData)handler.Read(_path);
            Assert.AreEqual(d1.application, d2.application);
            Assert.AreEqual(d1.startFrom, d2.startFrom);
            Assert.AreEqual(d1.startTime, d2.startTime);
            Assert.AreEqual(d1.stopAt, d2.stopAt);
            Assert.AreEqual(d1.endTime, d2.endTime);
            Assert.AreEqual(d1.deltaT, d2.deltaT);
            Assert.AreEqual(d1.writeControl, d2.writeControl);
            Assert.AreEqual(d1.writeInterval, d2.writeInterval);
            Assert.AreEqual(d1.purgeWrite, d2.purgeWrite);
            Assert.AreEqual(d1.writeFormat, d2.writeFormat);
            Assert.AreEqual(d1.writePrecision, d2.writePrecision);
            Assert.AreEqual(d1.writeCompression, d2.writeCompression);
            Assert.AreEqual(d1.timeFormat, d2.timeFormat);
            Assert.AreEqual(d1.timePrecision, d2.timePrecision);
            Assert.AreEqual(d1.runTimeModifiable, d2.runTimeModifiable);
            Assert.AreEqual(d1.adjustTimeStep, d2.adjustTimeStep);
            Assert.AreEqual(d1.maxCo, d2.maxCo);
            Assert.AreEqual(d1.maxDeltaT, d2.maxDeltaT);

            Trace.WriteLine(d1.application);
            Trace.WriteLine(d1.startFrom);
            Trace.WriteLine(d1.stopAt);
            Trace.WriteLine(d1.writeControl);
            Trace.WriteLine(d1.writeFormat);
            Trace.WriteLine(d1.writeCompression);
            Trace.WriteLine(d1.timeFormat);
            Trace.WriteLine(d1.runTimeModifiable);
            Trace.WriteLine(d1.adjustTimeStep);
        }

        [Test]
        public void FvSolution()
        {
            var handler = new FvSolutionHandler();

            var d1 = new FvSolutionData();
            d1.Options.nCorrectors = _rnd.Next();
            d1.Options.nNonOrthogonalCorrectors = _rnd.Next();
            d1.Options.pdRefOn = RandomBool();
            d1.Options.pdRefCell = _rnd.Next();
            d1.Options.pdRefValue = _rnd.Next();
            d1.Options.tempEqnOn = RandomBool();

            var solversNumber = _rnd.Next(10);
            for (int i = 0; i < solversNumber; i++)
            {
                var d1s = new MLinearSolver();
                d1s.Name = RandomString();
                d1s.solver = RandomEnum<LinearSolver>();
                d1s.agglomerator = RandomEnum<Agglomerator>();
                d1s.preconditioner = RandomEnum<Preconditioner>();
                d1s.smoother = RandomEnum<Smoother>();
                d1s.cacheAgglomeration = RandomBool();
                d1s.mergeLevels = _rnd.Next();
                d1s.nCellsInCoarsestLevel = _rnd.Next();
                d1s.nFinestSweeps = _rnd.Next();
                d1s.nPostSweeps = _rnd.Next();
                d1s.nPreSweeps = _rnd.Next();
                d1s.relTol = (decimal)_rnd.NextDouble();
                d1s.tolerance = (decimal)_rnd.NextDouble();
                d1.Solvers.Add(d1s);
            }
            handler.Write(_path, d1);

            var d2 = (FvSolutionData)handler.Read(_path);
            Assert.AreEqual(d1.Options.nCorrectors, d2.Options.nCorrectors);
            Assert.AreEqual(d1.Options.nNonOrthogonalCorrectors, d2.Options.nNonOrthogonalCorrectors);
            Assert.AreEqual(d1.Options.pdRefOn, d2.Options.pdRefOn);
            Assert.AreEqual(d1.Options.pdRefCell, d2.Options.pdRefCell);
            Assert.AreEqual(d1.Options.pdRefValue, d2.Options.pdRefValue);
            Assert.AreEqual(d1.Options.tempEqnOn, d2.Options.tempEqnOn);

            Assert.AreEqual(d1.Solvers.Count, d2.Solvers.Count);
            for (int i = 0; i < d1.Solvers.Count; i++)
            {
                var s1 = d1.Solvers[i];
                var s2 = d2.Solvers[i];
                Assert.AreEqual(s1.Name, s2.Name);
                Assert.AreEqual(s1.solver, s2.solver);
                Trace.WriteLine(String.Format("S#{0}: {1}", i, s1.solver));
            }
        }

        [Test]
        public void BlockMeshDict()
        {
            var handler = new BlockMeshDictHandler();

            var d1 = new BlockMeshDictData();
            d1.convertToMeters = (decimal)_rnd.NextDouble();
            d1.MeshBlocks.grading = RandomEnum<Grading>();
            d1.vertices = RandomArray(9, 10, RandomVector);
            d1.MeshBlocks.vertexNumbers = RandomArray(5, 10, _rnd.Next);
            d1.MeshBlocks.numberOfCells = RandomArray(5, 10, _rnd.Next);
            d1.MeshBlocks.gradingNumbers = RandomArray(5, 10, _rnd.Next);
            Func<string, MeshBoundary> randB = (s) =>
            {
                var b = new MeshBoundary();
                b.Name = RandomString();
                b.Type = RandomEnum<PatchType>();
                b.Faces = RandomArray(4, 4, () => RandomArray(4, 6, () => _rnd.Next(10)));
                b.NeighbourPatch = s;
                return b;
            };
            d1.boundaries = RandomArray(5, 10, () => randB(RandomString()));
            d1.boundaries.Add(randB(null));
            d1.boundaries.Add(randB(null));

            handler.Write(_path, d1);

            var d2 = (BlockMeshDictData)handler.Read(_path);
            Assert.AreEqual(d1.convertToMeters, d2.convertToMeters);

            Assert.AreEqual(d1.MeshBlocks.grading, d2.MeshBlocks.grading);
            Assert.AreEqual(d1.MeshBlocks.gradingNumbers, d2.MeshBlocks.gradingNumbers);
            Assert.AreEqual(d1.MeshBlocks.numberOfCells, d2.MeshBlocks.numberOfCells);
            Assert.AreEqual(d1.MeshBlocks.vertexNumbers, d2.MeshBlocks.vertexNumbers);

            Assert.AreEqual(d1.vertices.Count, d2.vertices.Count);
            if (d1.vertices.Count > 0)
            {
                for (int i = 0; i < d1.vertices.Count; i++)
                {
                    var v1 = d1.vertices[i];
                    var v2 = d2.vertices[i];
                    Assert.AreEqual(v1.X, v2.X);
                    Assert.AreEqual(v1.Y, v2.Y);
                    Assert.AreEqual(v1.Z, v2.Z);
                }
            }

            Assert.AreEqual(d1.boundaries.Count, d2.boundaries.Count);
            if (d1.boundaries.Count > 0)
            {
                for (int i = 0; i < d1.boundaries.Count; i++)
                {
                    var b1 = d1.boundaries[i];
                    var b2 = d2.boundaries[i];
                    Assert.AreEqual(b1.Name, b2.Name);
                    Assert.AreEqual(b1.Type, b2.Type);
                    Assert.AreEqual(b1.NeighbourPatch, b2.NeighbourPatch);
                    Assert.AreEqual(b1.Faces.Count, b2.Faces.Count);

                    if (b1.Faces.Count > 0)
                    {
                        for (int j = 0; j < b1.Faces.Count; j++)
                        {
                            Assert.AreEqual(b1.Faces[j], b2.Faces[j]);
                        }
                    }
                }
            }
        }

        [Test]
        [Ignore]
        public void TopoSetDictHandler()
        {
            var handler = new TopoSetDictHandler();

            var d1 = new TopoSetDictData();
            d1.X1 = (decimal)_rnd.NextDouble();
            d1.Y1 = (decimal)_rnd.NextDouble();
            d1.Z1 = (decimal)_rnd.NextDouble();
            d1.X2 = (decimal)_rnd.NextDouble();
            d1.Y2 = (decimal)_rnd.NextDouble();
            d1.Z2 = (decimal)_rnd.NextDouble();
            
            handler.Write(_path, d1);

            var d2 = (TopoSetDictData)handler.Read(_path);
            Assert.AreEqual(d1.X1, d2.X1);
            Assert.AreEqual(d1.Y1, d2.Y1);
            Assert.AreEqual(d1.Z1, d2.Z1);
            Assert.AreEqual(d1.X2, d2.X2);
            Assert.AreEqual(d1.Y2, d2.Y2);
            Assert.AreEqual(d1.Z2, d2.Z2);
        }

        [Test]
        public void SetFieldsAblDict()
        {
            var handler = new SetFieldsAblDictHandler();

            var d1 = new SetFieldsAblDictData();
            d1.xMax = (decimal)_rnd.NextDouble();
            d1.yMax = (decimal)_rnd.NextDouble();
            d1.zMax = (decimal)_rnd.NextDouble();
            d1.logInit = RandomBool();
            d1.deltaU = (decimal)_rnd.NextDouble();
            d1.deltaV = (decimal)_rnd.NextDouble();
            d1.Uperiods = (decimal)_rnd.NextDouble();
            d1.Vperiods = (decimal)_rnd.NextDouble();
            d1.zPeak = (decimal)_rnd.NextDouble();
            d1.zInversion = (decimal)_rnd.NextDouble();
            d1.widthInversion = (decimal)_rnd.NextDouble();
            d1.Tbottom = (decimal)_rnd.NextDouble();
            d1.Ttop = (decimal)_rnd.NextDouble();
            d1.dTdz = (decimal)_rnd.NextDouble();
            d1.Ug = (decimal)_rnd.NextDouble();
            d1.UgDir = (decimal)_rnd.NextDouble();
            d1.z0 = (decimal)_rnd.NextDouble();
            d1.kappa = (decimal)_rnd.NextDouble();
            d1.updateInternalFields = RandomBool();
            d1.updateBoundaryFields = RandomBool();
            handler.Write(_path, d1);

            var d2 = (SetFieldsAblDictData)handler.Read(_path);
            Assert.AreEqual(d1.xMax, d2.xMax);
            Assert.AreEqual(d1.yMax, d2.yMax);
            Assert.AreEqual(d1.zMax, d2.zMax);
            Assert.AreEqual(d1.logInit, d2.logInit);
            Assert.AreEqual(d1.deltaU, d2.deltaU);
            Assert.AreEqual(d1.deltaV, d2.deltaV);
            Assert.AreEqual(d1.Uperiods, d2.Uperiods);
            Assert.AreEqual(d1.Vperiods, d2.Vperiods);
            Assert.AreEqual(d1.zPeak, d2.zPeak);
            Assert.AreEqual(d1.zInversion, d2.zInversion);
            Assert.AreEqual(d1.widthInversion, d2.widthInversion);
            Assert.AreEqual(d1.Tbottom, d2.Tbottom);
            Assert.AreEqual(d1.Ttop, d2.Ttop);
            Assert.AreEqual(d1.dTdz, d2.dTdz);
            Assert.AreEqual(d1.Ug, d2.Ug);
            Assert.AreEqual(d1.UgDir, d2.UgDir);
            Assert.AreEqual(d1.z0, d2.z0);
            Assert.AreEqual(d1.kappa, d2.kappa);
            Assert.AreEqual(d1.updateInternalFields, d2.updateInternalFields);
            Assert.AreEqual(d1.updateBoundaryFields, d2.updateBoundaryFields);
        }

        [Test]
        public void FieldDataHandler()
        {
            var handler = new FieldDataHandler();

            var d1 = new FieldData();
            d1.FieldFormat = RandomEnum<Format>();
            d1.FieldClass = RandomEnum<FieldClass>();
            d1.FieldLocation = RandomString();
            d1.FieldObject = RandomString();
            d1.InternalFieldType = RandomEnum<FieldType>(FieldType.undefined);
            d1.InternalFieldValue = RandomArray(1, 4, () => (decimal)_rnd.NextDouble()).ToArray();
            d1.Dimensions.Mass = (decimal)_rnd.NextDouble();
            d1.Dimensions.Length = (decimal)_rnd.NextDouble();
            d1.Dimensions.Time = (decimal)_rnd.NextDouble();
            d1.Dimensions.Temperature = (decimal)_rnd.NextDouble();
            d1.Dimensions.Quantity = (decimal)_rnd.NextDouble();
            d1.Dimensions.Current = (decimal)_rnd.NextDouble();
            d1.Dimensions.LuminousIntensity = (decimal)_rnd.NextDouble();
            Func<string, BoundaryPatch> randBP = (s) =>
            {
                var b = new BoundaryPatch();
                b.Name = RandomString();
                b.Rho = RandomString(true);
                b.PatchType = RandomEnum<PatchType>(PatchType.undefined);
                b.GradientFieldType = RandomEnum<FieldType>();

                if (b.GradientFieldType != FieldType.undefined)
                    b.GradientValue = RandomArray(1, 4, _rnd.Next).Select(i => (decimal)i).ToArray();

                b.ValueFieldType = RandomEnum<FieldType>();
                if (b.ValueFieldType != FieldType.undefined)
                    b.ValueValue = RandomArray(1, 4, _rnd.Next).Select(i => (decimal)i).ToArray();

                return b;
            };
            d1.Patches = RandomArray(5, 15, () => randBP(RandomString()));

            handler.Write(_path, d1);
            var d2 = (FieldData)handler.Read(_path);
            Assert.AreEqual(d1.FieldFormat, d2.FieldFormat);
            Assert.AreEqual(d1.FieldLocation, d2.FieldLocation);
            Assert.AreEqual(d1.FieldClass, d2.FieldClass);
            Assert.AreEqual(d1.FieldObject, d2.FieldObject);
            Assert.AreEqual(d1.InternalFieldType, d2.InternalFieldType);
            Assert.AreEqual(d1.InternalFieldValue, d2.InternalFieldValue);
            Assert.AreEqual(d1.Dimensions.Mass, d2.Dimensions.Mass);
            Assert.AreEqual(d1.Dimensions.Length, d2.Dimensions.Length);
            Assert.AreEqual(d1.Dimensions.Time, d2.Dimensions.Time);
            Assert.AreEqual(d1.Dimensions.Temperature, d2.Dimensions.Temperature);
            Assert.AreEqual(d1.Dimensions.Quantity, d2.Dimensions.Quantity);
            Assert.AreEqual(d1.Dimensions.Current, d2.Dimensions.Current);
            Assert.AreEqual(d1.Dimensions.LuminousIntensity, d2.Dimensions.LuminousIntensity);
            Assert.AreEqual(d1.Patches.Count, d2.Patches.Count);
            if (d1.Patches.Count > 0)
            {
                for (int i = 0; i < d1.Patches.Count; i++)
                {
                    var b1 = d1.Patches[i];
                    var b2 = d2.Patches[i];
                    Assert.AreEqual(b1.Name, b2.Name);
                    Assert.AreEqual(b1.Rho, b2.Rho);
                    Assert.AreEqual(b1.PatchType, b2.PatchType);
                    Assert.AreEqual(b1.GradientFieldType, b2.GradientFieldType);
                    Assert.AreEqual(b1.ValueFieldType, b2.ValueFieldType);
                    Assert.AreEqual(b1.GradientValue.Length, b2.GradientValue.Length);
                    Assert.AreEqual(b1.ValueValue.Length, b2.ValueValue.Length);
                    Assert.AreEqual(b1.GradientValue, b2.GradientValue);
                    Assert.AreEqual(b1.ValueValue, b2.ValueValue);
                }
            }
        }

        [Test]
        [Ignore]
        public void WriteDefault()
        {
            var dir = Path.GetTempPath();
            dir = Path.Combine(dir, Path.GetRandomFileName().Replace(".", ""));
            var handlers = new List<FoamFileHandler>();
            foreach (var type in GetTypes<FoamFileHandler>(Assembly.GetAssembly(typeof(SowfaConstants))))
            {
                var descriptor = (FoamFileHandler)Activator.CreateInstance(type);
                handlers.Add(descriptor);
            }

            foreach (var handler in handlers)
            {
                handler.WriteDefault(dir, null);
            }
            Trace.WriteLine(dir);
            Assert.True(Directory.Exists(dir));
        }

        private static IEnumerable<Type> GetTypes<T>(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsInterface) continue;
                if (type.IsAbstract) continue;
                if (typeof(T).IsAssignableFrom(type))
                    yield return type;
            }
        }

        [Test]
        public void TurbineArrayProperties()
        {
            var t_data = new TurbineArrayPropData();
            var handler = new TurbineArrayPropHandler();
            //handler.SetPath(_path);

            t_data.outputControl = RandomEnum<OutputControl>();
            t_data.outputInterval = (decimal) _rnd.NextDouble();
            for (int i = 0; i < 4; i++)
            {
                var t_item = new TurbineInstance();
                t_item.azimuth = (decimal)_rnd.NextDouble();
                t_item.baseLocation = RandomVector();
                t_item.bladeUpdateType = RandomEnum<BladeUpdateType>();
                t_item.epsilon = (decimal)_rnd.NextDouble();
                t_item.fluidDensity = (decimal)_rnd.NextDouble();
                t_item.nacYaw = (decimal)_rnd.NextDouble();
                t_item.numBladePoints = (decimal)_rnd.NextDouble();
                t_item.pitch = (decimal)_rnd.NextDouble();
                t_item.pointDistType = RandomString();
                t_item.pointInterpType = RandomEnum<PointInterpType>();
                t_item.rotSpeed = (decimal)_rnd.NextDouble();
                t_item.rotationDir = RandomString();
                t_item.tipRootLossCorrType = RandomEnum<TipRootLossCorrType>();
                t_item.turbineType = "turbine_type_" + i;
                t_data.turbine.Add(t_item);
                
            }
            handler.Write(_path, t_data);

            var o_data = (TurbineArrayPropData) handler.Read(_path);
            Assert.AreEqual(t_data.outputControl, o_data.outputControl);
            Assert.AreEqual(t_data.outputInterval, o_data.outputInterval);
            Assert.IsNotNull(o_data.turbine);
            Assert.AreEqual(t_data.turbine.Count, o_data.turbine.Count);
            for (int i = 0; i < o_data.turbine.Count; i++)
            {
                var d0 = t_data.turbine[i];
                var d1 = o_data.turbine[i];
                Assert.AreEqual(d0.epsilon,d1.epsilon);
                Assert.AreEqual(d0.azimuth, d1.azimuth);
                Assert.IsNotNull(d1.baseLocation);
                Assert.AreEqual(d0.baseLocation.X, d1.baseLocation.X);
                Assert.AreEqual(d0.baseLocation.Y, d1.baseLocation.Y);
                Assert.AreEqual(d0.baseLocation.Z, d1.baseLocation.Z);
                Assert.AreEqual(d0.bladeUpdateType, d1.bladeUpdateType);
                Assert.AreEqual(d0.fluidDensity, d1.fluidDensity);
                Assert.AreEqual(d0.nacYaw, d1.nacYaw);
                Assert.AreEqual(d0.numBladePoints, d1.numBladePoints);
                Assert.AreEqual(d0.pitch, d1.pitch);
                Assert.AreEqual(d0.pointDistType, d1.pointDistType);
                Assert.AreEqual(d0.pointInterpType, d1.pointInterpType);
                Assert.AreEqual(d0.rotSpeed, d1.rotSpeed);
                Assert.AreEqual(d0.rotationDir, d1.rotationDir);
                Assert.AreEqual(d0.tipRootLossCorrType, d1.tipRootLossCorrType);
                Assert.AreEqual(d0.turbineType, d1.turbineType);
            }
        }

        [Test]
        public void TurbineProperties()
        {
            var d0      = new TurbinePropertiesData();
            var handler = new TurbineProperiesHandler("super_turbine_1", true);

            d0.BladeIner = (decimal) _rnd.NextDouble();
            d0.GBRatio = (decimal) _rnd.NextDouble();
            d0.GenIner = (decimal) _rnd.NextDouble();
            d0.HubIner = (decimal)_rnd.NextDouble();
            d0.HubRad = (decimal)_rnd.NextDouble();
            d0.NumBl = (int) _rnd.NextDouble()*100;
            d0.OverHang = (decimal)_rnd.NextDouble();
            d0.PitchControllerType = RandomEnum<ControllerType>();
            d0.PreCone = RandomVector();
            d0.ShftTilt = (decimal)_rnd.NextDouble();
            d0.TipRad = (decimal)_rnd.NextDouble();
            d0.TorqueControllerType = RandomEnum<ControllerType>();
            d0.TowerHt = (decimal)_rnd.NextDouble();
            d0.Twr2Shft = (decimal)_rnd.NextDouble();
            d0.UndSling = (decimal)_rnd.NextDouble();
            d0.YawControllerType = RandomEnum<ControllerType>();

            d0.pitchControllerParams = new PitchControllerParams();
            d0.pitchControllerParams.PitchControlEndPitch = (decimal)_rnd.NextDouble();
            d0.pitchControllerParams.PitchControlEndSpeed = (decimal)_rnd.NextDouble();
            d0.pitchControllerParams.PitchControlStartPitch = (decimal)_rnd.NextDouble();
            d0.pitchControllerParams.PitchControlStartSpeed = (decimal)_rnd.NextDouble();
            d0.pitchControllerParams.RateLimitPitch = (decimal)_rnd.NextDouble();

            d0.torqueControllerParams = new TorqueControllerParams();
            d0.torqueControllerParams.CutInGenSpeed = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.CutInGenTorque = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.KGen = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.RateLimitGenTorque = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.RatedGenSpeed = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.RatedGenTorque = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.Region2EndGenSpeed = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.Region2StartGenSpeed = (decimal)_rnd.NextDouble();
            d0.torqueControllerParams.TorqueControllerRelax = (decimal)_rnd.NextDouble();

            for (int i = 0; i < 16; i++)
            {
                var item = new AirfoilBlade();
                item.AirfoilName = RandomString();
                item.Blade = new List<Vertice>();

                for (int j = 0; j < 4; j++)
                {
                    item.Blade.Add(RandomVector());
                }                                
                d0.airfoilBlade.Add(item);
            }

            handler.Write( _path, d0 );

            var d1 = (TurbinePropertiesData) handler.Read(_path);

            Assert.AreEqual(d1.BladeIner, d0.BladeIner);
            Assert.AreEqual(d1.GBRatio, d0.GBRatio);
            Assert.AreEqual(d1.GenIner, d0.GenIner);
            Assert.AreEqual(d1.HubIner, d0.HubIner);
            Assert.AreEqual(d1.HubRad, d0.HubRad);
            Assert.AreEqual(d1.NumBl, d0.NumBl);
            Assert.AreEqual(d1.OverHang, d0.OverHang);
            Assert.AreEqual(d1.PitchControllerType, d0.PitchControllerType);
            Assert.AreEqual(d1.PreCone.X, d0.PreCone.X);
            Assert.AreEqual(d1.PreCone.Y, d0.PreCone.Y);
            Assert.AreEqual(d1.PreCone.Z, d0.PreCone.Z);
            Assert.AreEqual(d1.ShftTilt, d0.ShftTilt);
            Assert.AreEqual(d1.TipRad, d0.TipRad);
            Assert.AreEqual(d1.TorqueControllerType, d0.TorqueControllerType);
            Assert.AreEqual(d1.TowerHt, d0.TowerHt);
            Assert.AreEqual(d1.Twr2Shft, d0.Twr2Shft);
            Assert.AreEqual(d1.UndSling, d0.UndSling);
            Assert.AreEqual(d1.YawControllerType, d0.YawControllerType);

            Assert.AreEqual(d1.pitchControllerParams.PitchControlEndPitch, d0.pitchControllerParams.PitchControlEndPitch);
            Assert.AreEqual(d1.pitchControllerParams.PitchControlEndSpeed, d0.pitchControllerParams.PitchControlEndSpeed);
            Assert.AreEqual(d1.pitchControllerParams.PitchControlStartPitch, d0.pitchControllerParams.PitchControlStartPitch);
            Assert.AreEqual(d1.pitchControllerParams.PitchControlStartSpeed, d0.pitchControllerParams.PitchControlStartSpeed);
            Assert.AreEqual(d1.pitchControllerParams.RateLimitPitch, d0.pitchControllerParams.RateLimitPitch);
            
            Assert.AreEqual(d1.torqueControllerParams.CutInGenSpeed, d0.torqueControllerParams.CutInGenSpeed);
            Assert.AreEqual(d1.torqueControllerParams.CutInGenTorque, d0.torqueControllerParams.CutInGenTorque);
            Assert.AreEqual(d1.torqueControllerParams.KGen, d0.torqueControllerParams.KGen);
            Assert.AreEqual(d1.torqueControllerParams.RateLimitGenTorque, d0.torqueControllerParams.RateLimitGenTorque);
            Assert.AreEqual(d1.torqueControllerParams.RatedGenSpeed, d0.torqueControllerParams.RatedGenSpeed);
            Assert.AreEqual(d1.torqueControllerParams.RatedGenTorque, d0.torqueControllerParams.RatedGenTorque);
            Assert.AreEqual(d1.torqueControllerParams.Region2EndGenSpeed, d0.torqueControllerParams.Region2EndGenSpeed);
            Assert.AreEqual(d1.torqueControllerParams.Region2StartGenSpeed, d0.torqueControllerParams.Region2StartGenSpeed);
            Assert.AreEqual(d1.torqueControllerParams.TorqueControllerRelax, d0.torqueControllerParams.TorqueControllerRelax);
            
            Assert.IsNotNull(d1.airfoilBlade);
            Assert.AreEqual(d1.airfoilBlade.Count, d0.airfoilBlade.Count);
            for (int i = 0; i < d1.airfoilBlade.Count; i++)
            {
                var x0 = d0.airfoilBlade[i];
                var x1 = d1.airfoilBlade[i];
                Assert.AreEqual(x0.AirfoilName, x1.AirfoilName);
                Assert.IsNotNull(x1.Blade);
                Assert.AreEqual(x1.Blade.Count, x0.Blade.Count);
                for (int j = 0; j < x1.Blade.Count; j++)
                {
                    var y0 = x0.Blade[j];
                    var y1 = x1.Blade[j];
                    Assert.AreEqual(y0.X, y1.X);
                    Assert.AreEqual(y0.Y, y1.Y);
                    Assert.AreEqual(y0.Z, y1.Z);
                }
            }
        }

        [Test]
        public void AirfoilProperties()
        {
            var d0 = new AirfoilPropertiesData();
            var handler = new AirfoilPropertiesHandler("strange_airfoil");

            d0.airfoilData = new List<Vertice>();
            var count = (int)((_rnd.NextDouble()+0.1)*32);
            for (int i = 0; i < count; i++)
            {
                var x = new Vertice((decimal)_rnd.NextDouble(), (decimal)_rnd.NextDouble(), (decimal)_rnd.NextDouble());
                d0.airfoilData.Add(x);
            }
            handler.Write(_path, d0);

            var d1 = (AirfoilPropertiesData) handler.Read(_path);

            Assert.IsNotNull(d1.airfoilData);
            Assert.AreEqual(d1.airfoilData.Count, d0.airfoilData.Count);
            for (int i = 0; i < d1.airfoilData.Count; i++)
            {
                var x = d0.airfoilData[i];
                var y = d1.airfoilData[i];

                Assert.AreEqual(x.X, y.X);
                Assert.AreEqual(x.Y, y.Y);
                Assert.AreEqual(x.Z, y.Z);
            }
        }
        [Test]
        public void TurbineArrayPropertiesFAST()
        {
            var d0 = new TurbineArrayPropFASTData();
            var handler = new TurbineArrayPropFASTHandler();

            d0.general.effectiveRadiusFactor = (decimal) _rnd.NextDouble();
            d0.general.epsilon = (decimal)_rnd.NextDouble();
            d0.general.numberofBld = (int)((_rnd.NextDouble() + 0.1)*100);
            d0.general.numberofBldPts = (int)((_rnd.NextDouble() + 0.1) * 100);
            d0.general.pointInterpType = (int)((_rnd.NextDouble() + 0.1) * 100);
            d0.general.rotorDiameter = (decimal)_rnd.NextDouble();
            d0.general.smearRadius = (decimal)_rnd.NextDouble();
            d0.general.yawAngle = (decimal)_rnd.NextDouble();

            for (int i = 0; i < 8; i++)
            {
                var ti = new TurbineInstanceFAST();
                ti.hubz = (decimal)_rnd.NextDouble();
                ti.refx = (decimal)_rnd.NextDouble();
                ti.refy = (decimal)_rnd.NextDouble();
                ti.refz = (decimal)_rnd.NextDouble();
                d0.turbine.Add( ti );
            }
            handler.Write( _path, d0 );

            var d1 = (TurbineArrayPropFASTData)handler.Read(_path);

            Assert.AreEqual(d0.general.effectiveRadiusFactor, d1.general.effectiveRadiusFactor);
            Assert.AreEqual(d0.general.epsilon, d1.general.epsilon);
            Assert.AreEqual(d0.general.numberofBld, d1.general.numberofBld);
            Assert.AreEqual(d0.general.numberofBldPts, d1.general.numberofBldPts);
            Assert.AreEqual(d0.general.pointInterpType, d1.general.pointInterpType);
            Assert.AreEqual(d0.general.rotorDiameter, d1.general.rotorDiameter);
            Assert.AreEqual(d0.general.smearRadius, d1.general.smearRadius);
            Assert.AreEqual(d0.general.yawAngle, d1.general.yawAngle);

            Assert.IsNotNull( d1.turbine );
            Assert.AreEqual(d0.turbine.Count, d1.turbine.Count);
            for (int i = 0; i < d0.turbine.Count; i++)
            {
                Assert.AreEqual(d0.turbine[i].hubz, d1.turbine[i].hubz);
                Assert.AreEqual(d0.turbine[i].refx, d1.turbine[i].refx);
                Assert.AreEqual(d0.turbine[i].refy, d1.turbine[i].refy);
                Assert.AreEqual(d0.turbine[i].refz, d1.turbine[i].refz);
            }
        }
        [Test]
        public void FvSchemeHandler()
        {
            var d0 = new FvSchemesData(true);
            var handler = new FvSchemesHandler();

            var count = (int) ((_rnd.NextDouble() + 0.1)*8);
            for (int i = 0; i < count; i++)
            {
                var x = new TimeScheme();
                x.type = RandomEnum<TimeSchemeType>();
                x.psi = (decimal) _rnd.NextDouble();
                d0.ddtSchemes.Add(x);
            }
            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new GradientScheme();
                //x.keyword = RandomString();
                x.use_default = RandomBool();
                if (!x.use_default)
                {
                    x.function = "(" + RandomString() + ")";
                    x.scheme = "grad";
                }
                x.limited = RandomEnum<LimitedType>();
                do
                {
                    x.discretisation = RandomEnum<DiscretisationType>();
                } while (x.discretisation == DiscretisationType.none);

                if (x.discretisation == DiscretisationType.Gauss )
                {
                    do
                    {
                        x.interpolation = RandomEnum<InterpolationType>();                        
                    } while (x.interpolation == InterpolationType.none );
                }
                if (x.limited != LimitedType.none)
                {
                    x.psi = (decimal) _rnd.NextDouble();
                }
                d0.gradSchemes.Add(x);
            }
            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new InterpolationScheme();
                //x.keyword = RandomString();
                x.use_default = RandomBool();
                if (!x.use_default)
                {
                    x.function = "(" + RandomString() + ")";
                    x.scheme = "iterpolate";
                }
                do
                {
                    x.interpolation = RandomEnum<InterpolationType>();    
                } while (x.interpolation == InterpolationType.none);
                
                x.view = RandomEnum<BoundView>();

                if (x.view == BoundView.Name)
                {
                    if (InterpolationScheme.StrictlyBoundedField(x.interpolation))
                    {
                        x.lower_limit = 0;
                        x.upper_limit = 1;
                    }
                    else x.view = BoundView.None;
                }
                else if (x.view == BoundView.Range)
                {
                    if (InterpolationScheme.StrictlyBoundedField(x.interpolation))
                    {
                        x.lower_limit = (decimal)(_rnd.NextDouble() + 0.1) * 10;
                        x.upper_limit = (decimal)(_rnd.NextDouble() + 0.1) * 10;
                    }
                    else x.view = BoundView.None;
                }
                if (x.view == BoundView.None)
                {
                    x.psi = (decimal)_rnd.NextDouble();
                    x.flux = RandomString();
                }
                d0.interpolationSchemes.Add(x);
            }

            /*
            body4.Append("\tdefault limitedLinear 1.0 psi;\n");
            body4.Append("\tdefault vanLeer01;\n");
            body4.Append("\tdefault upwind psi;\n");
            body4.Append("\tdefault limitedvanLeer -2.0 3.0;\n");
             */


            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new LaplacianScheme();
                x.interpolation = RandomEnum<InterpolationType>();
                //x.keyword = RandomString();
                x.use_default = RandomBool();
                if (!x.use_default)
                {
                    x.function = "(" + RandomString() + ")";
                    x.scheme = "lapalacian";
                }
                x.discretisation = RandomEnum<DiscretisationType>();
                x.snGradScheme = RandomEnum<SurfaceNormalGradientType>();
                if (x.snGradScheme==SurfaceNormalGradientType.limited)
                {
                    x.psi = (decimal)_rnd.NextDouble();                    
                }
                d0.laplacianSchemes.Add(x);
            }
            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new DivergenceScheme();
                x.interpolation = RandomEnum<InterpolationType>();
                //x.keyword = RandomString();
                x.use_default = RandomBool();
                if (!x.use_default)
                {
                    x.function = "(" + RandomString() + ")";
                    x.scheme = "div";
                }
                //x.psi = (decimal)_rnd.NextDouble();
                d0.divSchemes.Add(x);
            }
            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new SurfaceNormalGradientScheme();
                x.scheme = RandomString();
                x.type = RandomEnum<SurfaceNormalGradientType>();
                if (x.type==SurfaceNormalGradientType.limited)
                {
                    x.psi = (decimal)_rnd.NextDouble();
                }
                d0.snGradSchemes.Add(x);
            }
            count = (int)((_rnd.NextDouble() + 0.1) * 8);
            for (int i = 0; i < count; i++)
            {
                var x = new FluxCalculation();
                x.flux = RandomString();
                x.enable = Convert.ToBoolean(RandomEnum<FlagYesNo>());
                d0.fluxCalculation.Add(x);
            }



            handler.Write( _path, d0 );
            var d1 = (FvSchemesData)handler.Read(_path);

            Assert.IsNotNull(d1.ddtSchemes);
            Assert.AreEqual(d1.ddtSchemes.Count,d0.ddtSchemes.Count);
            for (int i = 0; i < d1.ddtSchemes.Count; i++)
            {
                Assert.AreEqual(d1.ddtSchemes[i].type,d0.ddtSchemes[i].type);
                Assert.AreEqual(d1.ddtSchemes[i].psi, d0.ddtSchemes[i].psi);
            }
            Assert.IsNotNull(d1.gradSchemes);
            Assert.AreEqual(d1.gradSchemes.Count,d0.gradSchemes.Count);
            for (int i = 0; i < d1.gradSchemes.Count; i++)
            {
                Assert.AreEqual(d1.gradSchemes[i].discretisation, d0.gradSchemes[i].discretisation);
                Assert.AreEqual(d1.gradSchemes[i].psi, d0.gradSchemes[i].psi);
                Assert.AreEqual(d1.gradSchemes[i].interpolation, d0.gradSchemes[i].interpolation);
                Assert.AreEqual(d1.gradSchemes[i].limited, d0.gradSchemes[i].limited);
                Assert.AreEqual(d1.gradSchemes[i].use_default, d0.gradSchemes[i].use_default);
                Assert.AreEqual(d1.gradSchemes[i].function, d0.gradSchemes[i].function);
            }
            Assert.IsNotNull(d1.divSchemes);
            Assert.AreEqual(d1.divSchemes.Count,d0.divSchemes.Count);
            for (int i = 0; i < d1.divSchemes.Count; i++)
            {
                Assert.AreEqual(d1.divSchemes[i].function, d0.divSchemes[i].function);
                Assert.AreEqual(d1.divSchemes[i].use_default, d0.divSchemes[i].use_default);
                Assert.AreEqual(d1.divSchemes[i].interpolation, d0.divSchemes[i].interpolation);
                Assert.AreEqual(d1.divSchemes[i].psi, d0.divSchemes[i].psi);
            }

            Assert.IsNotNull(d1.fluxCalculation);
            Assert.AreEqual(d1.fluxCalculation.Count, d0.fluxCalculation.Count);
            for (int i = 0; i < d1.fluxCalculation.Count; i++)
            {
                Assert.AreEqual(d1.fluxCalculation[i].flux, d0.fluxCalculation[i].flux);
                Assert.AreEqual(d1.fluxCalculation[i].enable, d0.fluxCalculation[i].enable);
            }

            Assert.IsNotNull(d1.laplacianSchemes);
            Assert.AreEqual(d1.laplacianSchemes.Count,d0.laplacianSchemes.Count);
            for (int i = 0; i < d1.laplacianSchemes.Count; i++)
            {
                Assert.AreEqual(d1.laplacianSchemes[i].function,d0.laplacianSchemes[i].function);
                Assert.AreEqual(d1.laplacianSchemes[i].use_default, d0.laplacianSchemes[i].use_default);
                Assert.AreEqual(d1.laplacianSchemes[i].discretisation, d0.laplacianSchemes[i].discretisation);
                Assert.AreEqual(d1.laplacianSchemes[i].interpolation, d0.laplacianSchemes[i].interpolation);
                Assert.AreEqual(d1.laplacianSchemes[i].psi, d0.laplacianSchemes[i].psi);
                Assert.AreEqual(d1.laplacianSchemes[i].snGradScheme, d0.laplacianSchemes[i].snGradScheme);
            }

            Assert.IsNotNull(d1.snGradSchemes);
            Assert.AreEqual(d1.snGradSchemes.Count,d0.snGradSchemes.Count);
            for (int i = 0; i < d1.snGradSchemes.Count; i++)
            {
                Assert.AreEqual(d1.snGradSchemes[i].scheme,d0.snGradSchemes[i].scheme);
                Assert.AreEqual(d1.snGradSchemes[i].psi, d0.snGradSchemes[i].psi);
                Assert.AreEqual(d1.snGradSchemes[i].scheme, d0.snGradSchemes[i].scheme);
            }
        }
        [Test]
        public void RefineMeshFileHandler()
        {
            var d0 = new RefineMeshDictData();
            var handler = new RefineMeshDictHandler();

            d0.setvalue = RandomString();
            d0.coordsys = RandomEnum<CoordinateSystem>();
            d0.globalCoeffs = new List<Coeffs>();
            var count = _rnd.Next(5) + 1;
            for (int i = 0; i < count; i++)
            {
                var x = new Coeffs();
                x.dir = RandomEnum<DirectionType>();
                x.value = new Vertice();
                x.value.X = (decimal) _rnd.NextDouble();
                x.value.Y = (decimal)_rnd.NextDouble();
                x.value.Z = (decimal)_rnd.NextDouble();
                d0.globalCoeffs.Add(x);
            }
            d0.patchLocalCoeffs = new List<Coeffs>();
            d0.patch = RandomString();
            count = _rnd.Next(5) + 1;
            for (int i = 0; i < count; i++)
            {
                var x = new Coeffs();
                x.dir = RandomEnum<DirectionType>();
                x.value = new Vertice();
                x.value.X = (decimal)_rnd.NextDouble();
                x.value.Y = (decimal)_rnd.NextDouble();
                x.value.Z = (decimal)_rnd.NextDouble();
                d0.patchLocalCoeffs.Add(x);
            }
            d0.direction = new List<DirectionType>();
            count = _rnd.Next(4) + 1;
            for (int i = 0; i < count; i++)
            {
                d0.direction.Add(RandomEnum<DirectionType>());
            }

            d0.geometricCut = RandomBool();
            d0.useHexTopology = RandomBool();
            d0.writeMesh = RandomBool();

            handler.Write(_path, d0);

            var d1 = (RefineMeshDictData) handler.Read(_path);

            Assert.IsNotNull(d1);
            Assert.AreEqual(d1.setvalue, d0.setvalue);
            Assert.AreEqual(d1.coordsys, d0.coordsys);
            Assert.AreEqual(d1.geometricCut, d0.geometricCut);
            Assert.AreEqual(d1.patch, d0.patch);
            Assert.AreEqual(d1.useHexTopology, d0.useHexTopology);
            Assert.AreEqual(d1.writeMesh, d0.writeMesh);
            Assert.AreEqual(d1.globalCoeffs.Count, d0.globalCoeffs.Count);
            Assert.AreEqual(d1.patchLocalCoeffs.Count, d0.patchLocalCoeffs.Count);
            Assert.AreEqual(d1.direction.Count, d0.direction.Count);

            for (int i = 0; i < d1.patchLocalCoeffs.Count; i++)
            {
                Assert.AreEqual(d1.patchLocalCoeffs[i].dir, d0.patchLocalCoeffs[i].dir);
                Assert.AreEqual(d1.patchLocalCoeffs[i].value.X, d0.patchLocalCoeffs[i].value.X);
                Assert.AreEqual(d1.patchLocalCoeffs[i].value.Y, d0.patchLocalCoeffs[i].value.Y);
                Assert.AreEqual(d1.patchLocalCoeffs[i].value.Z, d0.patchLocalCoeffs[i].value.Z);
            }

            for (int i = 0; i < d1.globalCoeffs.Count; i++)
            {
                Assert.AreEqual(d1.globalCoeffs[i].dir, d0.globalCoeffs[i].dir);
                Assert.AreEqual(d1.globalCoeffs[i].value.X, d0.globalCoeffs[i].value.X);
                Assert.AreEqual(d1.globalCoeffs[i].value.Y, d0.globalCoeffs[i].value.Y);
                Assert.AreEqual(d1.globalCoeffs[i].value.Z, d0.globalCoeffs[i].value.Z);
            }

            for (int i = 0; i < d1.direction.Count; i++)
            {
                Assert.AreEqual(d1.direction[i], d0.direction[i]);
            }

        }

        [Test]
        public void ShmDictHandler()
        {
            var d0 = new ShmDictData();

            d0.CastellatedMeshControls.locationInMesh = new RealPoint { X = -9.23149e-05, Y = -0.0025, Z = -0.0025 };
            d0.CastellatedMeshControls.allowFreeStandingZoneFaces = true;
            d0.CastellatedMeshControls.Features.Add(new ShmFeature { File = "flange.eMesh", Level = 0 });
            d0.CastellatedMeshControls.Features.Add(new ShmFeature { File = "flange2.eMesh", Level = 1 });
            d0.CastellatedMeshControls.Surfaces.Add(new ShmRefinementSurface { Name = "flange", Level = new ShmRefinementLevel(2, 2) });
            d0.CastellatedMeshControls.Surfaces.Add(new ShmRefinementSurface { Name = "flange1", Level = new ShmRefinementLevel(3, 5) });

            var r1 = new ShmRefinementRegion {Name = "refineHole", Mode = ShmRefinementRegionMode.inside};
            r1.Levels.Add(new ShmRefinementLevel(1E15, 3));
            d0.CastellatedMeshControls.Regions.Add(r1);

            var g1 = new ShmGeometry();
            g1.Type = ShmGeometryType.triSurfaceMesh;
            g1.GlobalName = "flange.stl";
            g1.Name = "flange";
            d0.Geometries.Add(g1);

            var g2 = new ShmGeometry();
            g2.Type = ShmGeometryType.searchableBox;
            g2.GlobalName = "box1x1x1";
            g2.BoxMin = new RealPoint { X = 1.5, Y = 1, Z = -0.5 };
            g2.BoxMax = new RealPoint { X = 3.5, Y = 2, Z = 0.5 };
            d0.Geometries.Add(g2);

            var g3 = new ShmGeometry();
            g3.Type = ShmGeometryType.searchableSphere;
            g3.GlobalName = "refineHole";
            g3.SphereCenter = new RealPoint { X = 0, Y = 0, Z = -0.012 };
            g3.SphereRadius = 0.003;
            d0.Geometries.Add(g3);

            var handler = new ShmDictHandler();
            handler.Write(@"D:\projects\temp\snappy.txt", d0);
        }

        [Test]
        [Ignore]
        public void GenerateShm()
        {
            var type = typeof(ShmSnapControls);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (PropertyInfo info in type.GetProperties())
            {
                if (info.PropertyType == typeof(bool))
                    Console.WriteLine("t.Replace(\"({{[[{0}]]}})\", d.{0}.ToString().ToLowerInvariant());", LowerFirstChar(info.Name));
                else
                    Console.WriteLine("t.Replace(\"({{[[{0}]]}})\", d.{0}.ToString());", LowerFirstChar(info.Name));
            }
        }

        private string LowerFirstChar(string input)
        {
            return input.Substring(0, 1).ToLowerInvariant() + input.Substring(1, input.Length - 1);
        }
    }
}
