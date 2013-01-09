using System.Text;
using Irony.Parsing;
using Offwind.OpenFoam.Models.TurbulenceModels;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class TurbulencePropertiesHandler : FoamFileHandler
    {
        private FoamFileHandler _fileHandler;

        public TurbulencePropertiesHandler()
            : base("turbulenceProperties", null, "constant", TurbulencePropertiesRes.Default)
        {
        }

        public override object Read(string path)
        {
            var obj = new TurbulencePropertiesData();
            string text = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(text);

            _fileHandler = null;

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                if (rootEntryNode.GetEntryIdentifier() == "simulationType")
                {
                    obj.SimulationType = rootEntryNode.GetBasicValEnum<TurbulenceModel>();
                    if (obj.SimulationType == TurbulenceModel.RASModel)
                    {
                        _fileHandler = new RASPropertiesHandler();
                        obj.RasProperties = (RASPropertiesData) _fileHandler.Read(path);
                    }
                    else if (obj.SimulationType == TurbulenceModel.LESModel)
                    {
                        _fileHandler = new LESPropertiesHandler();
                        obj.LesProperties = (LESPropertiesData) _fileHandler.Read(path);
                    }
                    break;
                }
            }
            return obj;
        }

        public override void Write(string path, object data)
        {
            var obj = (TurbulencePropertiesData) data;
            var txt = new StringBuilder(TurbulencePropertiesRes.Template);
            txt.Replace( "({[[simulationType]]})", obj.SimulationType.ToString());
            WriteToFile(path, txt.ToString());

            if (_fileHandler != null)
            {
                path = path.Replace("\\constant\\turbulenceProperties", "");
                _fileHandler.Write(_fileHandler.GetPath(path),
                                    (obj.SimulationType == TurbulenceModel.RASModel)
                                        ? (object) obj.RasProperties
                                        : obj.LesProperties);
            }
        }
    }
}