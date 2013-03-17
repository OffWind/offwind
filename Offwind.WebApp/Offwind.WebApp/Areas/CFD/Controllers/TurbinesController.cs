using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EmitMapper;
using Offwind.OpenFoam.Sintef;
using Offwind.Sowfa.Constant.TurbineArrayProperties;
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
            ObjectMapperManager.DefaultInstance.GetMapper<TurbinePropertiesData, VTurbineType>().Map(sd.TurbineProperties, m);
            return View(m);
        }

        public JsonResult VGetBladeData(int id)
        {
            var sd = GetSolverData();
            var res = sd.TurbineProperties.airfoilBlade[id].Blade.Select(t => new object[]
                                                                                  {
                                                                                      t.X,
                                                                                      t.Y,
                                                                                      t.Z
                                                                                  });
            return Json(res, JsonRequestBehavior.AllowGet);
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

        public ActionResult TurbineArray()
        {
            ShortTitle = "Turbine Array";
            var m = new VTurbineArray();
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<TurbineArrayPropData, VTurbineArray>().Map(sd.TurbineArrayProperties, m);
            return View(m);
        }

        public JsonResult TurbineArrayData()
        {
            var sd = GetSolverData();
            var arr = sd
                .TurbineArrayProperties
                .turbine
                .Select(t => new object[]
                {
                    t.turbineType,
                    t.baseLocation.X,
                    t.baseLocation.Y,
                    t.baseLocation.Z,
                    t.numBladePoints,
                    t.pointDistType,
                    t.pointInterpType,
                    t.bladeUpdateType,
                    t.epsilon,
                    t.tipRootLossCorrType,
                    t.rotationDir,
                    t.azimuth,
                    t.rotSpeed,
                    t.pitch,
                    t.nacYaw,
                    t.fluidDensity,
                });
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TurbineArrayDataSave(List<string[]> turbines)
        {
            if (turbines == null) return Json("Bad model");
            //var model = GetModelTurbines();
            //turbines.RemoveAt(turbines.Count - 1);
            //model.Turbines.Clear();
            //model.Turbines.AddRange(turbines.Select(t => new VTurbine(t[0], t[1])));
            return Json("OK");
        }
    }
}