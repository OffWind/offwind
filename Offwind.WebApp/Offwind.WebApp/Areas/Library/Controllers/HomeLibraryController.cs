using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Library.Controllers
{
    public class HomeLibraryController : _BaseController
    {
        public ActionResult Index()
        {
            var m = new VWebPage();
            ViewBag.Title = "Library | Offwind";
            return View(m);
        }

    }
}
