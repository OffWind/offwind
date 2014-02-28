using System;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;
using Offwind.Web.Core.Extensions;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class NewsController : ManagmentControllerBase
    {
        public ActionResult Create()
        {
            ViewModel.Title = string.Empty;
            ViewModel.Content = string.Empty;
            ViewModel.Announce = string.Empty;
            ViewModel.Id = -1;
            ViewModel.Header = "New page";
            ViewModel.Published = null;
            return View("~/Areas/Management/Views/News/Details.cshtml",ViewModel);
        }
        public ActionResult Update(int id,string header,string announce,string content)
        {
            var item = _ctx.Pages.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Title = header;
                item.Announce = announce;
                item.Text = content;
                _ctx.Pages.ApplyCurrentValues(item);
                _ctx.SaveChanges();
            }
            else
            {
                item = new Page
                {
                    Title = header,
                    Announce = announce,
                    Text = content,
                    PageType = PageTypes.News,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Published = DateTime.UtcNow,
                };
                _ctx.Pages.AddObject(item);
                _ctx.SaveChanges();
            }
            return RedirectToAction("News", "Management");
        }
        public ActionResult Delete()
        {
            var content = _ctx.Pages.Where(x => x.PageType == PageTypes.News).ToList();
            return RedirectToAction("News", "Management");
        }
        public ActionResult Details(int id)
        {
            var item = _ctx.Pages.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewModel.Title = item.Title;
                ViewModel.Content = item.Text;
                ViewModel.Announce = item.Announce;
                ViewModel.Id = item.Id;
                ViewModel.Header = "Edit page";
                ViewModel.Published = item.Published;
            }
            return View(ViewModel);
        }
    }
}
