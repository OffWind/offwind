using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;

namespace Offwind.WebApp.Controllers
{
    public class NewsController : BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        public ActionResult Index()
        {
            ViewBag.BrowserTitle = "Offwind News";

            var news = _ctx.Pages
                .Where(p => p.PageType == "News")
                .OrderByDescending(p => p.Published)
                .ToList();

            return View(news);
        }

        public ActionResult Show(int id)
        {
            var page = _ctx.Pages.FirstOrDefault(p => p.Id == id);
            return View(page);
        }
    }
}
