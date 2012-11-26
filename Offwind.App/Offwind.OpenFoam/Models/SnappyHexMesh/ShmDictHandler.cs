using System;
using System.Collections.Generic;
using System.Text;

namespace Offwind.Products.OpenFoam.Models.SnappyHexMesh
{
    public sealed class ShmDictHandler : FoamFileHandler
    {
        public ShmDictHandler()
            : base("snappyHexMeshDict", null, "system", ShmDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new ShmDictData();
            //string txt;
            //using (var reader = new StreamReader(path))
            //{
            //    txt = reader.ReadToEnd();
            //}

            //var grammar = new OpenFoamGrammar();
            //var parser = new Parser(grammar);
            //var tree = parser.Parse(txt);

            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (ShmDictData)data;
            var t = new StringBuilder(ShmDictRes.Template);

            t.Replace("({[[castellatedMesh]]})", d.castellatedMesh.ToString().ToLowerInvariant());
            t.Replace("({[[snap]]})", d.snap.ToString().ToLowerInvariant());
            t.Replace("({[[addLayers]]})", d.addLayers.ToString().ToLowerInvariant());
            t.Replace("({[[debug]]})", d.debug.ToString());
            t.Replace("({[[mergeTolerance]]})", d.mergeTolerance.ToString());


            var geometry = GetGeometry(d.Geometries);
            t.Replace("({[[geometry]]})", geometry);

            var castellatedMeshControls = GetCastellatedMeshControls(d.CastellatedMeshControls);
            t.Replace("({[[castellatedMeshControls]]})", castellatedMeshControls);

            var snapControls = GetSnapControls(d.SnapControls);
            t.Replace("({[[snapControls]]})", snapControls);

            var addLayersControls = GetAddLayersControls(d.AddLayersControls);
            t.Replace("({[[addLayersControls]]})", addLayersControls);

            var meshQualityControls = GetMeshQualityControls(d.MeshQualityControls);
            t.Replace("({[[meshQualityControls]]})", meshQualityControls);

            WriteToFile(path, t.ToString());
        }

        private string GetMeshQualityControls(ShmMeshQualityControls d)
        {
            var t = new StringBuilder(ShmDictRes.TemplateMeshQualityControls);
            t.Replace("({[[maxNonOrtho]]})", d.maxNonOrtho.ToString());
            t.Replace("({[[maxBoundarySkewness]]})", d.maxBoundarySkewness.ToString());
            t.Replace("({[[maxInternalSkewness]]})", d.maxInternalSkewness.ToString());
            t.Replace("({[[maxConcave]]})", d.maxConcave.ToString());
            t.Replace("({[[minVol]]})", d.minVol.ToString());
            t.Replace("({[[minTetQuality]]})", d.minTetQuality.ToString());
            t.Replace("({[[minArea]]})", d.minArea.ToString());
            t.Replace("({[[minTwist]]})", d.minTwist.ToString());
            t.Replace("({[[minDeterminant]]})", d.minDeterminant.ToString());
            t.Replace("({[[minFaceWeight]]})", d.minFaceWeight.ToString());
            t.Replace("({[[minVolRatio]]})", d.minVolRatio.ToString());
            t.Replace("({[[minTriangleTwist]]})", d.minTriangleTwist.ToString());
            t.Replace("({[[nSmoothScale]]})", d.nSmoothScale.ToString());
            t.Replace("({[[errorReduction]]})", d.errorReduction.ToString());
            t.Replace("({[[relaxed]]})", d.relaxed.ToString().ToLowerInvariant());
            t.Replace("({[[relaxedMaxNonOrtho]]})", d.relaxedMaxNonOrtho.ToString());
            return t.ToString();
        }

        private string GetAddLayersControls(ShmAddLayersControls d)
        {
            var t = new StringBuilder(ShmDictRes.TemplateAddLayersControls);
            t.Replace("({[[relativeSizes]]})", d.relativeSizes.ToString().ToLowerInvariant());
            t.Replace("({[[expansionRatio]]})", d.expansionRatio.ToString());
            t.Replace("({[[finalLayerThickness]]})", d.finalLayerThickness.ToString());
            t.Replace("({[[minThickness]]})", d.minThickness.ToString());
            t.Replace("({[[nGrow]]})", d.nGrow.ToString());
            t.Replace("({[[featureAngle]]})", d.featureAngle.ToString());
            t.Replace("({[[nRelaxIter]]})", d.nRelaxIter.ToString());
            t.Replace("({[[nSmoothSurfaceNormals]]})", d.nSmoothSurfaceNormals.ToString());
            t.Replace("({[[nSmoothNormals]]})", d.nSmoothNormals.ToString());
            t.Replace("({[[nSmoothThickness]]})", d.nSmoothThickness.ToString());
            t.Replace("({[[maxFaceThicknessRatio]]})", d.maxFaceThicknessRatio.ToString());
            t.Replace("({[[maxThicknessToMedialRatio]]})", d.maxThicknessToMedialRatio.ToString());
            t.Replace("({[[minMedianAxisAngle]]})", d.minMedianAxisAngle.ToString());
            t.Replace("({[[nBufferCellsNoExtrude]]})", d.nBufferCellsNoExtrude.ToString());
            t.Replace("({[[nLayerIter]]})", d.nLayerIter.ToString());
            t.Replace("({[[nRelaxedIter]]})", d.nRelaxedIter.ToString());
            return t.ToString();
        }

        private string GetSnapControls(ShmSnapControls d)
        {
            var t = new StringBuilder(ShmDictRes.TemplateSnapControls);
            t.Replace("({[[nSmoothPatch]]})", d.NSmoothPatch.ToString());
            t.Replace("({[[tolerance]]})", d.Tolerance.ToString());
            t.Replace("({[[nSolveIter]]})", d.NSolveIter.ToString());
            t.Replace("({[[nRelaxIter]]})", d.NRelaxIter.ToString());
            t.Replace("({[[nFeatureSnapIter]]})", d.NFeatureSnapIter.ToString());
            return t.ToString();
        }

        private string GetCastellatedMeshControls(ShmCastellatedMeshControls d)
        {
            var t = new StringBuilder(ShmDictRes.TemplateCastellatedMeshControls);
            t.Replace("({[[maxLocalCells]]})", d.maxLocalCells.ToString());
            t.Replace("({[[maxGlobalCells]]})", d.maxGlobalCells.ToString());

            t.Replace("({[[locationInMesh_x]]})", d.locationInMesh.X.ToString());
            t.Replace("({[[locationInMesh_y]]})", d.locationInMesh.Y.ToString());
            t.Replace("({[[locationInMesh_z]]})", d.locationInMesh.Z.ToString());
            
            t.Replace("({[[minRefinementCells]]})", d.minRefinementCells.ToString());
            t.Replace("({[[nCellsBetweenLevels]]})", d.nCellsBetweenLevels.ToString());
            t.Replace("({[[resolveFeatureAngle]]})", d.resolveFeatureAngle.ToString());
            t.Replace("({[[allowFreeStandingZoneFaces]]})", d.allowFreeStandingZoneFaces.ToString().ToLowerInvariant());

            var features = GetFeatures(d.Features);
            t.Replace("({[[features]]})", features);

            var surfaces = GetSurfaces(d.Surfaces);
            t.Replace("({[[surfaces]]})", surfaces);

            var regions = GetRegions(d.Regions);
            t.Replace("({[[regions]]})", regions);

            return t.ToString();
        }

        private string GetRegions(List<ShmRefinementRegion> d)
        {
            var t = new StringBuilder();
            foreach (var r in d)
            {
                t.AppendFormat("{0}{0}{1}", _indent, r.Name.Trim());
                t.AppendLine();
                t.AppendFormat("{0}{0}{{", _indent);
                t.AppendLine();
                switch (r.Mode)
                {
                    case ShmRefinementRegionMode.inside:
                        t.AppendFormat("{0}{0}{0}mode inside;", _indent);
                        break;
                    case ShmRefinementRegionMode.outside:
                        t.AppendFormat("{0}{0}{0}mode outside;", _indent);
                        break;
                    case ShmRefinementRegionMode.distance:
                        t.AppendFormat("{0}{0}{0}mode distance;", _indent);
                        break;
                }
                t.AppendLine();
                t.AppendFormat("{0}{0}{0}levels (", _indent);
                foreach (var l in r.Levels)
                {
                    t.AppendFormat(" ({0} {1})", l.Min, l.Max);
                }
                t.Append(");");
                t.AppendLine();
                t.AppendFormat("{0}{0}}}", _indent);
                t.AppendLine();
            }
            return t.ToString();
        }

        private string GetSurfaces(List<ShmRefinementSurface> d)
        {
            var t = new StringBuilder();
            foreach (var f in d)
            {
                t.AppendFormat("{0}{0}{1}", _indent, f.Name.Trim());
                t.AppendLine();
                t.AppendFormat("{0}{0}{{", _indent);
                t.AppendLine();
                t.AppendFormat("{0}{0}{0}level ({1} {2});", _indent, f.Level.Min, f.Level.Max);
                t.AppendLine();
                t.AppendFormat("{0}{0}}}", _indent);
                t.AppendLine();
            }
            return t.ToString();
        }

        private string GetFeatures(List<ShmFeature> d)
        {
            var t = new StringBuilder();
            foreach (var f in d)
            {
                t.AppendFormat("{0}{0}{{", _indent);
                t.AppendLine();
                t.AppendFormat("{0}{0}{0}file \"{1}\";", _indent, f.File.Trim());
                t.AppendLine();
                t.AppendFormat("{0}{0}{0}level {1};", _indent, f.Level);
                t.AppendLine();
                t.AppendFormat("{0}{0}}}", _indent);
                t.AppendLine(); 
            }
            return t.ToString();
        }

        private string GetGeometry(IEnumerable<ShmGeometry> d)
        {
            var t = new StringBuilder();
            Action<string> writeType = x =>
            {
                t.AppendFormat("{0}{0}type {1};", _indent, x);
                t.AppendLine();
            };
            Action<ShmGeometry> writeName = x =>
            {
                if (x.Name != null && x.Name.Trim().Length > 0)
                {
                    t.AppendFormat("{0}{0}name {1};", _indent, x.Name.Trim());
                    t.AppendLine();
                }
            };

            t.AppendFormat("geometry");
            t.AppendLine();
            t.AppendFormat("{{");
            t.AppendLine();
            foreach (var g in d)
            {
                t.AppendFormat("{0}{1}", _indent, g.GlobalName);
                t.AppendLine();
                t.AppendFormat("{0}{{", _indent);
                t.AppendLine();
                switch (g.Type)
                {
                    case ShmGeometryType.triSurfaceMesh:
                        writeType("triSurfaceMesh");
                        writeName(g);
                        break;
                    case ShmGeometryType.searchableBox:
                        writeType("searchableBox");
                        writeName(g);
                        t.AppendFormat("{0}{0}min ({1} {2} {3});", _indent, g.BoxMin.X, g.BoxMin.Y, g.BoxMin.Z);
                        t.AppendLine();
                        t.AppendFormat("{0}{0}max ({1} {2} {3});", _indent, g.BoxMax.X, g.BoxMax.Y, g.BoxMax.Z);
                        t.AppendLine();
                        break;
                    case ShmGeometryType.searchableSphere:
                        writeType("searchableSphere");
                        writeName(g);
                        t.AppendFormat("{0}{0}centre ({1} {2} {3});", _indent, g.SphereCenter.X, g.SphereCenter.Y, g.SphereCenter.Z);
                        t.AppendLine();
                        t.AppendFormat("{0}{0}radius {1};", _indent, g.SphereRadius);
                        t.AppendLine();
                        break;
                }
                t.AppendFormat("{0}}}", _indent);
                t.AppendLine();
            }
            t.AppendFormat("}};");
            return t.ToString();
        }

        private const string _indent = "    ";

    }
}
