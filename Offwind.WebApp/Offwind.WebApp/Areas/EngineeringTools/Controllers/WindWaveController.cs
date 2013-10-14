using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Serialization;
using Offwind.Web.Core;
using Offwind.WebApp.Areas.EngineeringTools.Models.WindWave;
using Offwind.WebApp.Areas.EngineeringTools.Models.WindWave.Computations;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class WindWaveController : _BaseController
    {
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            Debug.Assert(Request.IsAuthenticated);

            var user = User.Identity.Name;
            using (var ctx = new OffwindEntities())
            {
                var dCase = ctx.DCases.FirstOrDefault(c => c.Owner == user && c.Name == StandardCases.WindWave);
                if (dCase == null)
                {
                    // Init basic properties
                    dCase = new DCase();
                    dCase.Id = Guid.NewGuid();
                    dCase.Name = StandardCases.WindWave;
                    dCase.Owner = user;
                    dCase.Created = DateTime.UtcNow;

                    // Init model
                    var model = CreateProjectModel();
                    var serializer = new XmlSerializer(typeof(WindWaveInput));
                    using (var writer = new StringWriter())
                    {
                        serializer.Serialize(writer, model);
                        dCase.Model = writer.ToString();
                        writer.Close();
                    }

                    ctx.DCases.AddObject(dCase);
                    ctx.SaveChanges();
                }
            }
            base.Initialize(requestContext);
        }

        public ActionResult InputData()
        {
            ViewBag.Title = "Input Data | Wind Wave | Offwind";
            var m = new VWindWave();
            var d = GetDbModel();
            m.WindSpeed = (decimal) d.Ug;
            m.ReferenceHeight = (decimal)d.Zg;
            m.TurbineHubHeight = (decimal)d.Zhub;
            m.TurbineDiameter = (decimal)d.Td;
            m.TurbineEfficiency = (decimal)d.Ef;
            m.WaveSpeed = (decimal)d.Cw;
            return View(m);
        }


        [ActionName("InputData")]
        [HttpPost]
        public ActionResult InputDataSave(VWindWave m)
        {
            var d = GetDbModel();
            d.Ug = (double) m.WindSpeed;
            d.Zg = (double)m.ReferenceHeight;
            d.Zhub = (double)m.TurbineHubHeight;
            d.Td = (double)m.TurbineDiameter;
            d.Ef = (double)m.TurbineEfficiency;
            d.Cw = (double)m.WaveSpeed;
            SetDbModel(d);
            if (Request.IsAjaxRequest()) return Json("OK");
            return View(m);
        }

        public ActionResult PowerOutput()
        {
            ViewBag.Title = "Power Output | Wind Wave | Offwind";
            var m = new VWebPage();
            return View(m);
        }

        public JsonResult PowerOutputData()
        {
            var input = GetDbModel();
            var calc = new Calculator();
            calc.Do(input);
            var arr = calc.PowerOutput.Select(po =>
                new object[]
                    {
                        po.Method,
                        po.Velocity.ToString ("0.00000000"),
                        po.Output.ToString ("0.00000000"),
                        po.Differences.ToString ("0.00000000")
                    }).ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PowerOutputAdvanced()
        {
            ViewBag.Title = "Power Output Advanced | Wind Wave | Offwind";
            var m = new VWebPage();
            return View(m);
        }

        public JsonResult PowerOutputAdvancedData()
        {
            var input = GetDbModel();
            var calc = new Calculator();
            calc.Do(input);
            var arr = calc.AdvancedCfdItems.Select(po =>
                new object[]
                    {
                        po.Method,
                        po.FrictionVelocity.ToString ("0.00000000"),
                        po.RoughnessHeight.ToString ("0.00000000")
                    }).ToArray();
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public WindWaveInput CreateProjectModel()
        {
            var m = new WindWaveInput();
            m.Ug = 7;
            m.Zg = 20;
            m.Zhub = 100;
            m.Td = 100;
            m.Ef = 35;
            m.Cw = 2;
            return m;
        }

        protected WindWaveInput GetDbModel()
        {
            using (var ctx = new OffwindEntities())
            {
                var dCase = ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.WindWave);
                var serializer = new XmlSerializer(typeof(WindWaveInput));
                using (var reader = new StringReader(dCase.Model))
                {
                    return (WindWaveInput)serializer.Deserialize(reader);
                }
            }
        }

        protected void SetDbModel(WindWaveInput model)
        {
            var serializer = new XmlSerializer(typeof(WindWaveInput));
            using (var ctx = new OffwindEntities())
            using (var writer = new StringWriter())
            {
                var dCase = ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.WindWave);
                serializer.Serialize(writer, model);
                dCase.Model = writer.ToString();
                writer.Close();
                ctx.SaveChanges();
            }
        }
    }
}
