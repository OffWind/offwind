using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.OpenFoam.Models.TransportProperties;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.TransportProperties
{
    public sealed class TransportPropertiesHandler : FoamFileHandler
    {
        public TransportPropertiesHandler()
            : base("transportProperties", null, "constant", TransportPropertiesRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new TransportPropertiesData();
            string txt = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "transportModel":
                        rawData.transportModel = rootEntryNode.GetBasicValEnum<TransportModel>();
                        break;
                    case "nu":
                        rawData.MolecularViscosity = rootEntryNode.GetDimVal().ScalarValue;
                        break;
                    case "TRef":
                        rawData.TRef = rootEntryNode.GetDimVal().ScalarValue;
                        break;
                    case "LESModel":
                        rawData.LESModel = rootEntryNode.GetBasicValEnum<LesModel>();
                        break;
                    case "Cs":
                        rawData.Cs = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "deltaLESCoeff":
                        rawData.deltaLESCoeff = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "kappa":
                        rawData.kappa = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "betaM":
                        rawData.betaM = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "gammM":
                        rawData.gammM = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "z0":
                        rawData.z0 = rootEntryNode.GetDimVal().ScalarValue;
                        break;
                    case "q0":
                        rawData.q0 = rootEntryNode.GetDimVal().ScalarValue;
                        break;
                    case "surfaceStressModel":
                        rawData.surfaceStressModel = rootEntryNode.GetBasicValEnum<SurfaceStressModel>();
                        break;
                    case "betaSurfaceStress":
                        rawData.betaSurfaceStress = rootEntryNode.GetBasicValDecimal();
                        break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (TransportPropertiesData)data;

            /*
            var t = new StringBuilder(TransportPropertiesRes.Template);
            t.Replace("({[[transportModel]]})", d.transportModel.ToString());
            t.Replace("({[[nu]]})", d.MolecularViscosity.ToString());
            t.Replace("({[[TRef]]})", d.TRef.ToString());
            t.Replace("({[[LESModel]]})", d.LESModel.ToString());
            t.Replace("({[[Cs]]})", d.Cs.ToString());
            t.Replace("({[[deltaLESCoeff]]})", d.deltaLESCoeff.ToString());
            t.Replace("({[[kappa]]})", d.kappa.ToString());
            t.Replace("({[[betaM]]})", d.betaM.ToString());
            t.Replace("({[[gammM]]})", d.gammM.ToString());
            t.Replace("({[[z0]]})", d.z0.ToString());
            t.Replace("({[[q0]]})", d.q0.ToString());
            t.Replace("({[[surfaceStressModel]]})", d.surfaceStressModel.ToString());
            t.Replace("({[[betaSurfaceStress]]})", d.betaSurfaceStress.ToString());
             */

            var t = new StringBuilder(TransportPropertiesRes.Template2);
            t.Replace("({[[nu]]})", d.MolecularViscosity.ToString());
            t.Replace("({[[nu0]]})", d.CplcNu0.ToString());
            t.Replace("({[[nuInf]]})", d.CplcNuInf.ToString());
            t.Replace("({[[m]]})", d.CplcM.ToString());
            t.Replace("({[[n]]})", d.CplcN.ToString());

            t.Replace("({[[_nu0]]})", d.BccNu0.ToString());
            t.Replace("({[[_nuInf]]})", d.BccNuInf.ToString());
            t.Replace("({[[_m]]})", d.BccM.ToString());
            t.Replace("({[[_n]]})", d.BccN.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
