
using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldKHandler : FoamFileHandler
    {
        public FieldKHandler() :
            base("k", null, "0", null)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var obj = (FieldK) data;
            var t1 = new StringBuilder(BoundaryFieldsRes.TemplateK);

            t1.Replace("({[[internalField]]})", obj.InternalField.ToString());

            t1.Replace("({[[bottomType]]})", obj.BottomType.ToString());
            t1.Replace("({[[bottomValue]]})", obj.BottomValue.Value.ToString());

            t1.Replace("({[[topType]]})", obj.TopType.ToString());
            t1.Replace("({[[topValue]]})", obj.TopValue.Value.ToString());

            t1.Replace("({[[westType]]})", obj.WestType.ToString());
            t1.Replace("({[[westValue]]})", obj.WestValue.Value.ToString());

            t1.Replace("({[[southType]]})", obj.SouthType.ToString());
            t1.Replace("({[[southValue]]})", obj.SouthValue.Value.ToString());

            WriteToFile(path, t1.ToString());
        }
    }
}
