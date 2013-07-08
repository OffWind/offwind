using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.WindFarms.Models;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.WindFarms.Controllers
{
    public class HomeWindFarmsController : _BaseController
    {
        //
        // GET: /WindFarms/WindFarms/

        public ActionResult Index()
        {
            var m = new VWindFarmsHome();
            var wfMapper = ObjectMapperManager.DefaultInstance.GetMapper<DWindFarm, VWindFarm>();

            foreach (var dWindFarm in _ctx.DWindFarms)
            {
                m.WindFarms.Add(wfMapper.Map(dWindFarm));
            }
            return View(m);
        }

    }
}
