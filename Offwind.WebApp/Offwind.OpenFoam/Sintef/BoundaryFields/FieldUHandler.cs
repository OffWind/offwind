using System.Text;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldUHandler : FoamFileHandler
    {
        public FieldUHandler() :
            base("U", null, "0", null)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        private string VerticeFormat(Vertice v)
        {
            var s = new StringBuilder();
            s.AppendFormat("({0} {1} {2})", v.X, v.Y, v.Z);
            return s.ToString();
        }

        private string ValueVectorFormat(PatchValueVector v)
        {
            var s = new StringBuilder();
            s.AppendFormat("({0} {1} {2})", v.Value1, v.Value2, v.Value3);
            return s.ToString();            
        }

        public override void Write(string path, object data)
        {
            var obj = (FieldU) data;
            var t1 = new StringBuilder(BoundaryFieldsRes.TemplateU);


            t1.Replace("({[[internalField]]})", ValueVectorFormat(obj.InternalField));


            t1.Replace("({[[westUref]]})", obj.WestValue.Uref.ToString());
            t1.Replace("({[[westHref]]})", obj.WestValue.Href.ToString());
            t1.Replace("({[[westn]]})", VerticeFormat(obj.WestValue.n));
            t1.Replace("({[[westz]]})", VerticeFormat(obj.WestValue.z));
            t1.Replace("({[[westz0]]})", obj.WestValue.z0.Value.ToString());
            t1.Replace("({[[westzGround]]})", obj.WestValue.zGround.Value.ToString());
            t1.Replace("({[[westValue]]})", ValueVectorFormat(obj.WestValue.Value));

            t1.Replace("({[[southUref]]})", obj.SouthValue.Uref.ToString());
            t1.Replace("({[[southHref]]})", obj.SouthValue.Href.ToString());
            t1.Replace("({[[southn]]})", VerticeFormat(obj.SouthValue.n));
            t1.Replace("({[[southz]]})", VerticeFormat(obj.SouthValue.z));
            t1.Replace("({[[southz0]]})", obj.SouthValue.z0.Value.ToString());
            t1.Replace("({[[southzGround]]})", obj.SouthValue.zGround.Value.ToString());
            t1.Replace("({[[southValue]]})", ValueVectorFormat(obj.SouthValue.Value));

            t1.Replace("({[[bottomType]]})", obj.BottomType.ToString());
            t1.Replace("({[[bottomValue]]})", ValueVectorFormat(obj.BottomValue.Value));

            WriteToFile(path, t1.ToString());
        }
    }
}
