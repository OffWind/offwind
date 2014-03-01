using System;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class AboutController : ManagmentControllerBase
    {
        public ActionResult Details()
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.DContentCategory.Name == CategoryNames.About);
            if (item != null)
            {
                ViewModel.Content = item.Content;
                ViewModel.Title = item.Title;
                ViewModel.Id = item.Id;
                ViewModel.Header = "Edit page";
                ViewModel.Published = item.PublishDate;
            }
            else
            {
                ViewModel.Content = string.Empty;
                ViewModel.Title = string.Empty;
                ViewModel.Id = -1;
                ViewModel.Header = "New page";
                ViewModel.Published = null;
            }
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Guid id, string content, string title)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Content = content;
                _ctx.DContents.ApplyCurrentValues(item);
                _ctx.SaveChanges();
            }
            else
            {
                var page = new DContent
                {
                    Content = content,
                    Title = title,
                    CategoryId = Categories.About,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                };
                _ctx.DContents.AddObject(page);
                _ctx.SaveChanges();
            }
            return RedirectToAction("About", "Management");
        }
    }
}