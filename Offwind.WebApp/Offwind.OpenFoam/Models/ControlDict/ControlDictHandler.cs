using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.ControlDict;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.ControlDict
{
    public sealed class ControlDictHandler : FoamFileHandler
    {
        public ControlDictHandler()
            : base("controlDict", null, "system", ControlDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new ControlDictData();
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
                    case "application":
                        rawData.application = rootEntryNode.GetBasicValEnum<ApplicationSolver>();
                        break;
                    case "startFrom":
                        rawData.startFrom = rootEntryNode.GetBasicValEnum<StartFrom>();
                        break;
                    case "startTime":
                        rawData.startTime = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "stopAt":
                        rawData.stopAt = rootEntryNode.GetBasicValEnum<StopAt>();
                        break;
                    case "endTime":
                        rawData.endTime = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "deltaT":
                        rawData.deltaT = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "writeControl":
                        rawData.writeControl = rootEntryNode.GetBasicValEnum<WriteControl>();
                        break;
                    case "writeInterval":
                        rawData.writeInterval = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "purgeWrite":
                        rawData.purgeWrite = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "writeFormat":
                        rawData.writeFormat = rootEntryNode.GetBasicValEnum<WriteFormat>();
                        break;
                    case "writePrecision":
                        rawData.writePrecision = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "writeCompression":
                        rawData.writeCompression = rootEntryNode.GetBasicValEnum<WriteCompression>();
                        break;
                    case "timeFormat":
                        rawData.timeFormat = rootEntryNode.GetBasicValEnum<TimeFormat>();
                        break;
                    case "timePrecision":
                        rawData.timePrecision = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "runTimeModifiable":
                        rawData.runTimeModifiable = rootEntryNode.GetBasicValEnum<FlagYesNo>();
                        break;
                    case "adjustTimeStep":
                        rawData.adjustTimeStep = rootEntryNode.GetBasicValEnum<FlagYesNo>();
                        break;
                    case "maxCo":
                        rawData.maxCo = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "maxDeltaT":
                        rawData.maxDeltaT = rootEntryNode.GetBasicValDecimal();
                        break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (ControlDictData)data;
            var t = new StringBuilder(ControlDictRes.Template);
            t.Replace("({[[application]]})", d.application.ToString());
            t.Replace("({[[startFrom]]})", d.startFrom.ToString());
            t.Replace("({[[startTime]]})", d.startTime.ToString());
            t.Replace("({[[stopAt]]})", d.stopAt.ToString());
            t.Replace("({[[endTime]]})", d.endTime.ToString());
            t.Replace("({[[deltaT]]})", d.deltaT.ToString());
            t.Replace("({[[writeControl]]})", d.writeControl.ToString());
            t.Replace("({[[writeInterval]]})", d.writeInterval.ToString());
            t.Replace("({[[purgeWrite]]})", d.purgeWrite.ToString());
            t.Replace("({[[writeFormat]]})", d.writeFormat.ToString());
            t.Replace("({[[writePrecision]]})", d.writePrecision.ToString());
            t.Replace("({[[writeCompression]]})", d.writeCompression.ToString());
            t.Replace("({[[timeFormat]]})", d.timeFormat.ToString());
            t.Replace("({[[timePrecision]]})", d.timePrecision.ToString());
            t.Replace("({[[runTimeModifiable]]})", d.runTimeModifiable.ToString());
            t.Replace("({[[adjustTimeStep]]})", d.adjustTimeStep.ToString());
            t.Replace("({[[maxCo]]})", d.maxCo.ToString());
            t.Replace("({[[maxDeltaT]]})", d.maxDeltaT.ToString());

            t.Replace("({[[functions]]})", "");

            WriteToFile(path, t.ToString());
        }
    }
}
