using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Time.FieldData
{
    public class FieldDataHandler : FoamFileHandler
    {
        const string FieldFormatRegex = @"format\s+(ascii|binary);";
        const string FieldLocationRegex = @"location\s+""(.+)"";";
        const string FieldObjectRegex = @"object\s+(.+);";
        const string FieldClassRegex = @"class\s+(volScalarField|volVectorField);";
        const string DimRegex = @"dimensions\s+\[\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*([-|\d|\.]+)\s*\];";

        public FieldDataHandler()
            : base("T", null, "0.original", FieldDataRes.Default)
        {
        }

        public FieldDataHandler(string fileName, string fileSuffix, string relativePath)
            : base(fileName, fileSuffix, relativePath, FieldDataRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new FieldData();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            var formatMatch = Regex.Match(txt, FieldFormatRegex);
            rawData.FieldFormat = (Format)Enum.Parse(typeof(Format), formatMatch.Groups[1].Value);

            var classMatch = Regex.Match(txt, FieldClassRegex);
            rawData.FieldClass = (FieldClass)Enum.Parse(typeof(FieldClass), classMatch.Groups[1].Value);

            var locationMatch = Regex.Match(txt, FieldLocationRegex);
            rawData.FieldLocation = locationMatch.Groups[1].Value;

            var objectMatch = Regex.Match(txt, FieldObjectRegex);
            rawData.FieldObject = objectMatch.Groups[1].Value;

            var dimMatch = Regex.Match(txt, DimRegex);
            rawData.Dimensions.Mass = decimal.Parse(dimMatch.Groups[1].Value);
            rawData.Dimensions.Length = decimal.Parse(dimMatch.Groups[2].Value);
            rawData.Dimensions.Time = decimal.Parse(dimMatch.Groups[3].Value);
            rawData.Dimensions.Temperature = decimal.Parse(dimMatch.Groups[4].Value);
            rawData.Dimensions.Quantity = decimal.Parse(dimMatch.Groups[5].Value);
            rawData.Dimensions.Current = decimal.Parse(dimMatch.Groups[6].Value);
            rawData.Dimensions.LuminousIntensity = decimal.Parse(dimMatch.Groups[7].Value);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "boundaryField":
                        ParsePatches(rootEntryNode.ChildNodes[2], rawData);
                        break;
                    case "internalField":
                        rawData.InternalFieldType = GetFieldType(rootEntryNode);
                        rawData.InternalFieldValue = GetFieldValue(rootEntryNode.ChildNodes[2].ChildNodes[0]);
                        break;
                }
            }
            return rawData;
        }

        private void ParsePatches(ParseTreeNode node, FieldData rawData)
        {
            foreach (ParseTreeNode rootEntryNode in node.FindDictEntries(null))
            {
                var s = new BoundaryPatch();
                s.Name = rootEntryNode.GetEntryIdentifier();
                rawData.Patches.Add(s);
                ParsePatch(rootEntryNode.ChildNodes[2], s);
            }
        }

        private void ParsePatch(ParseTreeNode node, BoundaryPatch s)
        {
            foreach (ParseTreeNode rootEntryNode in node.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "type":
                        s.PatchType = rootEntryNode.GetBasicValEnum<PatchType>();
                        break;
                    case "rho":
                        s.Rho = rootEntryNode.GetBasicValString();
                        break;
                    case "gradient":
                        s.GradientFieldType = GetFieldType(rootEntryNode);
                        s.GradientValue = GetFieldValue(rootEntryNode.ChildNodes[2].ChildNodes[0]);
                        break;
                    case "value":
                        s.ValueFieldType = GetFieldType(rootEntryNode);
                        s.ValueValue = GetFieldValue(rootEntryNode.ChildNodes[2].ChildNodes[0]);
                        break;
                }
            }
        }

        private FieldType GetFieldType(ParseTreeNode rootEntryNode)
        {
            var text = rootEntryNode.ChildNodes[1].ChildNodes[0].Token.Text;
            return text.ToEnum<FieldType>();
        }

        private decimal[] GetFieldValue(ParseTreeNode valNode)
        {
            if (valNode.ToString() == "ofArray")
            {
                return valNode.ChildNodes[0].ChildNodes[0].ChildNodes[1].GetArrayOfDecimal();
            }
            return new [] { decimal.Parse(valNode.Token.Text) };
        }

        public override void Write(string path, object data)
        {
            var d = (FieldData)data;

            if (d.FieldLocation == null || d.FieldLocation.Trim().Length == 0)
                throw new ArgumentException("FieldLocation property must be set");

            if (d.FieldObject == null || d.FieldObject.Trim().Length == 0)
                throw new ArgumentException("FieldObject property must be set");

            var t = new StringBuilder(FieldDataRes.Template);

            t.Replace("({[[FieldFormat]]})", d.FieldFormat.ToString());
            t.Replace("({[[FieldClass]]})", d.FieldClass.ToString());
            t.Replace("({[[FieldLocation]]})", d.FieldLocation);
            t.Replace("({[[FieldObject]]})", d.FieldObject);

            t.Replace("({[[InternalFieldType]]})", d.InternalFieldType.ToString());
            t.Replace("({[[InternalFieldValue]]})", d.InternalFieldValue.WriteArrayOrNumber());

            t.Replace("({[[Mass]]})", d.Dimensions.Mass.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[Length]]})", d.Dimensions.Length.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[Time]]})", d.Dimensions.Time.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[Temperature]]})", d.Dimensions.Temperature.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[Quantity]]})", d.Dimensions.Quantity.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[Current]]})", d.Dimensions.Current.ToString(CultureInfo.InvariantCulture));
            t.Replace("({[[LuminousIntensity]]})", d.Dimensions.LuminousIntensity.ToString(CultureInfo.InvariantCulture));

            var tPatches = new StringBuilder();
            foreach (var patch in d.Patches)
            {
                Debug.Assert(Validator.IsIdentifier(patch.Name), "Incorrect name");
                const string indent = "    ";
                var pt = new StringBuilder();
                pt.AppendFormat("{0}{1}{2}", indent, patch.Name, Environment.NewLine);
                pt.AppendFormat("{0}{{{1}", indent, Environment.NewLine);

                pt.AppendFormat("{0}{0}type        {1};{2}", indent, patch.PatchType, Environment.NewLine);

                if (patch.Rho != null && patch.Rho.Trim().Length > 0)
                {
                    pt.AppendFormat("{0}{0}rho         {1};{2}", indent, patch.Rho, Environment.NewLine);
                }

                if (patch.GradientFieldType != FieldType.undefined)
                {
                    pt.AppendFormat("{0}{0}gradient    {1} {2};{3}", indent, patch.GradientFieldType, patch.GradientValue.WriteArrayOrNumber(), Environment.NewLine);
                }

                if (patch.ValueFieldType != FieldType.undefined)
                {
                    pt.AppendFormat("{0}{0}value       {1} {2};{3}", indent, patch.ValueFieldType, patch.ValueValue.WriteArrayOrNumber(), Environment.NewLine);
                }
                pt.AppendFormat("{0}}}{1}", indent, Environment.NewLine);
                tPatches.Append(pt);
            }

            t.Replace("({[[BoundaryPatches]]})", tPatches.ToString());
            WriteToFile(path, t.ToString());
        }
  }
}
