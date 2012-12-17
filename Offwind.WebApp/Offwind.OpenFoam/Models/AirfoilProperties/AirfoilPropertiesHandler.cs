using System.Globalization;
using System.IO;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Text;
using Offwind.OpenFoam.Models.AirfoilProperties;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.AirfoilProperties
{
    public sealed class AirfoilPropertiesHandler : FoamFileHandler
    {
        public AirfoilPropertiesHandler( string airfoil_object ) :
            base(airfoil_object, null, "constant/airfoilProperties", AirfoilPropRes.Default)
        {            
        }

        public override object Read(string path)
        {
            var obj = new AirfoilPropertiesData();
            string text;
            using (var reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }
            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(text);
            obj.airfoilData = new List<Vertice>();

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var id = rootEntryNode.GetEntryIdentifier();
                if (id == "airfoilData")
                {
                    var dict = rootEntryNode.GetDictContent().ChildNodes[1];
                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        var array_head = dict.ChildNodes[i].ChildNodes[0].ChildNodes[1].ChildNodes;
                        var v = new Vertice();
                        v.X = Convert.ToDecimal(array_head[0].ChildNodes[0].Token.Value);
                        v.Y = Convert.ToDecimal(array_head[1].ChildNodes[0].Token.Value);
                        v.Z = Convert.ToDecimal(array_head[2].ChildNodes[0].Token.Value);
                        obj.airfoilData.Add( v );
                    }
                }
            }
            return obj;
        }

        public override void Write(string path, object data)
        {
            var obj = (AirfoilPropertiesData) data;            
            var str = new StringBuilder(AirfoilPropRes.Template);
            var culture = CultureInfo.InvariantCulture;
            var p = new StringBuilder();

            for (int i = 0; i < obj.airfoilData.Count; i++)
            {
                var x = obj.airfoilData[i];
                p.Append(String.Format("\t( {0} {1} {2} )\n", x.X.ToString(culture),
                                                              x.Y.ToString(culture),
                                                              x.Z.ToString(culture)));
            }
            str.Replace("({[[airfoilData]]})", p.ToString());
            WriteToFile(path, str.ToString());
        }
    }
}

