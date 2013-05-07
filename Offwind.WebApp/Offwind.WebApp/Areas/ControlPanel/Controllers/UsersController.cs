using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Offwind.WebApp.Areas.ControlPanel.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /ControlPanel/Users/

        public ActionResult Index()
        {
            return View();
        }

    }
}
