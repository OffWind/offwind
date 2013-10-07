using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EmitMapper;
using Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2;
using WakeFarmControl;
using WakeFarmControl.Input;


namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class WakeSimulation2Controller : _BaseController
    {
        private static VGeneralProperties _model = null;
        private static List<string> _wfl = null;
        static private double[][] _simulation;

        public ActionResult Index()
        {
            ViewBag.Title = "Input | Wake Simulation II | Offwind";
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
            ViewBag.Title = "Input | Wake Simulation II | Offwind";
            lock (_model)
            {
                model.WindFarm = _model.WindFarm;
                ObjectMapperManager.DefaultInstance.GetMapper<VGeneralProperties, VGeneralProperties>().Map(model, _model);
            }
            var dWindFarm = _ctx.DWindFarms.First(e => _model.WindFarm == e.Name);
            _model.NTurbines = dWindFarm.DWindFarmTurbines.Count();

            var input = new Simulation()
            {
                Tstart = (double)_model.StartTime,
                Tend = (double)_model.StopTime,
                DT = (double)_model.TimeStep,
                NTurbines = _model.NTurbines,
                RatedPower = 5,
                EnablePowerDistribution = true,
                EnableTurbineDynamics = true,
                PowerRefInterpolation = true,
                Pdemand = 3 * 5e6,
                PRefSampleTime = 5
            };

            input.LoadNREL5MW_MatFile(WebConfigurationManager.AppSettings["WakeFarmControlNREL5MW"]);
            input.LoadWind_MatFile(WebConfigurationManager.AppSettings["WakeFarmControlWind"]);

            _simulation = FarmControl2.Simulation(input);
            return RedirectToAction("Results");
            return View(_model);
        }

        public ActionResult Results()
        {
            ViewBag.Title = "Results | Wake Simulation II | Offwind";
            if (_simulation != null)
            {
                var res = _simulation.Select(x => new object[] { x }).ToArray();
                return View(res);
            }
            return View(new object[0]);
        }

        public ContentResult GetSimulationResults()
        {
            var serializer = new JavaScriptSerializer();

            // For simplicity just use Int32's max value.
            // You could always read the value from the config section mentioned above.
            serializer.MaxJsonLength = Int32.MaxValue;

            var res = _simulation.Select(x => new object[] { x }).ToArray();
            var result = new ContentResult
            {
                Content = serializer.Serialize(res),
                ContentType = "application/json"
            };
            return result;

            //if (_simulation != null)
            //{
            //    var res = _simulation.Select(x => new object[] { x }).ToArray();
            //    return Json(res, JsonRequestBehavior.AllowGet);
            //}
            //return Json(null, JsonRequestBehavior.AllowGet);
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

