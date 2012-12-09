using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class AirfoilAndTurbulenceController : __BaseCfdController
    {
        public ActionResult AirfoilProperties()
        {
            return View();
        }

        public ActionResult TurbulenceProperties()
        {
            return View();
        }
    }
}