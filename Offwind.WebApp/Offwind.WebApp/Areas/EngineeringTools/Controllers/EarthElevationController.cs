using System.Threading;
using System.Web.Mvc;

namespace MvcApplication1.Areas.EngineeringTools.Controllers
{
    public class EarthElevationController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "World Map | STL Earth Elevation | Offwind";
            return View();
        }

        public FileResult Generate()
        {
            ViewBag.Title = "Generate | STL Earth Elevation | Offwind";
            Thread.Sleep(3000);
            return File(new byte[0], "text/plain", "result.stl");
        }

        public ActionResult Results()
        {
            ViewBag.Title = "Results | STL Earth Elevation | Offwind";
            return View();
        }
    }
}
