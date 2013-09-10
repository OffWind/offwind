using System;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.WebApp.Models.Event;

namespace Offwind.WebApp.Controllers
{
    public class GlobalMeetingController : BaseController
    {
        public ActionResult Index()
        {
            return View(new VEventApplication());
        }

        [HttpPost]
        public ActionResult Index(VEventApplication model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var app = new DEventApplication();
            app.Id = Guid.NewGuid();
            app.Created = app.Updated = DateTime.UtcNow;
            app.FullName = model.FullName ?? "";
            app.Email = model.Email ?? "";
            app.Phone = model.Phone ?? "";
            app.Comment = model.Comment ?? "";
            app.Company = model.Company ?? "";
            _ctx.DEventApplications.AddObject(app);
            _ctx.SaveChanges();

            return View("RegisterComplete", model);
        }
    }
}
