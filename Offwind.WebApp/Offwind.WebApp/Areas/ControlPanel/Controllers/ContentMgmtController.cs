using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Offwind.Web.Core;
using Offwind.WebApp.App_Start;
using Offwind.WebApp.Areas.ControlPanel.Models;
using Offwind.WebApp.Areas.ControlPanel.Tools;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.ControlPanel.Controllers
{
    public class ContentMgmtController : _BaseCmController
    {
        private Dictionary<ContentType, string> _titles = new Dictionary<ContentType,string>();

        public ContentMgmtController()
        {
            _titles[ContentType.Undefined] = "Content";
            _titles[ContentType.Page] = "Pages";
            _titles[ContentType.Block] = "Blocks";
            _titles[ContentType.Blog] = "Blogs";
        }


        public ActionResult Index(string type)
        {
            var dPages = _ctx.DContents.AsQueryable();

            var cType = type.S();
            if (cType != ContentType.Undefined)
            {
                dPages = dPages.Where(p => p.TypeId == type);
            }
            var pages = dPages.OrderBy(p => p.Route).ThenByDescending(p => p.DisplayDateTime).ToList();

            ViewBag.ContentType = type;
            ViewBag.HTitle = _titles[cType];
            return View(pages);
        }

        [DisplayName("View")]
        public ViewResult Show(Guid id, string type)
        {
            ViewBag.ContentType = type ?? "";
            var page = _ctx.DContents.Single(p => p.Id == id);
            return View(page);
        }

        [DisplayName("Edit")]
        public ActionResult Edit(Guid? id, string type)
        {
            var model = new ContentModel();
            model.ContentType = type.S();
            if (id == null)
            {
                if (type == null) throw new ApplicationException("Content type must be specified");
                ViewBag.Title += " - New";
            }
            else
            {
                ViewBag.Title += " - Edit";
                var page = _ctx.DContents.First(n => n.Id == id);
                model.Name = page.Name;
                model.NTitle = page.Title;
                model.Route = page.Route;
                model.Announce = page.Announce;
                model.Content = page.Content;
                model.BrowserTitle = page.BrowserTitle;
                model.DisplayDateTime = page.DisplayDateTime.ToString("yyyy-MM-dd");
            }

            return View("Edit", model);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult EditSave(ContentModel model, string type, int? returnPage)
        {
            if (ModelState.IsValid)
            {
                SaveContent(model);
                if (model.ContentType == ContentType.Page)
                {
                    RouteTable.Routes.Clear();
                    AreaRegistration.RegisterAllAreas();
                    RouteConfig.RegisterRoutes(RouteTable.Routes);
                }
                return RedirectToAction("Index", new { type = model.ContentType.S() });
            }

            return View("Edit", model);
        }

        [DisplayName("Delete")]
        public ActionResult Delete(Guid id, string type)
        {
            ViewBag.ContentType = type ?? "";
            var page = _ctx.DContents.Single(p => p.Id == id);
            return View(page);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmed(Guid id, string type)
        {
            var page = _ctx.DContents.Single(p => p.Id == id);
            _ctx.DContents.DeleteObject(page);
            _ctx.SaveChanges();
            return RedirectToAction("Index", new { type });
        }

        private void SaveContent(ContentModel model)
        {
            DContent page;
            var now = DateTime.UtcNow;
            if (model.Id == Guid.Empty)
            {
                page = new DContent();
                page.Id = Guid.NewGuid();
                page.Route = "";
                page.TypeId = model.ContentType.S();
                page.Created = now;
                _ctx.DContents.AddObject(page);
            }
            else
            {
                page = _ctx.DContents.First(t => t.Id == model.Id);
            }

            page.Name = model.Name ?? "";
            page.Title = model.NTitle ?? "";
            page.Announce = model.Announce ?? "";
            page.Content = model.Content ?? "";
            page.BrowserTitle = model.BrowserTitle ?? "";
            page.Route = model.Route ?? "";

            try
            {
                page.DisplayDateTime = DateTime.ParseExact(model.DisplayDateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                page.DisplayDateTime = now;
            }

            page.Updated = now;

            _ctx.SaveChanges();
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
            base.Dispose(disposing);
        }
    }
}
                    