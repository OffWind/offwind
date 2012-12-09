using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class TurbinesController : __BaseCfdController
    {
        public TurbinesController()
        {
            SectionTitle = "Turbines";
        }

        public ActionResult TurbineTypes()
        {
            ShortTitle = "Turbine Types";
            return View();
        }

        public ActionResult TurbineArray()
        {
            ShortTitle = "Turbine Array";
            return View();
        }
    }
}