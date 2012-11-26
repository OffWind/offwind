using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Irony.Parsing;
using System;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Parsing;

namespace Offwind.Sowfa.Constant.TurbineProperties
{
    public sealed class TurbineProperiesHandler : FoamFileHandler
    {
        private bool useDefault;
        public TurbineProperiesHandler(string turbine_name, bool useDefaultData) :
            base(turbine_name, null, "constant/turbineProperties", TurbinePropRes.Default)
        {
            useDefault = useDefaultData;
        }

        public override object Read(string path)
        {
            var obj = new TurbinePropertiesData();
            string text;

            if (!File.Exists(path))
            {
                if (!useDefault) return obj;
                WriteToFile(path, DefaultData);
            }

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

                switch ( identifier )
                {
                    case "NumBl":
                        obj.NumBl = rootEntryNode.GetBasicValInt();
                        break;
                    case "TipRad":
                        obj.TipRad = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "HubRad":
                        obj.HubRad = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "UndSling":
                        obj.UndSling = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "OverHang":
                        obj.OverHang = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "TowerHt":
                        obj.TowerHt = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "Twr2Shft":
                        obj.Twr2Shft = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "ShftTilt":
                        obj.ShftTilt = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "PreCone":
                        {
                            var dict = rootEntryNode.GetDictArrayBody();
                            var array = dict.GetArrayOfDecimal();
                            obj.PreCone = new Vertice( array[0], array[1], array[2]);
                        }
                        break;
                    case "GBRatio":
                        obj.GBRatio = rootEntryNode.GetBasicValDecimal();;
                        break;
                    case "GenIner":
                        obj.GenIner = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "HubIner":
                        obj.HubIner = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "BladeIner":
                        obj.BladeIner = rootEntryNode.GetBasicValDecimal();
                        break;
                    case "TorqueControllerType":
                        obj.TorqueControllerType = rootEntryNode.GetBasicValEnum<ControllerType>();
                        break;
                    case "YawControllerType":
                        obj.YawControllerType = rootEntryNode.GetBasicValEnum<ControllerType>();
                        break;
                    case "PitchControllerType":
                        obj.PitchControllerType = rootEntryNode.GetBasicValEnum<ControllerType>();
                        break;
                    case "TorqueControllerParams":
                        obj.torqueControllerParams = TorqueControllerParamsHandler(rootEntryNode.GetDictContent());
                        break;
                    case "PitchControllerParams":
                        obj.pitchControllerParams = PitchControllerParamsHandler(rootEntryNode.GetDictContent());
                        break;
                    case "Airfoils":
                        {
                            var dict = rootEntryNode.GetDictArrayBody();
                            for (int i = 0; i < dict.ChildNodes.Count; i++)
                            {
                                var item = new AirfoilBlade();
                                item.AirfoilName = dict.ChildNodes[i].ChildNodes[0].Token.Text.Replace("\"", "");
                                item.Blade = new List<Vertice>();
                                obj.airfoilBlade.Add(item);
                            }
                        }
                        break;
                    case "BladeData":
                        {
                            var dict = rootEntryNode.GetDictArrayBody();
                            for (int i = 0; i < dict.ChildNodes.Count; i++)
                            {
                                var array_head = dict.ChildNodes[i].ChildNodes[0].ChildNodes[1].ChildNodes;
                                if (array_head.Count > 3)
                                {
                                    var airfoil_id = Convert.ToInt32(array_head[3].ChildNodes[0].Token.Value);
                                    if ((airfoil_id >= 0) && (airfoil_id < obj.airfoilBlade.Count))
                                    {
                                        var blade_prop = new Vertice();
                                        blade_prop.X = Convert.ToDecimal(array_head[0].ChildNodes[0].Token.Value);
                                        blade_prop.Y = Convert.ToDecimal(array_head[1].ChildNodes[0].Token.Value);
                                        blade_prop.Z = Convert.ToDecimal(array_head[2].ChildNodes[0].Token.Value);
                                        obj.airfoilBlade[airfoil_id].Blade.Add(blade_prop);
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            return obj;
        }

        private static TorqueControllerParams TorqueControllerParamsHandler(ParseTreeNode dict)
        {
            var p = new TorqueControllerParams();
            for (int i = 0; i < dict.ChildNodes.Count; i++)
            {
                var node = dict.ChildNodes[i].ChildNodes[0];
                var id = node.GetEntryIdentifier();
                switch ( id )
                {
                    case "CutInGenSpeed":
                        p.CutInGenSpeed = node.GetBasicValDecimal();
                        break;
                    case "RatedGenSpeed":
                        p.RatedGenSpeed = node.GetBasicValDecimal();
                        break;
                    case "Region2StartGenSpeed":
                        p.Region2StartGenSpeed = node.GetBasicValDecimal();
                        break;
                    case "Region2EndGenSpeed":
                        p.Region2EndGenSpeed = node.GetBasicValDecimal();
                        break;
                    case "CutInGenTorque":
                        p.CutInGenTorque = node.GetBasicValDecimal();
                        break;
                    case "RatedGenTorque":
                        p.RatedGenTorque = node.GetBasicValDecimal();
                        break;
                    case "RateLimitGenTorque":
                        p.RateLimitGenTorque = node.GetBasicValDecimal();
                        break;
                    case "KGen":
                        p.KGen = node.GetBasicValDecimal();
                        break;
                    case "TorqueControllerRelax":
                        p.TorqueControllerRelax = node.GetBasicValDecimal();
                        break;
                }
                
            }
            return p;
        }

        private static PitchControllerParams PitchControllerParamsHandler(ParseTreeNode dict)
        {
            var p = new PitchControllerParams();
            for (int i = 0; i < dict.ChildNodes.Count; i++)
            {
                var node = dict.ChildNodes[i].ChildNodes[0];
                var id = node.GetEntryIdentifier();
                switch ( id )
                {
                    case "PitchControlStartPitch":
                        p.PitchControlStartPitch = node.GetBasicValDecimal();
                        break;
                    case "PitchControlEndPitch":
                        p.PitchControlEndPitch = node.GetBasicValDecimal();
                        break;
                    case "PitchControlStartSpeed":
                        p.PitchControlStartSpeed = node.GetBasicValDecimal();
                        break;
                    case "PitchControlEndSpeed":
                        p.PitchControlEndSpeed = node.GetBasicValDecimal();
                        break;
                    case "RateLimitPitch":
                        p.RateLimitPitch = node.GetBasicValDecimal();
                        break;
                }
            }
            return p;
        }


        public override void Write(string path, object data)
        {
            var obj = (TurbinePropertiesData) data;
            var str = new StringBuilder(TurbinePropRes.Template);
            var culture = CultureInfo.InvariantCulture;

            str.Replace("({[[NumBl]]})", obj.NumBl.ToString(culture));
            str.Replace("({[[TipRad]]})", obj.TipRad.ToString(culture));
            str.Replace("({[[HubRad]]})", obj.HubRad.ToString(culture));
            str.Replace("({[[UndSling]]})", obj.UndSling.ToString(culture));
            str.Replace("({[[OverHang]]})", obj.OverHang.ToString(culture));
            str.Replace("({[[TowerHt]]})", obj.TowerHt.ToString(culture));
            str.Replace("({[[Twr2Shft]]})", obj.Twr2Shft.ToString(culture));
            str.Replace("({[[ShftTilt]]})", obj.ShftTilt.ToString(culture));
            str.Replace("({[[x]]})", obj.PreCone.X.ToString(culture));
            str.Replace("({[[y]]})", obj.PreCone.Y.ToString(culture));
            str.Replace("({[[z]]})", obj.PreCone.Z.ToString(culture));
            str.Replace("({[[GBRatio]]})", obj.GBRatio.ToString(culture));
            str.Replace("({[[GenIner]]})", obj.GenIner.ToString(culture));
            str.Replace("({[[HubIner]]})", obj.HubIner.ToString(culture));
            str.Replace("({[[BladeIner]]})", obj.BladeIner.ToString(culture));
            str.Replace("({[[TorqueControllerType]]})", obj.TorqueControllerType.ToString());
            str.Replace("({[[YawControllerType]]})", obj.YawControllerType.ToString());
            str.Replace("({[[PitchControllerType]]})", obj.PitchControllerType.ToString());

            str.Replace("({[[CutInGenSpeed]]})", obj.torqueControllerParams.CutInGenSpeed.ToString(culture));
            str.Replace("({[[RatedGenSpeed]]})", obj.torqueControllerParams.RatedGenSpeed.ToString(culture));
            str.Replace("({[[Region2StartGenSpeed]]})", obj.torqueControllerParams.Region2StartGenSpeed.ToString(culture));
            str.Replace("({[[Region2EndGenSpeed]]})", obj.torqueControllerParams.Region2EndGenSpeed.ToString(culture));
            str.Replace("({[[CutInGenTorque]]})", obj.torqueControllerParams.CutInGenTorque.ToString(culture));
            str.Replace("({[[RatedGenTorque]]})", obj.torqueControllerParams.RatedGenTorque.ToString(culture));
            str.Replace("({[[RateLimitGenTorque]]})", obj.torqueControllerParams.RateLimitGenTorque.ToString(culture));
            str.Replace("({[[KGen]]})", obj.torqueControllerParams.KGen.ToString(culture));
            str.Replace("({[[TorqueControllerRelax]]})", obj.torqueControllerParams.TorqueControllerRelax.ToString(culture));

            str.Replace("({[[PitchControlStartPitch]]})", obj.pitchControllerParams.PitchControlStartPitch.ToString(culture));
            str.Replace("({[[PitchControlEndPitch]]})", obj.pitchControllerParams.PitchControlEndPitch.ToString(culture));
            str.Replace("({[[PitchControlStartSpeed]]})", obj.pitchControllerParams.PitchControlStartSpeed.ToString(culture));
            str.Replace("({[[PitchControlEndSpeed]]})", obj.pitchControllerParams.PitchControlEndSpeed.ToString(culture));
            str.Replace("({[[RateLimitPitch]]})", obj.pitchControllerParams.RateLimitPitch.ToString(culture));

            if (obj.airfoilBlade != null)
            {
                var airfoil_objs = new StringBuilder();
                var blade_props = new StringBuilder();

                for (int i = 0; i < obj.airfoilBlade.Count; i++)
                {
                    var item = obj.airfoilBlade[i];
                    airfoil_objs.Append(String.Format("\t\"{0}\"\n", item.AirfoilName));
                    for (int j = 0; j < item.Blade.Count; j++)
                    {
                        var x = item.Blade[j];
                        blade_props.Append(String.Format("\t( {0} {1} {2} {3} )\n", x.X.ToString(culture),
                                                                                    x.Y.ToString(culture),
                                                                                    x.Z.ToString(culture), i));
                    }
                }

                str.Replace("({[[Airfoils]]})", airfoil_objs.ToString());
                str.Replace("({[[BladeData]]})", blade_props.ToString());
            }
            WriteToFile(path, str.ToString());
        }
    }
}
