using System.Web.Mvc;
using EmitMapper;
using Offwind.Products.OpenFoam.Models;
using Offwind.Sowfa.System.ControlDict;
using Offwind.WebApp.Areas.CFD.Models.SystemControls;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class SystemControlsController : __BaseCfdController
    {
        public SystemControlsController()
        {
            SectionTitle = "System";
        }

        public ActionResult Time()
        {
            ShortTitle = "Time";
            var m = new VControlDict();
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<ControlDictData, VControlDict>().Map(sd.ControlDict, m);
            return View(m);
        }

        [ActionName("Time")]
        [HttpPost]
        public JsonResult TimeSave(VControlDict m)
        {
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<VControlDict, ControlDictData>().Map(m, sd.ControlDict);
            SetSolverData(sd);
            return Json("OK");
        }

        public ActionResult Schemes()
        {
            ShortTitle = "Schemes";
            return View();
        }

        public ActionResult Solution()
        {
            ShortTitle = "Solution";
            return View();
        }

        public ActionResult ParallelExecution()
        {
            ShortTitle = "Parallel Execution";
            return View();
        }
    }
}