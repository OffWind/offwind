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
    public class WakeSimulation2RController : _BaseController
    {
        private static VGeneralProperties _model = null;
        private static List<string> _wfl = null;
        static private double[][] _simulation;
        const string SimulationPageTitle = "Input | Wake Simulation II-R | Offwind";
        public ActionResult Simulation()
        {
            ViewBag.Title = SimulationPageTitle;
            if (_model == null)
            {
                _model = new VGeneralProperties();
                _model.TimeStep = (decimal)(0.1);
                _wfl = new List<string>();
                _simulation = null;
            }
            var model = new VGeneralProperties();
            ObjectMapperManager.DefaultInstance.GetMapper<VGeneralProperties, VGeneralProperties>().Map(_model, model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Simulation(VGeneralProperties model)
        {
            ViewBag.Title = SimulationPageTitle;
            lock (_model)
            {
                model.WindFarm = _model.WindFarm;
                ObjectMapperManager.DefaultInstance.GetMapper<VGeneralProperties, VGeneralProperties>().Map(model, _model);
            }
            var dWindFarm = _ctx.DWindFarms.First(e => _model.WindFarm == e.Name);
            var turbinesCoordinatesList = dWindFarm.DWindFarmTurbines.OrderBy(t => t.Number).Select(t => new double[] { (double)(t.X), (double)(t.Y) }).ToList();
            var turbinesCoordinates = new double[turbinesCoordinatesList.Count, 2];
            for (var index = 0; index < turbinesCoordinates.GetLength(0); index++ )
            {
                turbinesCoordinates[index, 0] = turbinesCoordinatesList[index][0];
                turbinesCoordinates[index, 1] = turbinesCoordinatesList[index][1];
            }
            _model.Turbines = turbinesCoordinates;

            var config = new WakeFarmControlR.WakeFarmControlConfig()
            {
                Turbines = _model.Turbines,
            };
            config.enablePowerDistribution = _model.EnablePowerDistribution;
            config.enableTurbineDynamics = _model.EnableTurbineDynamics;
            config.powerRefInterpolation = _model.PowerRefInterpolation;
            config.enableVaryingDemand = _model.EnableVaryingDemand;

            config.SimParm.timeStep = (double)_model.TimeStep;
            config.SimParm.tEnd = (double)_model.StopTime;
            config.SimParm.ctrlUpdate = (double)_model.ControlUpdateInterval;
            config.SimParm.powerUpdate = (double)_model.PowerUpdateInterval;
            config.InitialPowerDemand = (double)_model.InitialPowerDemand;

            config.NREL5MW_MatFile = WebConfigurationManager.AppSettings["WakeFarmControlRNREL5MW"]; // @"c:\farmcontrol\NREL5MW_Runc.mat";
            config.Wind_MatFile = WebConfigurationManager.AppSettings["WakeFarmControlRWind"]; // @"c:\farmcontrol\wind_Runc.mat";

            _simulation = WakeFarmControlR.FarmControl.Simulation(config);
            return RedirectToAction("Results");
        }

        public ActionResult Results()
        {
            ViewBag.Title = SimulationPageTitle;
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

            //var res = _simulation.Select(x => new object[] { x }).ToArray();

            var turbines = new object[] { };
            if (_model != null)
            {
                string modelWindFarm;
                lock (_model)
                {
                    modelWindFarm = _model.WindFarm;
                }
                var dWindFarm = _ctx.DWindFarms.First(wf => wf.Name == modelWindFarm);
                turbines = dWindFarm.DWindFarmTurbines.OrderBy(t => t.Number).Select(t => new { n = t.Number, x = t.X, y = t.Y }).ToArray();
            }
            var res = new { data = _simulation.Select(x => new object[] { x }).ToArray(), turbines = turbines };
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
            _wfl.AddRange(_ctx.DWindFarms.Where(entry => entry.DWindFarmTurbines.Count() >= 1).Select(entry => entry.Name));
            bool indexAdded = false;
            if (_model.WindFarm != "")
            {
                var index = 0;
                foreach (var v in _wfl)
                {
                    if (v == _model.WindFarm)
                    {
                        _wfl.Add(index.ToString(CultureInfo.InvariantCulture));
                        indexAdded = true;
                        break;
                    }
                    index++;
                }
            }
            if (!indexAdded)
            {
                _wfl.Add(((int)0).ToString(CultureInfo.InvariantCulture));
            }
            return Json(_wfl, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult WindFarmSelected(int id)
        {
            lock (_model)
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

