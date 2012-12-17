using System;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Web.Configuration;
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
            rootPath = "C:\\work\\temp"; // just for tests, take value from Web.config
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
            var dCase = GetCase();
            var caseHasJob = dCase.CurrentJobId != null;
            bool jobActive = false;
            if (caseHasJob)
            {
                var dJob = ctx.DJobs.FirstOrDefault(dj => dj.Id == dCase.CurrentJobId);
                jobActive = (dJob != null) && (dJob.State != JobState.Idle.ToString());
            }
            ViewBag.IsInProgress = jobActive;
            return View();
        }

        public JsonResult SimulationStart()
        {
            var job = new Job
            {
                Id = Guid.NewGuid(),
                Started = DateTime.UtcNow,
                Owner = User.Identity.Name,
                Name = StandardCases.CfdCase,
                State = JobState.Started,                
            };

            var jobZip = CreateJobPath(job);
            var jobPath = jobZip.Replace(".zip", "");
            
            var solverData = GetSolverData();            
            solverData.MakeJobFS(jobPath);
            SharpZipUtils.CompressFolder(jobPath, jobZip, null);

            new JobsController().AddJobManually(job);

            SetCaseJob(job.Id);
            return Json(job.Id);
        }

        public JsonResult SimulationStop()
        {
            var dCase = GetCase();
            if (dCase.CurrentJobId == null) return Json("Already idle");
            new JobsController().StopJob(dCase.CurrentJobId.Value);
            return Json("Simulation stopped");
        }

        public static string CreateJobPath(Job job)
        {
            Contract.Requires(job != null);
            Contract.Requires(job.Id != Guid.Empty);
            Contract.Requires(job.Owner != null);
            Contract.Requires(job.Owner.Trim().Length > 0);
            Contract.Requires(job.Owner.Trim().Length == job.Owner.Length); // No pre- and post- spaces

            string path = WebConfigurationManager.AppSettings["UsersDir"];
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, job.Owner);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, job.Id.ToString());
            path += ".zip";
            return path;
        }

        public static string CreateTestJobPath()
        {
            string path = WebConfigurationManager.AppSettings["UsersDir"];
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path = Path.Combine(path, "test");
            path += ".zip";
            return path;
        }

        public ActionResult History()
        {
            ShortTitle = "History";
            var jobs = ctx.DJobs
                .Where(dj => dj.Owner == User.Identity.Name)
                .OrderByDescending(dj => dj.Started);
            return View(jobs);
        }
    }
}