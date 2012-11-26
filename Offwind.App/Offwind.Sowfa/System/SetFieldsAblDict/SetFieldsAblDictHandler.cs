using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.SetFieldsAblDict
{
    public sealed class SetFieldsAblDictHandler : FoamFileHandler
    {
        public SetFieldsAblDictHandler()
            : base("setFieldsABLDict", null, "system", SetFieldsAblDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new SetFieldsAblDictData();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "xMax":
                        rawData.xMax = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "yMax":
                        rawData.yMax = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "zMax":
                        rawData.zMax = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "logInit":
                        rawData.logInit = rootEntryNode.GetBasicValBool();
                        break;
                    case "deltaU":
                        rawData.deltaU = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "deltaV":
                        rawData.deltaV = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Uperiods":
                        rawData.Uperiods = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Vperiods":
                        rawData.Vperiods = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "zPeak":
                        rawData.zPeak = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "zInversion":
                        rawData.zInversion = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "widthInversion":
                        rawData.widthInversion = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Tbottom":
                        rawData.Tbottom = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Ttop":
                        rawData.Ttop = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "dTdz":
                        rawData.dTdz = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Ug":
                        rawData.Ug = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "UgDir":
                        rawData.UgDir = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "z0":
                        rawData.z0 = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "kappa":
                        rawData.kappa = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "updateInternalFields":
                        rawData.updateInternalFields = rootEntryNode.GetBasicValBool();
                        break;
                    case "updateBoundaryFields":
                        rawData.updateBoundaryFields = rootEntryNode.GetBasicValBool();
                        break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (SetFieldsAblDictData)data;
            var t = new StringBuilder(SetFieldsAblDictRes.Template);
            t.Replace("({[[xMax]]})", d.xMax.ToString());
            t.Replace("({[[yMax]]})", d.yMax.ToString());
            t.Replace("({[[zMax]]})", d.zMax.ToString());
            t.Replace("({[[logInit]]})", d.logInit.ToString().ToLowerInvariant());
            t.Replace("({[[deltaU]]})", d.deltaU.ToString());
            t.Replace("({[[deltaV]]})", d.deltaV.ToString());
            t.Replace("({[[Uperiods]]})", d.Uperiods.ToString());
            t.Replace("({[[Vperiods]]})", d.Vperiods.ToString());
            t.Replace("({[[zPeak]]})", d.zPeak.ToString());
            t.Replace("({[[zInversion]]})", d.zInversion.ToString());
            t.Replace("({[[widthInversion]]})", d.widthInversion.ToString());
            t.Replace("({[[Tbottom]]})", d.Tbottom.ToString());
            t.Replace("({[[Ttop]]})", d.Ttop.ToString());
            t.Replace("({[[dTdz]]})", d.dTdz.ToString());
            t.Replace("({[[Ug]]})", d.Ug.ToString());
            t.Replace("({[[UgDir]]})", d.UgDir.ToString());
            t.Replace("({[[z0]]})", d.z0.ToString());
            t.Replace("({[[kappa]]})", d.kappa.ToString());
            t.Replace("({[[updateInternalFields]]})", d.updateInternalFields.ToString().ToLowerInvariant());
            t.Replace("({[[updateBoundaryFields]]})", d.updateBoundaryFields.ToString().ToLowerInvariant());

            WriteToFile(path, t.ToString());
        }
    }
}
