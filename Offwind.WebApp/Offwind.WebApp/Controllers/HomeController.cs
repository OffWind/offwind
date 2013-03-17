using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new VWebPage());
        }

        public ActionResult Contacts()
        {
            return View(new VWebPage());
        }
    }
}
