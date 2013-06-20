using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Mailing;
using WebMatrix.WebData;

namespace Offwind.WebApp.Controllers
{
    public class MailingController : BaseController
    {
        public ActionResult Mailing()
        {
            var model = new MailingModel();
            var d = _ctx.DUserProfiles
                .Where(x => x.UserName != WebSecurity.CurrentUserName)
                .Select(x => x.UserName);

            model.Users.AddRange(d);            
            return View(model);
        }

        [HttpPost]
        [ActionName("Mailing")]
        public ActionResult Send(MailingModel model)
        {
            return RedirectToAction("Mailing");
        }
    }
}
