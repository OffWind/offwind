using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class LESPropertiesHandler : FoamFileHandler
    {
        public LESPropertiesHandler() : base("LESProperties", null, "constant", TurbulencePropertiesRes.DefaultLES)
        {            
        }

        public override object Read(string path)
        {
            var obj = new LESPropertiesData();
            string text = Load(path);



            return null;
        }

        public override void Write(string path, object data)
        {
            var obj = (LESPropertiesData) data;
            var txt = new StringBuilder(TurbulencePropertiesRes.TemplateLES);

            WriteToFile(path, txt.ToString());
        }
    }
}
