using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Areas.CFD.Controllers
{
    public class PreprocessingController : Controller
    {
        public ActionResult AblSetup()
        {
            ViewBag.Title = "Atmospheric Boundary Layer (ABL) Setup | Preprocessing | Offwind";
            return View();
        }

        public ActionResult StlGenerator()
        {
            ViewBag.Title = "Earth Elevation STL Generator | Preprocessing | Offwind";
            return View();
        }

        public FileResult GenerateStl()
        {
            Thread.Sleep(3000);
            return File(new byte[0], "text/plain", "result.stl");
        }
    }
}
