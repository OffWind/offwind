using System;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class CarouselController : ManagmentControllerBase
    {
        public ActionResult Create()
        {
            ViewModel.Title = string.Empty;
            ViewModel.Content = string.Empty;
            ViewModel.Id = Guid.Empty;
            ViewModel.Header = "New item";
            ViewModel.Updated = null;
            ViewModel.BrowserTitle = "New carousel item";
            ViewModel.Url = string.Empty;
            return View("~/Areas/Management/Views/Carousel/Details.cshtml", ViewModel);
        }
        public ActionResult Details(Guid id)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewModel.Header = "Edit item";
                ViewModel.Title = item.Title;
                ViewModel.Content = item.Content;
                ViewModel.Id = item.Id;
                ViewModel.Url = item.Announce;
                ViewModel.BrowserTitle = "Edit carousel item";
                ViewModel.Updated = item.Updated;
            }

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Guid id, string header, string content,string url,string name)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Title = header;
                item.Content = content;
                item.Announce = url;
                item.BrowserTitle = name;
                item.Updated = DateTime.UtcNow;
                _ctx.DContents.ApplyCurrentValues(item);
                _ctx.SaveChanges();
            }
            else
            {
                item = _ctx.CreateObject<DContent>();
                item.CategoryId = Categories.Home;
                item.Id = Guid.NewGuid();
                item.Title = header;
                item.Content = content;
                item.TypeId = ContentTypes.Carousel;
                item.Route = string.Empty;
                item.Position = 0;
                item.BrowserTitle = name;
                item.Announce = url;
                item.IsPublished = true;
                item.Name = string.Empty;
                item.MetaDescription = string.Empty;
                item.MetaKeywords = string.Empty;
                item.Updated = DateTime.UtcNow;
                item.Created = DateTime.UtcNow;
                item.PublishDate = DateTime.UtcNow;
                item.ExpirationDate = DateTime.UtcNow;
                _ctx.DContents.AddObject(item);
                _ctx.SaveChanges();
            }
            return RedirectToAction("Home", "Management");
        }
    }
}