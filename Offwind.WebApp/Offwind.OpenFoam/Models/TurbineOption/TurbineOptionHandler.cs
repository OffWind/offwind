using System.Text;
using Offwind.OpenFoam.Models.TurbineOption;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public sealed class TurbineOptionHandler : FoamFileHandler
    {
        public TurbineOptionHandler()
            : base("TurbineOption", null, "constant", TurbineOptionRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(TurbineOptionRes.Default);
            WriteToFile(path, result.ToString());
        }
    }
}