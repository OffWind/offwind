using System;
using System.Collections.Generic;
using System.Linq;

namespace Offwind.OpenFoam.Models.DecomposeParDict
{
    public enum DecompositionMethod
    {
        simple,
        hierarchical,
        scotch,
        metis,
        manual
    }
}
