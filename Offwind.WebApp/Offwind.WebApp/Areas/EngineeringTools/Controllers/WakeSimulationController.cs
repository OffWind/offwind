using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using Offwind.WebApp.Areas.EngineeringTools.Models.WakeSimulation;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Models.Account;
using WakeCode;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    [Authorize(Roles = SystemRole.RegularUser)]
    public class WakeSimulationController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GeneralProperties()
        {
            ViewBag.Title = "General Properties | WakeSim | Offwind";
            var model = GetModelGeneral();
            return View(model);
        }

        [ActionName("GeneralProperties")]
        [HttpPost]
        public ActionResult GeneralPropertiesSave(VGeneralProperties model)
        {
            if (ModelState.IsValid)
            {
                Session["GeneralProperties"] = model;
                if (Request.IsAjaxRequest()) return Json("OK");
                return View(model);
            }
            if (Request.IsAjaxRequest()) return Json("FAIL");
            return View(model);
        }

        public ActionResult TurbineProperties()
        {
            ViewBag.Title = "Turbine Properties | WakeSim | Offwind";
            return View();
        }

        public JsonResult TurbinePropertiesData()
        {
            var model = GetModelTurbines();
            var arr = model.Turbines.Select(t => new[] {t.X, t.Y}).ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TurbinePropertiesSave(List<decimal[]> turbines)
        {
            if (turbines == null) return Json("Bad model");
            var model = GetModelTurbines();
            turbines.RemoveAt(turbines.Count - 1);
            model.Turbines.Clear();
            model.Turbines.AddRange(turbines.Select(t => new VTurbine(t[0], t[1])));
            return Json("OK");
        }

        public ActionResult Simulation()
        {
            ViewBag.Title = "Simulation | WakeSim | Offwind";
            return View();
        }

        [HttpPost]
        public JsonResult Run()
        {
            var randomDir = Guid.NewGuid().ToString();
            Session["WakeSimDir"] = randomDir;
            var modelGeneral = GetModelGeneral();
            var modelTurbines = GetModelTurbines();
            string dir = WebConfigurationManager.AppSettings["WakeSimulationDir"];
            dir = Path.Combine(dir, randomDir); // root temp dir
            Directory.CreateDirectory(dir);

            var resultDir = Path.Combine(dir, "output");
            Directory.CreateDirectory(resultDir);

            var calcData = new CalcData();
            var generalData = new GeneralData();
            var dataWriter = new DataWriter();
            var calc = new WakeCalc();

            generalData.GridPointsX = modelGeneral.GridPointsX;
            generalData.GridPointsY = modelGeneral.GridPointsY;
            generalData.TurbinesAmount = modelTurbines.Turbines.Count;
            generalData.RotationAngle = (double)modelGeneral.RotationAngle;
            generalData.x_turb = new double[modelTurbines.Turbines.Count];
            generalData.y_turb = new double[modelTurbines.Turbines.Count];
            for (var i = 0; i < modelTurbines.Turbines.Count; i++)
            {
                var t = modelTurbines.Turbines[i];
                generalData.x_turb[i] = (double) t.X;
                generalData.y_turb[i] = (double) t.Y;
            }
            generalData.TurbineDiameter = (double)modelGeneral.TurbineDiameter;
            generalData.TurbineHeight = (double)modelGeneral.TurbineHeight;
            generalData.TurbineThrust = (double)modelGeneral.TurbineThrust;
            generalData.WakeDecay = (double)modelGeneral.WakeDecay;
            generalData.VelocityAtHub = (double)modelGeneral.VelocityAtHub;
            generalData.AirDensity = (double)modelGeneral.AirDensity;
            generalData.PowerDistance = (double)modelGeneral.PowerDistance;

            calc.Initialize(generalData, calcData);
            calc.Run(generalData, calcData);

            dataWriter.Write(generalData, calcData, resultDir);
            dataWriter.WritePower(generalData, calcData, resultDir);

            SharpZipUtils.CompressFolder(resultDir, Path.Combine(dir, "output.zip"), null);

            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public FileResult DownloadResult()
        {
            var resultsDir = Session["WakeSimDir"] as string;
            if (resultsDir == null)
            {
                return File(new byte[0], "text/plain");
            }
            string dir = WebConfigurationManager.AppSettings["WakeSimulationDir"];
            dir = Path.Combine(dir, resultsDir); // root temp dir

            var file = Path.Combine(dir, "output.zip");
            if (!System.IO.File.Exists(file))
                return File(new byte[0], "text/plain");

            return File(file, "application/zip", "output.zip");
        }

        public ActionResult PostProcessing()
        {
            ViewBag.Title = "Post-processing | WakeSim | Offwind";
            return View();
        }

        private VGeneralProperties GetModelGeneral()
        {
            VGeneralProperties model = null;
            if (Session["GeneralProperties"] != null)
            {
                model = Session["GeneralProperties"] as VGeneralProperties;
            }
            if (model == null)
            {
                model = new VGeneralProperties();
                InitGeneralProperties(model);
                Session["GeneralProperties"] = model;
            }
            return model;
        }

        private VTurbineProperties GetModelTurbines()
        {
            VTurbineProperties model = null;
            if (Session["TurbineProperties"] != null)
            {
                model = Session["TurbineProperties"] as VTurbineProperties;
            }
            if (model == null)
            {
                model = new VTurbineProperties();
                InitTurbuneProperties(model);
                Session["TurbineProperties"] = model;
            }
            return model;
        }

        private void InitGeneralProperties(VGeneralProperties m)
        {
            m.GridPointsX = 1000;
            m.GridPointsY = 1000;
            m.TurbineDiameter = 50;
            m.TurbineHeight = 70;
            m.TurbineThrust = 0.5m;
            m.WakeDecay = 0.02m;
            m.VelocityAtHub = 9m;
            m.AirDensity = 1.225m;
            m.PowerDistance = 0.2m;
            m.RotationAngle = -48.4m;
        }

        private void InitTurbuneProperties(VTurbineProperties m)
        {
            m.Turbines.AddRange(new[]
                             {
                                 new VTurbine(3396.91m, 2696.66m),
                                 new VTurbine(3132.82m, 2393.34m),
                                 new VTurbine(2870.71m, 2090.02m),
                                 new VTurbine(2605.37m, 1796.49m),
                                 new VTurbine(2343.25m, 1493.17m),
                                 new VTurbine(2077.91m, 1199.63m),
                                 new VTurbine(1806.09m, 896.31m),
                                 new VTurbine(3132.82m, 2843.43m),
                                 new VTurbine(2867.48m, 2553.16m),
                                 new VTurbine(2605.37m, 2249.84m),
                                 new VTurbine(2343.25m, 1943.26m),
                                 new VTurbine(2077.91m, 1649.72m),
                                 new VTurbine(1802.85m, 1346.40m),
                                 new VTurbine(1543.98m, 1052.86m),
                                 new VTurbine(1278.63m, 750m),
                                 new VTurbine(2870.71m, 3003.24m),
                                 new VTurbine(2605.37m, 2699.93m),
                                 new VTurbine(2346.49m, 2393.34m),
                                 new VTurbine(2081.14m, 2103.07m),
                                 new VTurbine(1802.85m, 1796.49m),
                                 new VTurbine(1540.74m, 1506.21m),
                                 new VTurbine(1275.39m, 1199.63m),
                                 new VTurbine(1016.52m, 906.10m),
                                 new VTurbine(2605.37m, 3150.01m),
                                 new VTurbine(2343.25m, 2846.69m),
                                 new VTurbine(2081.14m, 2553.16m),
                                 new VTurbine(1806.09m, 2246.58m),
                                 new VTurbine(1278.63m, 1649.72m),
                                 new VTurbine(1016.52m, 1359.45m),
                                 new VTurbine(750.0m, 1052.86m),
                                 new VTurbine(2330.31m, 3280.47m),
                                 new VTurbine(2081.14m, 3003.24m),
                                 new VTurbine(1806.09m, 2699.93m),
                                 new VTurbine(1540.74m, 2406.39m),
                                 new VTurbine(1016.52m, 1809.53m),
                                 new VTurbine(750.0m, 1506.21m),
                                 new VTurbine(1806.09m, 3150.01m),
                                 new VTurbine(1543.98m, 2859.74m),
                                 new VTurbine(1278.63m, 2553.16m),
                                 new VTurbine(1016.52m, 2262.88m),
                                 new VTurbine(750.0m, 1956.30m),
                                 new VTurbine(1540.74m, 3309.83m),
                                 new VTurbine(1278.63m, 3003.24m),
                                 new VTurbine(1016.52m, 2709.71m),
                                 new VTurbine(750.0m, 2406.39m),
                                 new VTurbine(1278.63m, 3455.94m),
                                 new VTurbine(1013.28m, 3166.32m),
                                 new VTurbine(750.0m, 2859.74m),
                             });
        }
    }
}
