using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2;
using WakeFarmControl;


namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class WakeSimulation2Controller : _BaseController
    {
        private static VGeneralProperties _model = null;
        private static List<string> _wfl = null;
        static private double[][] _simulation;

        public ActionResult Index()
        {
            if (_model == null)
            {
                _model = new VGeneralProperties();
                _wfl = new List<string>();
                _simulation = null;
            }
            var model = new VGeneralProperties();
            ObjectMapperManager.DefaultInstance.GetMapper<VGeneralProperties, VGeneralProperties>().Map(_model, model);
            return View(model);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult Save(VGeneralProperties model)
        {
            lock(_model)
            {
                model.WindFarm = _model.WindFarm;
                ObjectMapperManager.DefaultInstance.GetMapper<VGeneralProperties, VGeneralProperties>().Map(model, _model);
            }
            var dWindFarm = _ctx.DWindFarms.First(e => _model.WindFarm == e.Name);
            _model.NTurbines = dWindFarm.DWindFarmTurbines.Count();
            _simulation = FarmControl.Simulation(new WakeFarmControlConfig()
                                                     {
                                                         Tstart = (double) _model.StartTime,
                                                         Tend = (double) _model.StopTime,
                                                         DT = _model.TimeStep,
                                                         //NTurbines = _model.NTurbines,
                                                         NTurbines = 4,
                                                         NREL5MW_MatFile =
                                                             WebConfigurationManager.AppSettings[
                                                                 "WakeFarmControlNREL5MW"],
                                                         Wind_MatFile =
                                                             WebConfigurationManager.AppSettings[
                                                                 "WakeFarmControlWind"],
                                                         EnablePowerDistribution = true,
                                                         EnableTurbineDynamics = true,
                                                         PowerRefInterpolation = true,
                                                         Pdemand = 3*5e6,
                                                         PRefSampleTime = 5
                                                     });
            return View(_model);
        }

        public JsonResult GetSimulationResults()
        {
            if (_simulation != null)
            {
                var res = _simulation.Select(x => new object[] {x}).ToArray();
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAvailWinFarms()
        {
            _wfl.Clear();
            _wfl.AddRange(_ctx.DWindFarms.Select(entry => entry.Name));
            if (_model.WindFarm != "")
            {
                var index = 0;
                foreach (var v in _wfl)
                {
                    if (v == _model.WindFarm)
                    {
                        _wfl.Add(index.ToString(CultureInfo.InvariantCulture));
                        break;
                    }
                    index++;
                }
            }
            return Json(_wfl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult WindFarmSelected(int id)
        {
            lock(_model)
            {
                _model.WindFarm = _wfl[id];
            }
            return Json("OK");
        }

        public ActionResult WindFarmInfo(Guid? id)
        {
            var dWindFarm = _ctx.DWindFarms.First(e => _model.WindFarm == e.Name);
            return RedirectToAction("Details", "WindFarm", new { area = "WindFarms", id = dWindFarm.Id, returnTo = "WakeSimulator2" });
        }
    }
}

