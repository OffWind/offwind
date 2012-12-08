using System;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.Sowfa.Constant.LesProperties
{
    public sealed class LesPropertiesHandler : FoamFileHandler
    {
        public LesPropertiesHandler()
            : base("LESProperties", null, "constant", LesPropertiesRes.Default)
        {
        }

        public override object Read(string path)
        {
            throw new NotImplementedException();
        }

        public override void Write(string path, object data)
        {
        }
    }
}
