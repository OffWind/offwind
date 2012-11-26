using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.System.RefineMeshDict
{
    public class RefineMeshDictHandler : FoamFileHandler
    {
        public RefineMeshDictHandler() :
            base("refineMeshDict", null, "system", RefineMeshDictRes.Default)
        {       
        }

        public override object Read(string path)
        {
            var obj = new RefineMeshDictData();
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
                string patch;
                switch ( identifier )
                {
                    case "set":
                        obj.setvalue = rootEntryNode.GetBasicValString();
                        break;
                    case "coordinateSystem":
                        obj.coordsys = rootEntryNode.GetBasicValEnum<CoordinateSystem>();
                        break;
                    case "globalCoeffs":
                        {
                            var dict = rootEntryNode.GetDictContent();
                            obj.globalCoeffs = GetCoeffs(ref dict, out patch);
                        }
                        break;
                    case "patchLocalCoeffs":
                        {
                            var dict = rootEntryNode.GetDictContent();
                            obj.patchLocalCoeffs = GetCoeffs(ref dict, out patch);
                            obj.patch = patch;
                        }
                        break;
                    case "directions":
                        {
                            obj.direction = new List<DirectionType>();
                            var s = rootEntryNode.ChildNodes[2].ChildNodes[1].GetArrayOfString();
                            foreach (string t in s)
                            {
                                obj.direction.Add(t.ToEnum<DirectionType>());
                            }
                        }
                        break;
                    case "useHexTopology":
                        obj.useHexTopology = rootEntryNode.GetBasicValBool();
                        break;
                    case "geometricCut":
                        obj.geometricCut = rootEntryNode.GetBasicValBool();
                        break;
                    case "writeMesh":
                        obj.writeMesh = rootEntryNode.GetBasicValBool();
                        break;
                }
            }
            return obj;
        }

        private static List<Coeffs> GetCoeffs(ref ParseTreeNode dictionary, out string patch)
        {
            var ret = new List<Coeffs>();
            patch = null;
            foreach (ParseTreeNode x in dictionary.ChildNodes)
            {
                var id = x.ChildNodes[0].GetEntryIdentifier();
                if (id == "patch")
                {
                    patch = x.ChildNodes[0].GetBasicValString();
                }
                else
                {
                    var item = new Coeffs();
                    item.dir = x.ChildNodes[0].ChildNodes[0].Token.Text.ToEnum<DirectionType>();
                    item.value = new Vertice();
                    var a = x.ChildNodes[0].ChildNodes[2].ChildNodes[1].GetArrayOfDecimal();
                    item.value.X = a[0];
                    item.value.Y = a[1];
                    item.value.Z = a[2];
                    ret.Add(item);
                }               
            }
            return ret;
        }

        public override void Write(string path, object data)
        {
            var obj = (RefineMeshDictData) data;
            var str = new StringBuilder(RefineMeshDictRes.Template);
            var culture = CultureInfo.InvariantCulture;


            str.Replace("({[[set]]})", obj.setvalue);
            str.Replace("({[[coordinateSystem]]})", obj.coordsys.ToString());
            var gbody = new StringBuilder(null);            
            foreach (var x in obj.globalCoeffs)
            {
                gbody.Append(String.Format("\t{0} ({1} {2} {3});{4}",
                                           x.dir, x.value.X.ToString(culture),
                                           x.value.Y.ToString(culture),
                                           x.value.Z.ToString(culture),
                                           Environment.NewLine));
            }
            str.Replace("({[[globalCoeffs]]})", gbody.ToString());

            var pbody = new StringBuilder(null);
            str.Replace("({[[patch]]})", obj.patch);
            foreach (var x in obj.patchLocalCoeffs)
            {
                pbody.Append(String.Format("\t{0} ({1} {2} {3});{4}",
                                           x.dir, x.value.X.ToString(culture),
                                           x.value.Y.ToString(culture),
                                           x.value.Z.ToString(culture),
                                           Environment.NewLine));
            }
            str.Replace("({[[patchLocalCoeffs]]})", pbody.ToString());

            var dbody = new StringBuilder(null);
            foreach (var x in obj.direction)
            {
                dbody.Append(String.Format("\t{0}{1}", x, Environment.NewLine));
            }
            str.Replace("({[[directions]]})", dbody.ToString());

            str.Replace("({[[useHexTopology]]})", obj.useHexTopology.ToString());
            str.Replace("({[[geometricCut]]})", obj.geometricCut.ToString());
            str.Replace("({[[writeMesh]]})", obj.writeMesh.ToString());

            WriteToFile(path, str.ToString());
        }
    }
}
