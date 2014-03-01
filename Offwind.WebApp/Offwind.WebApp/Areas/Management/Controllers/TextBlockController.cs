using System;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core.Data;
using Offwind.Web.Core.Extensions;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class TextBlockController : ManagmentControllerBase
    {
        public ActionResult Details(Guid id)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                ViewModel.Header = item.Title;
                ViewModel.Content = item.Content;
                ViewModel.Id = item.Id;
                ViewModel.Updated = item.Updated;
            }

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Guid id, string header, string content)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id && x.TypeId == ContentTypes.Block);
            if (item != null)
            {
                item.Title = header;
                item.Content = content;
                item.Updated =DateTime.UtcNow;
                _ctx.DContents.ApplyCurrentValues(item);
                _ctx.SaveChanges();
                if (this.GetAllActionNames().Contains(item.DContentCategory.Name))
                    return RedirectToAction(item.DContentCategory.Name);
            }
            return RedirectToAction("Home","Management");
        }
    }
}