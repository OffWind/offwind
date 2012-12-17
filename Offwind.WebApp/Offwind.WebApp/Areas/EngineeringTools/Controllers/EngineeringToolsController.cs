using System.Web.Mvc;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    [Authorize(Roles = SystemRole.RegularUser)]
    public class EngineeringToolsController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Engineering Tools | Offwind";
            return View();
        }
    }
}
