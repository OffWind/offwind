using System.Linq;
using System.Web.Mvc;
using Offwind.WebApp.Areas.WindFarms.Models;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class HomeWindFarmsController : _BaseController
    {
        public ActionResult Index()
        {
            var m = new VWindFarmsHome
            {
                TotalPublicWindFarms = _ctx.DWindFarms.Count(x => x.IsPublic || x.Author == User.Identity.Name),
                TotalPublicTurbines = _ctx.DTurbines.Count(x => x.IsPublic || x.Author == User.Identity.Name)
            };
            return View(m);
        }
    }
}
