using System.Collections.Generic;

namespace Offwind.Products.OpenFoam.Models.Fields
{
    public class BoundaryField
    {
        public List<Patch> Patches { get; set; }
        public BoundaryField()
        {
            Patches = new List<Patch>();
        }
    }
}
