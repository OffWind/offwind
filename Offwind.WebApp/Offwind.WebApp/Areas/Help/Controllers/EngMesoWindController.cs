using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Help.Controllers
{
    public class EngMesoWindController : _BaseController
    {
        public ActionResult Index()
        {
            _noNavigation = true;
            var m = new VWebPage();
            ViewBag.Title = "Mesoscale Wind Database | Engineering Tools | Help | Offwind";
            return View(m);
        }

        public ActionResult Usage()
        {
            _noNavigation = true;
            var m = new VWebPage();
            ViewBag.Title = "Usage | Mesoscale Wind Database | Engineering Tools | Help | Offwind";
            return View(m);
        }
    }
}
