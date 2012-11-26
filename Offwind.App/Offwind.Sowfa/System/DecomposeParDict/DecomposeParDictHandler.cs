using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.DecomposeParDict
{
    public sealed class DecomposeParDictHandler : FoamFileHandler
    {
        public DecomposeParDictHandler()
            : base("decomposeParDict", null, "system", DecomposeParDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new DecomposeParDictData();
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
                    case "numberOfSubdomains":
                        rawData.numberOfSubdomains = rootEntryNode.GetBasicValInt();
                        break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (DecomposeParDictData)data;
            var t = new StringBuilder(DecomposeParDictRes.Template);
            t.Replace("({[[numberOfSubdomains]]})", d.numberOfSubdomains.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
