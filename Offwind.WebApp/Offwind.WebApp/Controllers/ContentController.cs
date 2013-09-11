using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;

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

            return View("Show", dPage);
        }

    }
}
