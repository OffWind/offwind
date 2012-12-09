using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }
    }
}
