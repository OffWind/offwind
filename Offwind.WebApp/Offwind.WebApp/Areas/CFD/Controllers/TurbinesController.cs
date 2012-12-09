using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class TurbinesController : __BaseCfdController
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