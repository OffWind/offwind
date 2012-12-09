using System.Threading;
using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class PreprocessingController : __BaseCfdController
    {
        public PreprocessingController()
        {
            SectionTitle = "Pre-processing";
        }

        public ActionResult AblProperties()
        {
            Title = "Atmospheric Boundary Layer (ABL) Setup";
            ShortTitle = "ABL Properties";

            return View();
        }

        public ActionResult StlGenerator()
        {
            Title = "Earth Elevation STL Generator";
            ShortTitle = "STL Generator";
            return View();
        }

        public FileResult GenerateStl()
        {
            Thread.Sleep(3000);
            return File(new byte[0], "text/plain", "result.stl");
        }

        public ActionResult TransportProperties()
        {
            ShortTitle = "Transport Properties";
            return View();
        }
    }
}
