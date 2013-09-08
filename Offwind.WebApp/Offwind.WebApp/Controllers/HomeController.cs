using System.Web.Mvc;
using System.Linq;
using Offwind.Web.Core.News;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var m = new VWebPage();
            m.BrowserTitle = "Offwind - prediction tools for offshore wind energy generation";
            ViewBag.LatestPosts = _ctx.Pages
                .Where(p => p.PageType == "News")
                .OrderByDescending(p => p.Published)
                .Take(3)
                .ToList();
            return View(m);
        }

        public ActionResult Contacts()
        {
            return View(new VWebPage());
        }
    }
}
