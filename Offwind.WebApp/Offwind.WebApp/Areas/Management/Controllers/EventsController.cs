using System.Linq;
using System.Web.Mvc;
using Offwind.WebApp.Areas.Management.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class EventsController : _BaseController
    {
        public ActionResult Index()
        {
            var model = new VEventsHome();
            model.Participants = _ctx.DVEventParticipants.OrderBy(x => x.FirstName).ToList();
            return View(model);
        }
    }
}
