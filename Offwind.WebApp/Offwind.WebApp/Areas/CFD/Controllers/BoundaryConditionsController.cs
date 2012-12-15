using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.WebApp.Areas.CFD.Models.BoundaryConditions;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class BoundaryConditionsController : __BaseCfdController
    {
        public BoundaryConditionsController()
        {
            SectionTitle = "Boundary Conditions";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FieldK()
        {
            ShortTitle = "k";
            return View();
        }

        public ActionResult FieldEpsilon()
        {
            ShortTitle = "epsilon";
            return View();
        }

        public ActionResult FieldP()
        {
            ShortTitle = "p";
            return View();
        }

        public ActionResult FieldR()
        {
            ShortTitle = "R";
            return View();
        }

        public ActionResult FieldU()
        {
            ShortTitle = "U";
            var m = new VFieldU();
            m.BottomType = PatchType.cyclic;
            return View(m);
        }

        [ActionName("FieldU")]
        [HttpPost]
        public ActionResult FieldUSave(VFieldU m)
        {
            ShortTitle = "U";
            return View(m);
        }
    }
}
