
using System.Globalization;
using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldEpsilonHandler : FoamFileHandler
    {
        public FieldEpsilonHandler() :
            base("epsilon", null, "0", null)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var obj = (FieldEpsilon)data;
            var t1 = new StringBuilder(BoundaryFieldsRes.TemplateEpsilon);

            t1.Replace("({[[internalField]]})", obj.InternalField.ToString());
            t1.Replace("({[[bottomType]]})", obj.BottomType.ToString());
            t1.Replace("({[[bottomCmu]]})", obj.BottomValue.Cmu.ToString());
            t1.Replace("({[[bottomKappa]]})", obj.BottomValue.kappa.ToString());
            t1.Replace("({[[bottomE]]})", obj.BottomValue.E.ToString());
            t1.Replace("({[[bottomValue]]})", obj.BottomValue.value.Value.ToString());

            t1.Replace("({[[topType]]})", obj.TopType.ToString());
            t1.Replace("({[[topCmu]]})", obj.TopValue.Cmu.ToString());
            t1.Replace("({[[topKappa]]})", obj.TopValue.kappa.ToString());
            t1.Replace("({[[topE]]})", obj.TopValue.E.ToString());
            t1.Replace("({[[topValue]]})", obj.TopValue.value.Value.ToString());

            t1.Replace("({[[westValue]]})", obj.WestValue.Value.ToString());
            t1.Replace("({[[southValue]]})", obj.SouthValue.Value.ToString());
            WriteToFile(path, t1.ToString());           
        }
    }
}
