using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Areas.Partners.Controllers
{
    public class HomePartnersController : _BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Partners area | Offwind";
            return View();
        }
    }
}
