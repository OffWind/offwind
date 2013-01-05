using System.Globalization;
using System.IO;
using System.Linq.Expressions;
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
        public AirfoilPropertiesHandler(string airfoil_object) :
            base(airfoil_object, null, "constant/airfoilProperties", AirfoilPropRes.Default)
        {
        }

        private static string GetPropertyName(Expression<Func<string>> propertySelector)
        {
            var memberExpression = propertySelector.Body as MemberExpression;
            if (memberExpression == null)
                throw new ArgumentException();
            string name = memberExpression.Member.Name;
            return name;
        }

        public AirfoilPropertiesData ReadDefault()
        {
            var obj = new AirfoilPropertiesData();
            for (var i = 0; i < obj.DefaultAirfoils.Length; i++)
            {
                DefaultData = obj.DefaultAirfoils[i];
                var item = (AirfoilPropertiesInstance)Read(null);
                item.airfoilName = obj.DefaultFiles[i];
                obj.collection.Add(item);
            }           
            return obj;
        }

        public override object Read(string path)
        {
            var obj = new AirfoilPropertiesInstance();
            string text = Load(path);

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(text);
            obj.row = new List<Vertice>();

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var id = rootEntryNode.GetEntryIdentifier();
                if (id == "airfoilData")
                {
                    var dict = rootEntryNode.GetDictContent().ChildNodes[1];
                    foreach (ParseTreeNode t in dict.ChildNodes)
                    {
                        var array_head = t.ChildNodes[0].ChildNodes[1].ChildNodes;
                        var v = new Vertice
                                    {
                                        X = Convert.ToDecimal(array_head[0].ChildNodes[0].Token.Value),
                                        Y = Convert.ToDecimal(array_head[1].ChildNodes[0].Token.Value),
                                        Z = Convert.ToDecimal(array_head[2].ChildNodes[0].Token.Value)
                                    };
                        obj.row.Add( v );
                    }
                }
            }
            obj.airfoilName = FileName;
            return obj;
        }

        public override void Write(string path, object data)
        {
            var obj = (AirfoilPropertiesInstance)data;            
            var str = new StringBuilder(AirfoilPropRes.Template);
            var culture = CultureInfo.InvariantCulture;
            var p = new StringBuilder();

            for (int i = 0; i < obj.row.Count; i++)
            {
                var x = obj.row[i];
                p.Append(String.Format("{0}( {1} {2} {3} ){4}", _indent, x.X.ToString(culture),
                                                              x.Y.ToString(culture),
                                                              x.Z.ToString(culture),
                                                              Environment.NewLine));
            }
            str.Replace("({[[airfoilData]]})", p.ToString());
            WriteToFile(path, str.ToString());
        }
    }
}

