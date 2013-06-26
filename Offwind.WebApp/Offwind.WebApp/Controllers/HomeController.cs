using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var m = new VWebPage();
            m.BrowserTitle = "Offwind - prediction tools for offshore wind energy generation";
            return View(m);
        }

        public ActionResult Contacts()
        {
            return View(new VWebPage());
        }
    }
}
