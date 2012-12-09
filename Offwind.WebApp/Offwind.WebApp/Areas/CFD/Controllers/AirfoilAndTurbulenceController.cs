using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class AirfoilAndTurbulenceController : __BaseCfdController
    {
        public AirfoilAndTurbulenceController()
        {
            SectionTitle = "Airfoil & Turbulence";
        }

        public ActionResult AirfoilProperties()
        {
            ShortTitle = "Airfoil Properties";
            return View();
        }

        public ActionResult TurbulenceProperties()
        {
            ShortTitle = "Turbulence Properties";
            return View();
        }
    }
}