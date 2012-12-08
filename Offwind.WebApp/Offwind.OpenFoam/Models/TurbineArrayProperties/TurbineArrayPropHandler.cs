using System.Globalization;
using System.IO;
using System.Text;
using Irony.Parsing;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.TurbineArrayProperties
{
    public sealed class TurbineArrayPropHandler : FoamFileHandler
    {

        public TurbineArrayPropHandler() :
            base("turbineArrayProperties", null, "constant", TurbineArrayPropRes.Default)
        {
        }


        public override object Read(string path)
        {
            var turbine_prop = new TurbineArrayPropData();
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

                if ( identifier.Equals("globalProperties") )
                {
                    var dict = rootEntryNode.GetDictContent();
                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        var node = dict.ChildNodes[i].ChildNodes[0];
                        var id = node.GetEntryIdentifier();
                        if ( id.Equals("outputControl") )
                        {
                            turbine_prop.outputControl = node.GetBasicValEnum<OutputControl>();
                        }
                        else if ( id.Equals("outputInterval") )
                        {
                            turbine_prop.outputInterval = node.GetBasicValDecimal();
                        }
                    }
                }
                else if ( identifier.StartsWith("turbine") )
                {
                    var item = new TurbineInstance();
                    var dict = rootEntryNode.GetDictContent();
                    for (int i = 0; i < dict.ChildNodes.Count; i++)
                    {
                        var node = dict.ChildNodes[i].ChildNodes[0];
                        var id = node.GetEntryIdentifier();
                        switch ( id )
                        {
                            case "turbineType":
                                item.turbineType = node.GetBasicValString();
                                break;
                            case "baseLocation":
                                item.baseLocation = new Vertice();
                                item.baseLocation.X =node.GetDictArrayBody().GetArrayOfDecimal()[0];
                                item.baseLocation.Y =node.GetDictArrayBody().GetArrayOfDecimal()[1];
                                item.baseLocation.Z =node.GetDictArrayBody().GetArrayOfDecimal()[2];
                                break;
                            case "numBladePoints":
                                item.numBladePoints = node.GetBasicValDecimal();
                                break;
                            case "pointDistType":
                                item.pointDistType = node.GetBasicValString();
                                break;
                            case "pointInterpType":
                                item.pointInterpType = node.GetBasicValEnum<PointInterpType>();
                                break;
                            case "bladeUpdateType":
                                item.bladeUpdateType = node.GetBasicValEnum<BladeUpdateType>();
                                break;
                            case "epsilon":
                                item.epsilon = node.GetBasicValDecimal();
                                break;
                            case "tipRootLossCorrType":
                                item.tipRootLossCorrType = node.GetBasicValEnum<TipRootLossCorrType>();
                                break;
                            case "rotationDir":
                                item.rotationDir = node.GetBasicValString();
                                break;
                            case "Azimuth":
                                item.azimuth = node.GetBasicValDecimal();
                                break;
                            case "RotSpeed":
                                item.rotSpeed = node.GetBasicValDecimal();
                                break;
                            case "Pitch":
                                item.pitch = node.GetBasicValDecimal();
                                break;
                            case "NacYaw":
                                item.nacYaw = node.GetBasicValDecimal();
                                break;
                            case "fluidDensity":
                                item.fluidDensity = node.GetBasicValDecimal();
                                break;
                        }
                    }
                    turbine_prop.turbine.Add(item);
                }
            }
            return turbine_prop;
        }

        public override void Write(string path, object data)
        {
            var obj = (TurbineArrayPropData) data;
            var t1 = new StringBuilder(TurbineArrayPropRes.TemplateGlob);
            var culture = CultureInfo.InvariantCulture;

            t1.Replace("({[[outputControl]]})", obj.outputControl.ToString());
            t1.Replace("({[[outputInterval]]})", obj.outputInterval.ToString(culture));

            for (int i = 0; i < obj.turbine.Count; i++)
            {
                var instance = obj.turbine[i];
                t1.Append(TurbineArrayPropRes.TemplateArray);
                t1.Replace("({[[turnine_id]]})", "turbine" + (i + 1));
                t1.Replace("({[[turbineType]]})", (instance.turbineType == null) ? "none" : instance.turbineType.ToString());
                t1.Replace("({[[x]]})", instance.baseLocation.X.ToString(culture));
                t1.Replace("({[[y]]})", instance.baseLocation.Y.ToString(culture));
                t1.Replace("({[[z]]})", instance.baseLocation.Z.ToString(culture));
                t1.Replace("({[[numBladePoints]]})", instance.numBladePoints.ToString(culture));
                t1.Replace("({[[pointDistType]]})", instance.pointDistType.ToString());
                t1.Replace("({[[pointInterpType]]})", instance.pointInterpType.ToString());
                t1.Replace("({[[bladeUpdateType]]})", instance.bladeUpdateType.ToString());
                t1.Replace("({[[epsilon]]})", instance.epsilon.ToString(culture));
                t1.Replace("({[[tipRootLossCorrType]]})", instance.tipRootLossCorrType.ToString());
                t1.Replace("({[[rotationDir]]})", instance.rotationDir.ToString());
                t1.Replace("({[[Azimuth]]})", instance.azimuth.ToString(culture));
                t1.Replace("({[[RotSpeed]]})", instance.rotSpeed.ToString(culture));
                t1.Replace("({[[Pitch]]})", instance.pitch.ToString(culture));
                t1.Replace("({[[NacYaw]]})", instance.nacYaw.ToString(culture));
                t1.Replace("({[[fluidDensity]]})", instance.fluidDensity.ToString(culture));
            }
            WriteToFile(path, t1.ToString());

        }
    }
}
