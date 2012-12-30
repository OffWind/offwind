using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class RASPropertiesHandler : FoamFileHandler
    {
        public RASPropertiesHandler() : base("RASProperties", null, "constant", TurbulencePropertiesRes.DefaultRAS)
        {            
        }

        public override object Read(string path)
        {
            var obj = new RASPropertiesData();
            string text = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(text);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                switch (rootEntryNode.GetEntryIdentifier())
                {
                    case "RASModel":
                        obj.RasModelName = rootEntryNode.GetBasicValString();
                        break;
                    case "turbulence":
                        obj.Turbulence = rootEntryNode.GetBasicValEnum<OnOffValue>();
                        break;
                    case "printCoeffs":
                        obj.PrintCoeffs = rootEntryNode.GetBasicValEnum<OnOffValue>();
                        break;
                }
            }
            return obj;
        }

        public override void Write(string path, object data)
        {
            var obj = (RASPropertiesData) data;
            var txt = new StringBuilder(TurbulencePropertiesRes.TemplateRAS);

            txt.Replace("({[[model]]})", obj.RasModelName);
            txt.Replace("({[[turbulence]]})", obj.Turbulence.ToString());
            txt.Replace("({[[printCoeffs]]})", obj.PrintCoeffs.ToString());

            WriteToFile(path, txt.ToString());
        }
    }
}
