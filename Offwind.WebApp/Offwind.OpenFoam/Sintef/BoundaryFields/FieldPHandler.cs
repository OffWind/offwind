using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Sintef.BoundaryFields
{
    public class FieldPHandler : FoamFileHandler
    {
        public FieldPHandler() :
            base("p", null, "0", null)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var obj = (FieldP) data;
            var t1 = new StringBuilder(BoundaryFieldsRes.TemplateP);

            t1.Replace("({[[internalField]]})", obj.InternalField.ToString());

            t1.Replace("({[[eastType]]})", obj.EastType.ToString());
            t1.Replace("({[[eastValue]]})", obj.EastValue.Value.ToString());

            t1.Replace("({[[southType]]})", obj.SouthType.ToString());
            t1.Replace("({[[southValue]]})", obj.SouthValue.Value.ToString());

            WriteToFile(path, t1.ToString());
        }

    }
}
