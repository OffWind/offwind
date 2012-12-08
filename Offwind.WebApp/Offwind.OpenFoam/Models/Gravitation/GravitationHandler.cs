using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.Gravitation
{
    public sealed class GravitationHandler : FoamFileHandler
    {
        public GravitationHandler()
            : base("g", null, "constant", GravitationRes.Default)
        {
        }

        public override object Read(string path)
        {
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);
            var d = new Vertice();

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "value":
                        d.X = rootEntryNode.GetDictArrayBody().GetArrayOfDecimal()[0];
                        d.Y = rootEntryNode.GetDictArrayBody().GetArrayOfDecimal()[1];
                        d.Z = rootEntryNode.GetDictArrayBody().GetArrayOfDecimal()[2];
                        break;
                }
            }
            return d;
        }

        public override void Write(string path, object data)
        {
            var d = (Vertice)data;
            var t = new StringBuilder(GravitationRes.Template);
            t.Replace("({[[x]]})", d.X.ToString());
            t.Replace("({[[y]]})", d.Y.ToString());
            t.Replace("({[[z]]})", d.Z.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
