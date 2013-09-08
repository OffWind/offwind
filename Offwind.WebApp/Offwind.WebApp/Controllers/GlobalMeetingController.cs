using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Controllers
{
    public class GlobalMeetingController : BaseController
    {
        public ActionResult Index(string query)
        {
            return View();
        }
    }
}
