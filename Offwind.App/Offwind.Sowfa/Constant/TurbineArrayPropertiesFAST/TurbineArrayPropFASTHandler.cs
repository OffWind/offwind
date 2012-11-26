using System.Globalization;
using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.TurbineArrayPropertiesFAST
{
    public sealed class TurbineArrayPropFASTHandler : FoamFileHandler
    {
        public TurbineArrayPropFASTHandler() :
            base( "turbineArrayPropertiesFAST", null, "constant", TurbineArrayPropFAST.Default )
        {            
        }

        public override object Read(string path)
        {
            var obj = new TurbineArrayPropFASTData();
            string text;
            using (var reader = new StreamReader(path))
            {
                text = reader.ReadToEnd();
            }

            var grammar = new OpenFoamGrammar();
            var parser = new Parser(grammar);
            var tree = parser.Parse(text);

            foreach (ParseTreeNode rootEntryNode in tree.Root.FindDictEntries(null))
            {
                var identifier = rootEntryNode.GetEntryIdentifier();
                if (identifier.Equals("general"))
                {
                    var dict = rootEntryNode.GetDictContent();
                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        var node = dict.ChildNodes[i].ChildNodes[0];
                        var id = node.GetEntryIdentifier();
                        switch (id)
                        {
                            case "yawAngle":
                                obj.general.yawAngle = node.GetBasicValDecimal();
                                break;
                            case "numberofBld":
                                obj.general.numberofBld = node.GetBasicValInt();
                                break;
                            case "numberofBldPts":
                                obj.general.numberofBldPts = node.GetBasicValInt();
                                break;
                            case "rotorDiameter":
                                obj.general.rotorDiameter = node.GetBasicValDecimal();
                                break;
                            case "epsilon":
                                obj.general.epsilon = node.GetBasicValDecimal();
                                break;
                            case "smearRadius":
                                obj.general.smearRadius = node.GetBasicValDecimal();
                                break;
                            case "effectiveRadiusFactor":
                                obj.general.effectiveRadiusFactor = node.GetBasicValDecimal();
                                break;
                            case "pointInterpType":
                                obj.general.pointInterpType = node.GetBasicValInt();
                                break;
                        }
                    }
                }
                else if (identifier.StartsWith("turbine"))
                {
                    var instance = new TurbineInstanceFAST();
                    var dict = rootEntryNode.GetDictContent();
                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        var node = dict.ChildNodes[i].ChildNodes[0];
                        var id = node.GetEntryIdentifier();
                        switch ( id )
                        {
                            case "refx":
                                instance.refx = node.GetBasicValDecimal();
                                break;
                            case "refy":
                                instance.refy = node.GetBasicValDecimal();
                                break;
                            case "refz":
                                instance.refz = node.GetBasicValDecimal();
                                break;
                            case "hubz":
                                instance.hubz = node.GetBasicValDecimal();
                                break;
                        }
                    }
                    obj.turbine.Add(instance);
                }
            }
            return obj;
        }

        public override void Write(string path, object data)
        {
            var obj = (TurbineArrayPropFASTData) data;
            var str = new StringBuilder(TurbineArrayPropFAST.TemplateGeneral);
            var culture = CultureInfo.InvariantCulture;

            str.Replace("({[[yawAngle]]})", obj.general.yawAngle.ToString(culture));
            str.Replace("({[[numberofBld]]})", obj.general.numberofBld.ToString(culture));
            str.Replace("({[[numberofBldPts]]})", obj.general.numberofBldPts.ToString(culture));
            str.Replace("({[[rotorDiameter]]})", obj.general.rotorDiameter.ToString(culture));
            str.Replace("({[[epsilon]]})", obj.general.epsilon.ToString(culture));
            str.Replace("({[[smearRadius]]})", obj.general.smearRadius.ToString(culture));
            str.Replace("({[[effectiveRadiusFactor]]})", obj.general.effectiveRadiusFactor.ToString(culture));
            str.Replace("({[[pointInterpType]]})", obj.general.pointInterpType.ToString(culture));

            for (int i = 0; i < obj.turbine.Count; i++)
            {
                var instance = obj.turbine[i];
                str.Append(TurbineArrayPropFAST.TemplateTurbine);

                str.Replace("({[[turbine_id]]})", "turbine" + i);
                str.Replace("({[[refx]]})", instance.refx.ToString(culture));
                str.Replace("({[[refy]]})", instance.refy.ToString(culture));
                str.Replace("({[[refz]]})", instance.refz.ToString(culture));
                str.Replace("({[[hubz]]})", instance.hubz.ToString(culture));
            }

            WriteToFile(path, str.ToString());
        }
    }
}
