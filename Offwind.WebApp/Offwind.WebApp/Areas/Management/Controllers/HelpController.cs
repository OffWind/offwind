using System;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using MongoDB.Bson.Serialization.Serializers;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class HelpController : ManagmentControllerBase
    {
        public ActionResult Details()
        {
            var item = _ctx.Pages.FirstOrDefault(x => x.PageType==PageTypes.Help);
            if (item != null)
            {
                ViewModel.Content = item.Text;
                ViewModel.Id = item.Id;
                ViewModel.Updated = item.Updated;
            }
            else
            {
                ViewModel.Content = string.Empty;
                ViewModel.Id = -1;
                ViewModel.Updated = null;
            }
            ViewModel.BrowserTitle = "Edit help page";
            return View("~/Areas/Management/Views/Management/Help.cshtml",ViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, string content)
        {
            var item = _ctx.Pages.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Text = content;
                _ctx.Pages.ApplyCurrentValues(item);
                _ctx.SaveChanges();
            }
            else
            {
               var page = new Page
                {
                    Text = content,
                    PageType = "Help",
                    Created = DateTime.UtcNow,
                    Published = DateTime.UtcNow,
                    Updated = DateTime.UtcNow
                };
                _ctx.Pages.AddObject(page);
                _ctx.SaveChanges();
            }
            return RedirectToAction("Help", "Management");
        }

    }
}
