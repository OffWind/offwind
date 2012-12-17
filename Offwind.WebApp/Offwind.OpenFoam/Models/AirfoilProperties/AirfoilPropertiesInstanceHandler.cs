using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.AirfoilProperties
{
    public sealed class AirfoilPropertiesInstanceHandler : FoamFileHandler
    {
        public AirfoilPropertiesInstanceHandler()
            : base("Cylinder1", null, "constant/airfoilProperties", AirfoilPropRes.Default)
        {
        }

        public override object Read(string path)
        {
            return null;
        }

        public override void Write(string path, object data)
        {
            var result = new StringBuilder(AirfoilPropRes.Default);
            WriteToFile(path, result.ToString());
        }

        public void WriteInstance(string path, string txt)
        {
            WriteToFile(path, txt);
        }
    }
}