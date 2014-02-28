using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;
using Offwind.Web.Core.Extensions;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public sealed class ManagementController : ManagmentControllerBase
    {
        [Description("Home",1)]
        public ActionResult Home()
        {
            var content = _ctx.DContents.Where(x => x.DContentCategory.Name == CategoryNames.Home && x.TypeId == ContentTypes.Block)
                .ToList()
                .Select(x => new { x.Title, x.Id }.ToExpando())
                .ToList();

            var carousel = _ctx.DContents.Where(x => x.DContentCategory.Name == CategoryNames.Home && x.TypeId == ContentTypes.Carousel)
                .ToList()
                .Select(x => new { x.Title, x.Id }.ToExpando())
                .ToList();

            ViewModel.TotalCount = content.Count + carousel.Count;
            ViewModel.TextBlocks = content;
            ViewModel.Carousel = carousel;
            
            return View(ViewModel);
        }

        [Description("News", 2)]
        public ActionResult News()
        {
            var content = _ctx.Pages.Where(x => x.PageType == PageTypes.News).OrderByDescending(x=>x.Published).ToList();
            
            return View(content);
        }

        [Description("About", 3)]
        public ActionResult About()
        {
            IEnumerable<DContent> content = _ctx.DContents.Where(x => x.DContentCategory.Name == CategoryNames.About).ToList();
            return View(content);
        }

        [Description("Help", 4)]
        public ActionResult Help()
        {
            return RedirectToAction("Details", "Help");
        }
    }
}
