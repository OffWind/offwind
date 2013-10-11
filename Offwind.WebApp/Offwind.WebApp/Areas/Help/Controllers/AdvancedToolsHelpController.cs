using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Help.Controllers
{
    public class AdvancedToolsHelpController : _BaseController
    {
        public ActionResult Index()
        {
            _noNavigation = true;
            var m = new VWebPage();
            ViewBag.Title = "Help | Offwind";
            return View(m);
        }
    }
}
