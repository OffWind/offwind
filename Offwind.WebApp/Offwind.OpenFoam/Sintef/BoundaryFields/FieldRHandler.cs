using System.Text;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldRHandler : FoamFileHandler
    {
        public FieldRHandler() :
            base("R", null, "0", null)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        private string ValueVector2Format(PatchValueVector2 v)
        {
            var s = new StringBuilder();
            s.AppendFormat("({0} {1} {2} {3} {4} {5})", v.Value1, v.Value2, v.Value3, v.Value4, v.Value5, v.Value6);
            return s.ToString();
        }


        public override void Write(string path, object data)
        {
            var obj = (FieldR) data;
            var t1 = new StringBuilder(BoundaryFieldsRes.TemplateR);

            t1.Replace("({[[internalField]]})", ValueVector2Format(obj.InternalField));
            t1.Replace("({[[westValue]]})", ValueVector2Format(obj.WestValue));
            t1.Replace("({[[southValue]]})", ValueVector2Format(obj.SouthValue));

            WriteToFile(path, t1.ToString());
        }
    }
}
