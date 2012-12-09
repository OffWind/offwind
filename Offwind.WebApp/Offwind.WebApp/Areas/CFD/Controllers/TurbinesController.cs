using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class TurbinesController : Controller
    {
        public ActionResult TurbineTypes()
        {
            return View();
        }

        public ActionResult TurbineArray()
        {
            return View();
        }
    }
}