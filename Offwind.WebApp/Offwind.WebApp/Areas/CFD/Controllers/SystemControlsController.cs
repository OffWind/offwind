using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class SystemControlsController : __BaseCfdController
    {
        public SystemControlsController()
        {
            SectionTitle = "System";
        }

        public ActionResult Time()
        {
            ShortTitle = "Time";
            return View();
        }

        public ActionResult Schemes()
        {
            ShortTitle = "Schemes";
            return View();
        }

        public ActionResult Solution()
        {
            ShortTitle = "Solution";
            return View();
        }

        public ActionResult ParallelExecution()
        {
            ShortTitle = "Parallel Execution";
            return View();
        }
    }
}