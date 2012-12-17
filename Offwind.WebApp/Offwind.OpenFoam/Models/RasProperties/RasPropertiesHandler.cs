using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.RasProperties
{
    public sealed class RasPropertiesHandler : FoamFileHandler
    {
        public RasPropertiesHandler()
            : base("RASProperties", null, "constant", RasPropertiesRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(RasPropertiesRes.Default);
            WriteToFile(path, result.ToString());
        }
    }
}