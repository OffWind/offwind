using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class SystemControlsController : __BaseCfdController
    {
        public ActionResult Time()
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