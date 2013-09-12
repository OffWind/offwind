using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.WebApp.Infrastructure.Navigation;

namespace Offwind.WebApp.Controllers
{
    public class ContentController : BaseController
    {
        public ActionResult ShowById(string id)
        {
            Guid guid;
            DContent dPage;
            dPage = _ctx.DContents.FirstOrDefault(p => p.Route == id);
            if (dPage == null && Guid.TryParse(id, out guid))
            {
                dPage = _ctx.DContents.First(p => p.Id == guid);
            }
            if (dPage == null) return RedirectToAction("Error404", "Home");

            if (!String.IsNullOrWhiteSpace(dPage.BrowserTitle))
            {
                ViewBag.TitleSuffix = dPage.BrowserTitle;
            }
            else
            {
                ViewBag.TitleSuffix = dPage.Title;
            }

            if (!String.IsNullOrWhiteSpace(dPage.MetaDescription))
            {
                ViewBag.MetaDescription = dPage.MetaDescription;
            }

            InitNavigation();

            return View("Show", dPage);
        }

        private void InitNavigation()
        {
            var navigation = new NavItem<string>();
            var group = navigation.AddGroup("Information");
            var cid = Guid.Parse("9EC610BF-4F5B-4E02-A3DA-DA6C853F9B64");
            foreach (var ri in _ctx.VRouteItems.Where(ri => ri.CategoryId == cid).OrderBy(ri => ri.Position))
            {
                group.AddItem(ri.RouteTitle, ri.Route);
            }

            foreach (var grp in navigation)
            {
                grp.IsActive = true;
            }
            ViewBag.SideNav = navigation;
        }
    }
}
