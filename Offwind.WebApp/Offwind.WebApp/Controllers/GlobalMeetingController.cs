using System;
using System.Linq;
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

        [Authorize]
        public ActionResult Apply()
        {
            var user = _ctx.DVUserProfiles.First(x => x.UserName == User.Identity.Name);
            var app = new DEventParticipant();
            app.Id = Guid.NewGuid();
            app.EventId = Guid.Parse("44EC20AA-CE3A-4BD2-B48B-E8EBDA5D2E5B");
            app.UserId = user.UserId;
            app.Created = DateTime.UtcNow;
            _ctx.DEventParticipants.AddObject(app);
            _ctx.SaveChanges();

            return RedirectToAction("Profile", "Account");
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
