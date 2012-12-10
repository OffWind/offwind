using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using Offwind.OpenFoam.Sintef;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class ProcessingController : __BaseCfdController
    {
        public ProcessingController()
        {
            SectionTitle = "Processing";
        }

        public ActionResult Settings()
        {
            ShortTitle = "Settings";
            return View();
        }

        public ActionResult Simulation()
        {
            ShortTitle = "Simulation";
            ViewBag.IsInProgress = false;
            return View();
        }

        public JsonResult SimulationStart()
        {
            var model = GetSolverData();
            using (var ctx = new OffwindEntities())
            {
                var dJob = new DJob();
                dJob.Id = Guid.NewGuid();
                dJob.Started = DateTime.UtcNow;
                dJob.Name = StandardCases.CfdCase;
                dJob.Owner = User.Identity.Name;

                var serializer = new XmlSerializer(typeof(SolverData));
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, model);
                    dJob.Model = writer.ToString();
                    writer.Close();
                    ctx.SaveChanges();
                }

                dJob.State = "Started";
                ctx.DJobs.AddObject(dJob);
                ctx.SaveChanges();
            }
            return Json("Simulation successfully started");
        }

        public JsonResult SimulationStop()
        {
            return Json("Simulation stopped");
        }
    }
}