using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public sealed class DecomposeParDictData
    {
        public int numberOfSubdomains { set; get; }
        public DecompositionMethod method { set; get; }
        public HierarchicalCoeffs hCoefs { set; get; }
    }
}
