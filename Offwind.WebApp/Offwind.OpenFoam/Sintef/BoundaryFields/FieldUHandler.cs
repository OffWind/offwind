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


            t1.Replace("({[[westUref]]})", obj.WestBoundary.Uref.ToString());
            t1.Replace("({[[westHref]]})", obj.WestBoundary.Href.ToString());
            t1.Replace("({[[westn]]})", VerticeFormat(obj.WestBoundary.n));
            t1.Replace("({[[westz]]})", VerticeFormat(obj.WestBoundary.z));
            t1.Replace("({[[westz0]]})", obj.WestBoundary.z0.Value.ToString());
            t1.Replace("({[[westzGround]]})", obj.WestBoundary.zGround.Value.ToString());
            t1.Replace("({[[westValue]]})", ValueVectorFormat(obj.WestValue));

            t1.Replace("({[[southUref]]})", obj.SouthBoundary.Uref.ToString());
            t1.Replace("({[[southHref]]})", obj.SouthBoundary.Href.ToString());
            t1.Replace("({[[southn]]})", VerticeFormat(obj.SouthBoundary.n));
            t1.Replace("({[[southz]]})", VerticeFormat(obj.SouthBoundary.z));
            t1.Replace("({[[southz0]]})", obj.SouthBoundary.z0.Value.ToString());
            t1.Replace("({[[southzGround]]})", obj.SouthBoundary.zGround.Value.ToString());
            t1.Replace("({[[southValue]]})", ValueVectorFormat(obj.SouthValue));

            t1.Replace("({[[bottomType]]})", obj.BottomType.ToString());
            t1.Replace("({[[bottomValue]]})", ValueVectorFormat(obj.BottomValue));

            WriteToFile(path, t1.ToString());
        }
    }
}
