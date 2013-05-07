using System.Web.Mvc;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class AboutController : BaseController
    {
        public ActionResult Index()
        {
            return View(new VWebPage());
        }
    }
}
