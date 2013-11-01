using System;
using System.Linq;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.WebApp.Models.Event;

namespace Offwind.WebApp.Controllers
{
    public class EventsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
