using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class SystemControlsController : Controller
    {
        public ActionResult TimeControl()
        {
            return View();
        }

        public ActionResult Schemes()
        {
            return View();
        }

        public ActionResult Solution()
        {
            return View();
        }

        public ActionResult ParallelExecution()
        {
            return View();
        }
    }
}