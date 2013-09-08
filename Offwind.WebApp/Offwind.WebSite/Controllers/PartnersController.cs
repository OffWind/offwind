using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;

namespace Offwind.Web.Controllers
{
    [Authorize(Roles = "Admin, Partner")]
    public class PartnersController : PagesController
    {
        public ActionResult Index()
        {
            return ShowByType(PageType.Partners);
        }
    }
}
