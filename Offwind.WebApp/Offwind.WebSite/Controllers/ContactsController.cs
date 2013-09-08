using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Offwind.Web.Core;

namespace Offwind.Web.Controllers
{
    public class ContactsController : PagesController
    {
        public ActionResult Edit()
        {
            return RedirectToAction("Edit", "Pages", new { type = PageType.Contacts });
        }

        public ActionResult Index()
        {
            return ShowByType(PageType.Contacts);
        }
    }
}
