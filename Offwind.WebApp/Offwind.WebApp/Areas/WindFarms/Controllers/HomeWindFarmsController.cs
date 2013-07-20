using System.Linq;
using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.WindFarms.Models;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class HomeWindFarmsController : _BaseController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        public ActionResult Index()
        {
            var m = new VWindFarmsHome();
            m.TotalPublicWindFarms = _ctx.DWindFarms.Count();
            m.TotalPublicTurbines = _ctx.DTurbines.Count();
            return View(m);
        }
    }
}
