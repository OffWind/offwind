using System.Web.Mvc;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    [Authorize(Roles = SystemRole.RegularUser)]
    public class WindWaveController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PowerCalculator()
        {
            return View();
        }

        public ActionResult Results()
        {
            return View();
        }
    }
}
