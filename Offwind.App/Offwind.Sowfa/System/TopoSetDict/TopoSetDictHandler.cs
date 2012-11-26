using System.IO;
using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.System.TopoSetDict
{
    public sealed class TopoSetDictHandler : FoamFileHandler
    {
        public TopoSetDictHandler()
            : base("topoSetDict", null, "system", TopoSetDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            var rawData = new TopoSetDictData();
            string txt;
            using (var reader = new StreamReader(path))
            {
                txt = reader.ReadToEnd();
            }

            //var grammar = new OpenFoamGrammar();
            //var parser = new Parser(grammar);
            //var tree = parser.Parse(txt);

            //foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            //{
            //    var identifier = rootEntryNode.GetEntryIdentifier();
            //    switch (identifier)
            //    {
            //        //case "turbineArrayOn":
            //        //    rawData.TurbineArrayOn = rootEntryNode.GetBasicValBool();
            //        //    break;
            //    }
            //}
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (TopoSetDictData)data;
            var t = new StringBuilder(TopoSetDictRes.Template);
            t.Replace("({[[X1]]})", d.X1.ToString());
            t.Replace("({[[Y1]]})", d.Y1.ToString());
            t.Replace("({[[Z1]]})", d.Z1.ToString());
            t.Replace("({[[X2]]})", d.X2.ToString());
            t.Replace("({[[Y2]]})", d.Y2.ToString());
            t.Replace("({[[Z2]]})", d.Z2.ToString());

            WriteToFile(path, t.ToString());
        }
    }
}
