using System.Collections.Generic;
using Offwind.Products.OpenFoam.Models.Fields;

namespace Offwind.OpenFoam.Models.Fields
{
    public class BoundaryField
    {
        public BoundaryField()
        {
            Patches = new List<Patch>();
        }

        public List<Patch> Patches { get; set; }
    }
}