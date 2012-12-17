using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public sealed class DecomposeParDictHandler : FoamFileHandler
    {
        public DecomposeParDictHandler()
            : base("decomposeParDict", null, "system", DecomposeParDictRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(DecomposeParDictRes.Default);
            WriteToFile(path, result.ToString());
        }
    }
}