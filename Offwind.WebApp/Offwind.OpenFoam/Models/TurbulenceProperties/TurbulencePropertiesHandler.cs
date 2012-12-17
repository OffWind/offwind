using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.TurbulenceProperties
{
    public sealed class TurbulencePropertiesHandler : FoamFileHandler
    {
        public TurbulencePropertiesHandler()
            : base("turbulenceProperties", null, "constant", TurbulencePropertiesRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(TurbulencePropertiesRes.Default);
            WriteToFile(path, result.ToString());
        }
    }
}