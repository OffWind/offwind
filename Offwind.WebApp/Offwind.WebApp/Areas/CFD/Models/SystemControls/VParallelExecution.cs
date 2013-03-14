using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Offwind.OpenFoam.Models.DecomposeParDict;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Models.SystemControls
{
    public class VHierarchicalCoeffs
    {
        [DisplayName("n")]
        public VVertice n { set; get; }
        [DisplayName("delta")]
        public Decimal delta { set; get; }
        [DisplayName("order")]
        public DecompositionOrder order { set; get; }
    }

    public class VParallelExecution : VWebPage
    {
        [DisplayName("Number of subdomains")]
        public int numberOfSubdomains { set; get; }
        [DisplayName("Method")]
        public DecompositionMethod method { set; get; }
        public VHierarchicalCoeffs coefs { set; get; }

        public VParallelExecution()
        {
            coefs = new VHierarchicalCoeffs();
            coefs.n = new VVertice();
        }
    }
}
