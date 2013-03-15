using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using EmitMapper;
using Offwind.WebApp.Areas.EngineeringTools.Models.WindFarm;
using Offwind.WebApp.Infrastructure;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class WindFarmController : Controller
    {
        private static VWindFarm _model = null;
        //private static Thread _thread = null;
        private double process = 0;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InputData()
        {
            var model = new VWindFarm();
            if (_model == null)
            {
                _model = new VWindFarm();
                _model.Turbines.Add(new VWindTurbine() {rho = 1, radius = 63, rated = 5, Cp = 0.45, speed = 12});
            }
            ObjectMapperManager.DefaultInstance.GetMapper<VWindFarm, VWindFarm>().Map(_model, model);
            return View(model);
        }

        [ActionName("InputData")]
        [HttpPost]
        public ActionResult InputData(VWindFarm model)
        {
            lock (_model)
            {
                _model.StartTime = model.StartTime;
                _model.StopTime = model.StopTime;
                _model.TimeStep = model.TimeStep;
                _model.Scale = model.Scale;
                return View(_model);
            }
        }

        public ActionResult Simulation()
        {
            return View();
        }

        public JsonResult GetTurbines()
        {
            var res = _model
                .Turbines
                .Select( t => new object[]
                                  {
                                      t.rho.ToString(CultureInfo.InvariantCulture),
                                      t.radius.ToString(CultureInfo.InvariantCulture),
                                      t.rated.ToString(CultureInfo.InvariantCulture),
                                      t.Cp.ToString(CultureInfo.InvariantCulture),
                                      t.speed.ToString(CultureInfo.InvariantCulture)
                                  });

            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveTurbines(IEnumerable<string[]> modified)
        {
            lock (_model)
            {
                _model.Turbines.Clear();
                _model.Turbines.AddRange(from s in modified where s[0] != null select new VWindTurbine(s));
                return Json("OK");
            }
        }

        /*
        static void Compute()
        {
            lock (_model)
            {
                double t = _model.StartTime;
                var turbines = _model.Turbines.Count;
                var res = new double[turbines];

                while (t < _model.StopTime)
                {
                    double sum = 0;
                    for (int i = 0; i < turbines; i++)
                    {
                        var x = _model.Turbines[i];
                        double val = (Math.PI/2)*x.rho*x.radius*x.radius*x.speed*x.speed*x.speed*x.Cp;
                        res[i] = Math.Min(x.rated, val);
                        sum += res[i];
                    }
                    for (int i = 0; i < turbines; i++)
                    {
                        var x = _model.Turbines[i];
                        res[i] = Math.Max(0, Math.Min(x.rated, _model.Scale*res[i]/sum));
                    }
                    t = t + _model.TimeStep;
                }
            }
        }
         */

        public JsonResult Run()
        {
            /*
            if (_thread == null)
            {
                _thread = new Thread(Compute);
            }
            _thread.Start();
             */

            process = _model.StartTime;
            var turbines = _model.Turbines.Count;
            var res = new double[turbines];

            var randomDir = Guid.NewGuid().ToString();
            Session["WindFarmDir"] = randomDir;

            string dir = WebConfigurationManager.AppSettings["WindFarmSimulationDir"];
            dir = Path.Combine(dir, randomDir);
            Directory.CreateDirectory(dir);
            string wdir = Path.Combine(dir, "windfarm");
            Directory.CreateDirectory(wdir);
            double delta = 0;
            using (var f = System.IO.File.Open(Path.Combine(wdir, "windfarm.dat"), FileMode.OpenOrCreate, FileAccess.Write))
            using (var b = new StreamWriter(f))
            {
                while (process < _model.StopTime)
                {
                    double sum = 0;
                    for (var i = 0; i < turbines; i++)
                    {
                        var t = _model.Turbines[i];
                        var val = (Math.PI/2)*t.rho*t.radius*t.radius*t.speed*t.speed*t.speed*t.Cp;
                        res[i] = Math.Min(t.rated, val);
                        sum += res[i];
                    }

                    b.Write(delta*_model.TimeStep);
                    delta += 1;

                    for (var i = 0; i < turbines; i++)
                    {
                        var t = _model.Turbines[i];
                        var x = Math.Max(0, Math.Min(t.rated, _model.Scale*res[i]/sum));
                        b.Write(" " + x);
                    }
                    b.WriteLine();
                    process += _model.TimeStep;
                }
                b.Close();
                f.Close();
            }

            SharpZipUtils.CompressFolder(wdir, Path.Combine(dir, "windfarm.zip"), null);

            return Json("OK");
        }

        public FileResult DownloadResult()
        {
            var resultsDir = Session["WindFarmDir"] as string;
            if (resultsDir == null)
            {
                return File(new byte[0], "text/plain");
            }
            string dir = WebConfigurationManager.AppSettings["WindFarmSimulationDir"];
            dir = Path.Combine(dir, resultsDir); // root temp dir

            var file = Path.Combine(dir, "windfarm.zip");
            if (!System.IO.File.Exists(file))
                return File(new byte[0], "text/plain");

            return File(file, "application/zip", "windfarm.zip");
        }

        public JsonResult Tick()
        {
            double elapsed = ((process - _model.StartTime)/(_model.StopTime - _model.StartTime)) * 100;
            return Json(elapsed.ToString(CultureInfo.InvariantCulture));
        }
    }
}
