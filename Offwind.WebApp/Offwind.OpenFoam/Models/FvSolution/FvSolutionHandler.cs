using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.FvSolution
{
    public sealed class FvSolutionHandler : FoamFileHandler
    {
        public FvSolutionHandler()
            : base("fvSolution", null, "system", FvSolutionRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(FvSolutionRes.Default);
            WriteToFile(path, result.ToString());
        }
    }
}