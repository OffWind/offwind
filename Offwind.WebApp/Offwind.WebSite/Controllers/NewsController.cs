using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Models;

namespace Offwind.Web.Controllers
{
    public class NewsController : PagesController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ShowBlockNews = false;
        }

        public ActionResult Index()
        {
            ViewBag.BrowserTitle = "Offwind News";

            var news = _ctx.Pages
                .Where(p => p.PageType == PageType.News)
                .OrderByDescending(p => p.Published)
                .ToList()
                .Select(ConvertToVPage);

            var model = new VPagesList();
            model.Title = "News";
            model.PageType = "News";
            model.Pages.AddRange(news);
            return View(model);
        }

        public ActionResult Show(int id)
        {
            var page = _ctx.Pages.FirstOrDefault(p => p.Id == id);
            _breadcrumbs.Add(page.Title);
            ViewBag.BrowserTitle = page.Title;
            return View(ConvertToVPage(page));
        }
    }
}
