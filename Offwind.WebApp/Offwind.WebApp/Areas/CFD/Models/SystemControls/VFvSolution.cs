using System;
using System.Collections.Generic;
using System.ComponentModel;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.SystemControls
{
    public class VSolver
    {
        [ReadOnly(true)]
        public String Name { set; get; }
        public LinearSolver Solver { set; get; }
        public Preconditioner Preconditioner { set; get; }
        public Decimal Tolerance { set; get; }
        public Decimal RelTol { set; get; }
    }

    public class VSolution
    {
        [ReadOnly(true)]
        public String Name { set; get; }
        [DisplayName("nCorrectors")]
        public Int32 nCorrectors { set; get; }
        [DisplayName("nNonOrthogonalCorrectors")]
        public Int32 nNonOrthogonalCorrectors { set; get; }
        [DisplayName("pRefCell")]
        public Decimal pRefCell { set; get; }
        [DisplayName("pRefValue")]
        public Decimal pRefValue { set; get; }
    }

    public class VFvSolution : VWebPage
    {
        public List<VSolver> Solvers { set; get; }
        public VSolution Solution { set; get; }

        public VFvSolution()
        {
            //Solver = new List<VSolver>();
            Solution = new VSolution();
        }
    }
}
