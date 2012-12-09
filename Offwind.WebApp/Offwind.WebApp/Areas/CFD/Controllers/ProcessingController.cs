using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class ProcessingController : Controller
    {
        public ActionResult Settings()
        {
            return View();
        }

        public ActionResult Simulation()
        {
            return View();
        }
    }
}