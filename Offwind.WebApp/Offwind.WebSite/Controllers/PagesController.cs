using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Models;

namespace Offwind.Web.Controllers
{
    public class PagesController : BaseController
    {
        protected Page GetPageByType(string type)
        {
            var model = _ctx.Pages.FirstOrDefault(p => p.PageType == type);
            if (model == null)
            {
                var now = DateTime.UtcNow;
                _ctx.Pages.AddObject(new Page
                {
                    PageType = type,
                    Created = now,
                    Updated = now,
                    Published = now,
                    PageTitle = type,
                    Title = type,
                });
                _ctx.SaveChanges();
                model = _ctx.Pages.First(p => p.PageType == type);
            }
            return model;
        }

        protected VPage ConvertToVPage(Page model)
        {
            var vPage = new VPage
            {
                Announce = model.Announce,
                Updated = model.Updated,
                Published = model.Published,
                Id = model.Id,
                NTitle = model.Title,
                Text = model.Text,
                PageType = model.PageType,
            };

            return vPage;
        }

        public ActionResult ShowByType(string type)
        {
            var page = GetPageByType(type);
            return View("~/Views/Pages/Show.cshtml", ConvertToVPage(page));
        }

        public ActionResult ShowById(int id)
        {
            var page = _ctx.Pages.FirstOrDefault(p => p.Id == id);
            _breadcrumbs.Add(page.Title);
            ViewBag.BrowserTitle = page.Title;
            return View("~/Views/Pages/Show.cshtml", ConvertToVPage(page));
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Edit(int? id, string pageType)
        {
            var model = new VPage();
            if (id == null)
            {
                ViewBag.Title += " - Add";
                model.PageType = pageType;
            }
            else
            {
                ViewBag.Title += " - Edit";
                var page = _ctx.Pages.First(n => n.Id == id);
                model.NTitle = page.Title;
                //model.PageTitle = page.PageTitle;
                model.Slug = page.Slug;
                model.Announce = page.Announce;
                model.Text = page.Text;
                model.IsHot = page.HomePage;
                model.Published = page.Published;
                model.Id = page.Id;
                if (page.PageType == PageType.Contacts)
                {
                    model.ReturnUrl = "/contacts";
                }
                else if (page.PageType == PageType.News)
                {
                    model.ReturnUrl = "/news";
                }
                else
                {
                    model.ReturnUrl = "/";
                }
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        [Authorize(Roles = Role.Admin)]
        public ActionResult EditSave(VPage model, string returnPage)
        {
            var saveAndAdd = Request["SaveAndAdd"];
            //AuthorizeCurrentRole(SystemRoles.Admin);
            if (ModelState.IsValid)
            {
                SavePage(model);
                if (!String.IsNullOrWhiteSpace(saveAndAdd))
                {
                    return RedirectToAction("edit", new { id = "", pageType = model.PageType });
                }
                return RedirectToAction("ShowById", new { id = model.Id });
            }

            return View("Edit", model);
        }

        private void SavePage(VPage model)
        {
            Page page;
            var now = DateTime.UtcNow;
            if (model.Id == 0)
            {
                page = new Page();
                page.Created = now;
                page.PageType = model.PageType;
                _ctx.Pages.AddObject(page);
            }
            else
            {
                page = _ctx.Pages.First(t => t.Id == model.Id);
            }

            page.Title = model.NTitle ?? "";
            //page.PageTitle = model.PageTitle ?? "";
            page.Slug = model.Slug ?? "";
            page.Announce = model.Announce ?? "";
            page.Text = model.Text ?? "";
            page.HomePage = model.IsHot;
            page.Updated = now;
            page.Published = model.Published;
            _ctx.SaveChanges();

            model.Id = page.Id;
        }

        [HttpGet]
        [Authorize(Roles = Role.Admin)]
        public ActionResult Delete(int id)
        {
            ViewBag.Title += " - Delete";
            var page = _ctx.Pages.FirstOrDefault(n => n.Id == id);
            if (page == null)
                return Redirect("AccessDeniedException");
            var model = new VPageDelete();
            model.Created = page.Created;
            model.Title = page.Title;
            model.Announce = page.Announce;
            model.ReturnUrl = HttpContext.Request.UrlReferrer != null ? HttpContext.Request.UrlReferrer.PathAndQuery : "/";
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = Role.Admin)]
        public ActionResult DeleteSave(int id, int? returnPage)
        {
            var page = _ctx.Pages.FirstOrDefault(n => n.Id == id);

            if (page == null)
                return Redirect("AccessDeniedException");

            _ctx.Pages.DeleteObject(page);
            _ctx.SaveChanges();

            if (returnPage == null)
                returnPage = 0;

            return RedirectToAction("index", new { id = returnPage + 1 });
        }
    }
}
