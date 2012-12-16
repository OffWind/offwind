using System.Web.Mvc;
using EmitMapper;
using Offwind.OpenFoam.Sintef;
using Offwind.Sowfa.Constant.TurbineProperties;
using Offwind.WebApp.Areas.CFD.Models.Turbines;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class TurbinesController : __BaseCfdController
    {
        public TurbinesController()
        {
            SectionTitle = "Turbines";
        }

        public ActionResult TurbineTypes()
        {
            ShortTitle = "Turbine Types";
            var m = new VTurbineType();
            var sd = GetSolverData();
            //var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TurbinePropertiesData, VTurbineType>().Map(sd.TurbineProperties, m);
            return View(m);
        }

        [ActionName("TurbineTypes")]
        [HttpPost]
        public ActionResult TurbineTypesSave(VTurbineType m)
        {
            ShortTitle = "Turbine Types";
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<VTurbineType, TurbinePropertiesData>().Map(m, sd.TurbineProperties);
            SetSolverData(sd);
            if (Request.IsAjaxRequest()) return Json("OK");
            return View(m);
        }

        public ActionResult TurbineArray(VTurbineType m)
        {
            ShortTitle = "Turbine Array";
            return View();
        }
    }
}