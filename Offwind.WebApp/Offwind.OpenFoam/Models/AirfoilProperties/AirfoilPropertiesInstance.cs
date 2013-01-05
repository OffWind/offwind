using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Offwind.Products.OpenFoam.Models;

namespace Offwind.OpenFoam.Models.AirfoilProperties
{
    public sealed class AirfoilPropertiesInstance
    {
        public string airfoilName { set; get; }
        public List<Vertice> row { set; get; }

        public AirfoilPropertiesInstance()
        {
            row = new List<Vertice>();
        }
    }
}
