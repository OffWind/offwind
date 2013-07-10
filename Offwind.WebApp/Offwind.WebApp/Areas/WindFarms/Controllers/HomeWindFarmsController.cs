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
            m.TotalPublicWindFarms = _ctx.DWindFarms.Count(f => f.IsPublic);
            m.TotalPublicTurbines = _ctx.DTurbines.Count(f => f.IsPublic);
            return View(m);
        }

        [ActionName("wind-farms")]
        public ActionResult WindFarms()
        {
            var m = new VWindFarmsHome();
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<DWindFarm, VWindFarm>();

            foreach (var db in _ctx.DWindFarms)
            {
                m.WindFarms.Add(mapper.Map(db));
            }
            return View("WindFarms", m);
        }

        [ActionName("turbines")]
        public ActionResult Turbines()
        {
            var m = new VWindFarmsHome();
            foreach (var db in _ctx.DTurbines)
            {
                m.Turbines.Add(VTurbine.MapFromDb(db));
            }
            return View("Turbines", m);
        }
    }
}
