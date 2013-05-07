using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.WebApp.Areas.ControlPanel.Tools;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.ControlPanel.Controllers
{
    public class HomeMgmtController : Controller
    {
        //
        // GET: /ControlPanel/Home/

        public ActionResult Index()
        {
            return RedirectToAction("Index", "ContentMgmt", new { area = "ControlPanel", type = ContentType.Page.S() });
        }

    }
}
