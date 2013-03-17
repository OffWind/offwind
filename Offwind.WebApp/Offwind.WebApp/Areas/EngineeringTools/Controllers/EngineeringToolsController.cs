using System.Web.Mvc;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    [Authorize(Roles = SystemRole.RegularUser)]
    public class EngineeringToolsController : _BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Engineering Tools | Offwind";
            var m = new VWebPage();
            InitNavigation(m.Navigation);
            return View(m);
        }
    }
}
