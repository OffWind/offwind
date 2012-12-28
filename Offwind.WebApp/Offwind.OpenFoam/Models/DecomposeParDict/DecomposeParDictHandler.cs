using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public sealed class DecomposeParDictHandler : FoamFileHandler
    {
        public DecomposeParDictHandler()
            : base("decomposeParDict", null, "system", DecomposeParDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var obj = new DecomposeParDictData();
            string txt = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(txt);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                switch (identifier)
                {
                    case "numberOfSubdomains":
                        obj.numberOfSubdomains = rootEntryNode.GetBasicValInt();
                        break;
                    case "method":
                        obj.method = rootEntryNode.GetBasicValEnum<DecompositionMethod>();
                        break;
                    case "hierarchicalCoeffs":
                        obj.hCoefs = GetHierarchicalCoeffs(rootEntryNode.GetDictContent());
                        break;
                }
            }
            return obj;
        }

        private HierarchicalCoeffs GetHierarchicalCoeffs(ParseTreeNode content)
        {
            var x = new HierarchicalCoeffs();
            foreach (var entry in content.ChildNodes)
            {
                var h = entry.ChildNodes[0];
                switch (h.GetEntryIdentifier())
                {
                    case "n":
                        {
                            var m = h.GetDictArrayBody().GetArrayOfDecimal();
                            x.n = new Vertice()
                                      {
                                          X = m[0],
                                          Y = m[1],
                                          Z = m[2]
                                      };
                        }
                        break;
                    case "delta":
                        x.delta = h.GetBasicValDecimal();
                        break;
                    case "order":
                        x.order = h.GetBasicValEnum<DecompositionOrder>();
                        break;
                }
            }
            return x;
        }

        public override void Write(string path, object data)
        {
            var obj = (DecomposeParDictData) data;
            var str = new StringBuilder(DecomposeParDictRes.Template);

            str.Replace("({[[numberOfSubdomains]]})", obj.numberOfSubdomains.ToString());
            str.Replace("({[[method]]})", obj.method.ToString());
            str.Replace("({[[nx]]})", obj.hCoefs.n.X.ToString());
            str.Replace("({[[ny]]})", obj.hCoefs.n.Y.ToString());
            str.Replace("({[[nz]]})", obj.hCoefs.n.Z.ToString());
            str.Replace("({[[detla]]})", obj.hCoefs.delta.ToString());
            str.Replace("({[[order]]})", obj.hCoefs.order.ToString());
            WriteToFile(path, str.ToString());
        }
    }
}