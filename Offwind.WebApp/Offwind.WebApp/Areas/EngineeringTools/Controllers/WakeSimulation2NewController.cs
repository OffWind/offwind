using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EmitMapper;
using Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation2New;
using WakeFarmControl;
using WakeFarmControl.Input;


namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class WakeSimulation2NewController : _BaseController
    {
        const string SimulationPageTitle = "Input | Wake Simulation II-New | Offwind";
        const string ResultsPageTitle = "Results | Wake Simulation II-New | Offwind";
        const string NowcastingPageTitle = "Nowcasting | Wake Simulation II-New | Offwind";

        private VGeneralProperties _model
        {
            get
            {
                return (VGeneralProperties)(Session["WindFarmControlNew.GeneralProperties"]);
            }
            set
            {
                Session["WindFarmControlNew.GeneralProperties"] = value;
            }
        }

        private List<string> _wfl
        {
            get
            {
                var wfl = (List<string>)(Session["WindFarmControlNew.WindFarmsList"]);
                if (wfl == null)
                {
                    wfl = new List<string>();
                    Session["WindFarmControlNew.WindFarmsList"] = wfl;
                }
                return wfl;
            }
            set
            {
                Session["WindFarmControlNew.WindFarmsList"] = value;
            }
        }

        private decimal _simulationTimeStep
        {
            get
            {
                return (decimal)(Session["WindFarmControlNew.SimulationTimeStep"] ?? ((decimal)0));
            }
            set
            {
                Session["WindFarmControlNew.SimulationTimeStep"] = value;
            }
        }

        private double[][] _simulation
        {
            get
            {
                return (double[][])(Session["WindFarmControlNew.Simulation"]);
            }
            set
            {
                Session["WindFarmControlNew.Simulation"] = value;
            }
        }

        private double[][] _simulationDataOut
        {
            get
            {
                return (double[][])(Session["WindFarmControlNew.SimulationDataOut"]);
            }
            set
            {
                Session["WindFarmControlNew.SimulationDataOut"] = value;
            }
        }

        private List<string> _simulationInformationMessages
        {
            get
            {
                return (List<string>)(Session["WindFarmControlNew.SimulationInformationMessages"]);
            }
            set
            {
                Session["WindFarmControlNew.SimulationInformationMessages"] = value;
            }
        }

        private VNowcastingProperties _nowcastingModel
        {
            get
            {
                return (VNowcastingProperties)(Session["WindFarmControlNew.NowcastingModel"]);
            }
            set
            {
                Session["WindFarmControlNew.NowcastingModel"] = value;
            }
        }

        private WakeFarmControl.NowCast.NowCastSimulationResult _nowcastingSimulationResult
        {
            get
            {
                return (WakeFarmControl.NowCast.NowCastSimulationResult)(Session["WindFarmControlNew.NowcastingSimulationResult"]);
            }
            set
            {
                Session["WindFarmControlNew.NowcastingSimulationResult"] = value;
            }
        }

        private List<string> _nowcastingSimulationWarningMessages
        {
            get
            {
                return (List<string>)(Session["WindFarmControlNew.NowcastingSimulationWarningMessages"]);
            }
            set
            {
                Session["WindFarmControlNew.NowcastingSimulationWarningMessages"] = value;
            }
        }

        public ActionResult Simulation()
        {
            ViewBag.Title = SimulationPageTitle;
            if (_model == null)
            {
                _model = new VGeneralProperties();
                _model.TimeStep = (decimal)(0.1);
                //_wfl = new List<string>();
                _simulation = null;
                _simulationDataOut = null;
                _nowcastingSimulationResult = null;
                _nowcastingSimulationWarningMessages = null;
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
            //_model.Turbines = turbinesCoordinates;

            var config = new WakeFarmControlR.WakeFarmControlConfig()
            {
                //Turbines = _model.Turbines,
                Turbines = turbinesCoordinates,
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

            double[][] simulationDataOut;
            List<string> simulationInformationMessages;
            _simulation = WakeFarmControlR.FarmControl.Simulation(config, out simulationDataOut, out simulationInformationMessages);
            _simulationTimeStep = _model.TimeStep;
            _simulationDataOut = simulationDataOut;
            _simulationInformationMessages = simulationInformationMessages;
            return RedirectToAction("Results");
        }

        public ActionResult Results()
        {
            ViewBag.Title = ResultsPageTitle;
            ViewBag.WasWakeSimulationPerformed = !(_simulationDataOut == null);
            if (_simulation != null)
            {
                var res = new {
                                informationMessages = _simulationInformationMessages,
                                data = _simulation.Select(x => new object[] { x }).ToArray()
                };
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
            var res = new {
                            informationMessages = _simulationInformationMessages,
                            turbines = turbines,
                            data = (_simulation ?? new double[0][]).Select(x => new object[] { x }).ToArray()
            };
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

        public ActionResult Nowcasting()
        {
            ViewBag.Title = NowcastingPageTitle;
            ViewBag.WasWakeSimulationPerformed = !(_simulationDataOut == null);

            if (_nowcastingModel == null)
            {
                _nowcastingModel = new VNowcastingProperties();
                var config = new WakeFarmControl.NowCast.NowCastConfig();
                _nowcastingModel.TimeForStarting = (decimal)(config.TPredict);
                _nowcastingModel.Decimation = config.r;
                _nowcastingModel.SamplingTime = (decimal)(config.Ts);
            }
            _nowcastingModel.SamplingTime = _simulationTimeStep;
            //_nowcastingModel.WasWakeSimulationPerformed = !(_simulationDataOut == null);
            return View(_nowcastingModel);
        }

        [HttpPost]
        public ActionResult Nowcasting(VNowcastingProperties nowcastingModel)
        {
            if (_simulationDataOut == null)
            {
                return RedirectToAction("Simulation");
            }
            lock (_nowcastingModel)
            {
                ObjectMapperManager.DefaultInstance.GetMapper<VNowcastingProperties, VNowcastingProperties>().Map(nowcastingModel, _nowcastingModel);
                _nowcastingModel.SamplingTime = _simulationTimeStep;
            }

            var config = new WakeFarmControl.NowCast.NowCastConfig();
            config.Method = _nowcastingModel.Method.ToString();
            config.TPredict = (double)(_nowcastingModel.TimeForStarting);
            config.r = _nowcastingModel.Decimation;
            config.Ts = (double)(_simulationTimeStep);

            List<string> nowcastingSimulationWarningMessages;
            _nowcastingSimulationResult = WakeFarmControl.NowCast.NowCast.Simulation(_simulationDataOut, config, out nowcastingSimulationWarningMessages);
            _nowcastingSimulationWarningMessages = nowcastingSimulationWarningMessages;

            return RedirectToAction("Nowcasting");
        }

        private double?[] RemoveNaNs(double[] array)
        {
            if (array == null)
            {
                return null;
            }
            var arrayWithoutNaNs = new double?[array.GetLength(0)];
            for (var i = 0; i < arrayWithoutNaNs.GetLength(0); i++)
            {
                arrayWithoutNaNs[i] = (double.IsNaN(array[i]) ? (double?)null : array[i]);
            }

            return arrayWithoutNaNs;
        }

        private double?[][] RemoveNaNs(double[][] array)
        {
            if (array == null)
            {
                return null;
            }
            var arrayWithoutNaNs = new double?[array.GetLength(0)][];
            for (var i = 0; i < arrayWithoutNaNs.GetLength(0); i++)
            {
                if (array[i] == null)
                {
                    arrayWithoutNaNs[i] = null;
                    continue;
                }
                arrayWithoutNaNs[i] = new double?[array[i].GetLength(0)];
                for (var j = 0; j < arrayWithoutNaNs[i].GetLength(0); j++)
                {
                    arrayWithoutNaNs[i][j] = (double.IsNaN(array[i][j]) ? (double?)null : array[i][j]);
                }
            }

            return arrayWithoutNaNs;
        }

        public ContentResult GetNowcastingSimulationResults()
        {
            var serializer = new JavaScriptSerializer();

            // For simplicity just use Int32's max value.
            // You could always read the value from the config section mentioned above.
            serializer.MaxJsonLength = Int32.MaxValue;

            var res = new { data =
                                (_nowcastingSimulationResult == null ? null :
                                    new {
                                            Method = _nowcastingSimulationResult.Method,
                                            Time = RemoveNaNs(_nowcastingSimulationResult.Time),
                                            X = RemoveNaNs(_nowcastingSimulationResult.X),
                                            XhmsAll = RemoveNaNs(_nowcastingSimulationResult.XhmsAll),//.Select(x => new object[] { x }).ToArray(),
                                            XhmsAllTimeOffset = _nowcastingSimulationResult.XhmsAllTimeOffset,
                                            XhmsLLength = _nowcastingSimulationResult.XhmsLLength,
                                            XhmsUOffset = _nowcastingSimulationResult.XhmsUOffset
                                        }
                                ),
                            warningMessages = _nowcastingSimulationWarningMessages
                            };
            var result = new ContentResult
            {
                Content = serializer.Serialize(res),
                ContentType = "application/json"
            };
            return result;
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
            return RedirectToAction("Details", "WindFarm", new { area = "WindFarms", id = dWindFarm.Id, returnTo = "WakeSimulation2New" });
        }
    }
}

