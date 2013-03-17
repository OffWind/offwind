using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View(new VWebPage());
        }
    }
}
