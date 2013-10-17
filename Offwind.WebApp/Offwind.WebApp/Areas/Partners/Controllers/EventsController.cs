using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Offwind.WebApp.Areas.Partners.Models.Events;

namespace Offwind.WebApp.Areas.Partners.Controllers
{
    public class EventsController : _BaseController
    {
        public ActionResult Index()
        {
            var js = new JavaScriptSerializer();
            var list = new List<Event>();
            foreach (var devent in _ctx.DEvents)
            {
                var ev = js.Deserialize<Event>(devent.Data);
                list.Add(ev);
            }
            return View(list);
        }
    }
}
