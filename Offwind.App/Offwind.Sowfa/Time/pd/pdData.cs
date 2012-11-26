using System.Collections.Generic;
using Offwind.OpenFoam.Fields;

namespace Offwind.Sowfa.Time.pd
{
    public sealed class pdData
    {
        public List<Patch> Patches { get; set; }

        public pdData()
        {
            Patches = new List<Patch>();
        }
    }
}
