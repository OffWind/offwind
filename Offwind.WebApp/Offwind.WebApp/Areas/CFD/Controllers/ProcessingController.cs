using System;
using System.IO;
using System.Web.Mvc;
using System.Xml.Serialization;
using Offwind.OpenFoam.Sintef;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Jobs;

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
            var solverData = GetSolverData();
            var now = DateTime.UtcNow;
            var inputData = solverData.ArchName(now.ToBinary().ToString());
            var inputFs = solverData.MakeFS();
            SharpZipUtils.CompressFolder(inputFs, inputData, null);
            Directory.Delete(inputFs, true);

            var job = new Job
            {
                Id = Guid.NewGuid(),
                Started = now,
                Owner = User.Identity.Name,
                Name = StandardCases.CfdCase,
                State = JobState.Started,
                InputData = inputData // or may be just allocate memory for zip data ?
            };

            new JobsController().AddJobManually(job);
            return Json("Simulation successfully started");
        }

        public JsonResult SimulationStop()
        {
            return Json("Simulation stopped");
        }
    }
}