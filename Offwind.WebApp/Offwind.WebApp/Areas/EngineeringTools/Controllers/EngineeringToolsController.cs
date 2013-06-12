using System.Web.Mvc;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class EngineeringToolsController : _BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Engineering Tools | Offwind";
            _noNavigation = true;
            return View(new VWebPage());
        }
    }
}
