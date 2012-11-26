using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.AblProperties
{
    public sealed class AblPropertiesHandler : FoamFileHandler
    {
        public AblPropertiesHandler()
            : base("ABLProperties", null, "constant", AblRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new AblPropertiesData();
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
                    case "turbineArrayOn":
                        rawData.turbineArrayOn = rootEntryNode.GetBasicValBool();
                        break;
                    case "driveWindOn":
                        rawData.driveWindOn = rootEntryNode.GetBasicValBool();
                        break;
                    case "UWindSpeed":
                        rawData.UWindSpeedDim = rootEntryNode.GetDimVal();
                        break;
                    case "UWindDir":
                        rawData.UWindDir = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "hWind":
                        rawData.HWindDim = rootEntryNode.GetDimVal();
                        break;
                    case "alpha":
                        rawData.alpha = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "lowerBoundaryName":
                        rawData.lowerBoundaryName = rootEntryNode.GetBasicValString();
                        break;
                    case "upperBoundaryName":
                        rawData.upperBoundaryName = rootEntryNode.GetBasicValString();
                        break;
                    case "statisticsOn":
                        rawData.statisticsOn = rootEntryNode.GetBasicValBool();
                        break;
                    case "statisticsFrequency":
                        rawData.statisticsFrequency = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "meanAvgStartTime":
                        rawData.meanAvgStartTime = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "corrAvgStartTime":
                        rawData.corrAvgStartTime = rootEntryNode.GetBasicValDecimal();
                        break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (AblPropertiesData)data;
            var t = new StringBuilder(AblRes.Template);
            t.Replace("({[[turbineArrayOn]]})", d.turbineArrayOn.ToString().ToLowerInvariant());
            t.Replace("({[[driveWindOn]]})", d.driveWindOn.ToString().ToLowerInvariant());
            t.Replace("({[[UWindSpeed]]})", d.UWindSpeedDim.ScalarValue.ToString());
            t.Replace("({[[UWindDir]]})", d.UWindDir.ToString());
            t.Replace("({[[hWind]]})", d.HWindDim.ScalarValue.ToString());
            t.Replace("({[[alpha]]})", d.alpha.ToString());
            t.Replace("({[[lowerBoundaryName]]})", d.lowerBoundaryName);
            t.Replace("({[[upperBoundaryName]]})", d.upperBoundaryName);
            t.Replace("({[[statisticsOn]]})", d.statisticsOn.ToString().ToLowerInvariant());
            t.Replace("({[[statisticsFrequency]]})", d.statisticsFrequency.ToString());
            t.Replace("({[[meanAvgStartTime]]})", d.meanAvgStartTime.ToString());
            t.Replace("({[[corrAvgStartTime]]})", d.corrAvgStartTime.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
