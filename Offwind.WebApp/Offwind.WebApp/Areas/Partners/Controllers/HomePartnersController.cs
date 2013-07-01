using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.WebApp.Areas.Partners.Models;

namespace Offwind.WebApp.Areas.Partners.Controllers
{
    public class HomePartnersController : _BaseController
    {
        public ActionResult Index()
        {
            var m = new VPartnersHome();
            var partners = _ctx.VPartners.Select(
                vp =>
                new VPartner
                    {Id = vp.UserId, CompanyName = vp.CompanyName, FullName = vp.FullName, UserName = vp.UserName});
            m.Partners.AddRange(partners);
            return View(m);
        }
    }
}
