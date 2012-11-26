using System;
using System.IO;
using System.Text;
using Offwind.OpenFoam;
using Offwind.OpenFoam.Parsing;
using Irony.Parsing;

namespace Offwind.Sowfa.Time.p
{
    public sealed class pHandler : FoamFileHandler
    {
        public pHandler()
        {
            FileName = "p";
            RelativePath = "0";
            GetDefaultData = () => pRes.Default;
        }

        public override object Read(string path)
        {
            var rawData = new pData();
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
                    //case "turbineArrayOn":
                    //    rawData.TurbineArrayOn = rootEntryNode.GetBasicValBool();
                    //    break;
                }
            }
            return rawData;
        }

        public override void Write(string path, object data)
        {
            var d = (pData)data;
            var t = new StringBuilder(pRes.Template);

            WriteToFile(path, t.ToString());
        }
    }
}
