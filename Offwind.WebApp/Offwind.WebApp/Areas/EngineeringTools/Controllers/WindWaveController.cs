using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Areas.EngineeringTools.Controllers
{
    public class WindWaveController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PowerCalculator()
        {
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }
    }
}
