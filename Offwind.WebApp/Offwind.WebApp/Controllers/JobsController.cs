using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Newtonsoft.Json;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Jobs;

namespace Offwind.WebApp.Controllers
{
    /// <summary>
    /// /jobs/GetStartedJobs
    /// /jobs/GetRunningJobs
    /// /jobs/GetCancelledJobs
    /// /jobs/GetSingleJob?jobId=
    /// /jobs/IsJobCancelled?jobId=
    /// /jobs/SetJobRunning?jobId=
    /// /jobs/SetJobFinished?jobId=
    /// </summary>
    public class JobsController : Controller
    {
        private readonly OffwindEntities _ctx = new OffwindEntities();

        public JsonResult GetAllJobs()
        {
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Select(MapFromDB)
                .AsEnumerable());
        }

        public JsonResult GetStartedJobs()
        {
            string state = JobState.Started.ToString();
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable());
        }

        public JsonResult GetRunningJobs()
        {
            string state = JobState.Running.ToString();
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable());
        }

        public JsonResult GetCancelledJobs()
        {
            string state = JobState.Cancelled.ToString();
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable());
        }

        public JsonResult GetSingleJob(Guid jobId)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == jobId);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
            }
            return JsonX(HttpStatusCode.OK, MapFromDB(djob));
        }

        public JsonResult IsJobCancelled(Guid jobId)
        {
            string state = JobState.Cancelled.ToString();
            var djob = _ctx.DJobs.FirstOrDefault(d => d.Id == jobId && d.State == state);
            var isCancelled = djob != null;
            return JsonX(HttpStatusCode.OK, isCancelled);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Update(Job job)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == job.Id);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            MapToDB(job, djob);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }

            return JsonX(HttpStatusCode.OK);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Add(Job job)
        {
            try
            {
                var njob = AddJobManually(job);
                return JsonX(HttpStatusCode.Created, njob);
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetJobRunning(Guid jobId)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == jobId);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            try
            {
                djob.RunningSince = DateTime.UtcNow;
                djob.State = JobState.Running.ToString();
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }

            return JsonX(HttpStatusCode.OK);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetJobCancelled(Guid jobId)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == jobId);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            try
            {
                djob.RunningSince = DateTime.UtcNow;
                djob.State = JobState.Cancelled.ToString();
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }

            return JsonX(HttpStatusCode.OK);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SetJobFinished(Guid jobId)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == jobId);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            try
            {
                djob.Finished = DateTime.UtcNow;
                djob.State = JobState.Idle.ToString();
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }

            return JsonX(HttpStatusCode.OK);
        }

        public Job AddJobManually(Job job)
        {
            var dJob = new DJob();
            dJob.Id = job.Id;
            MapToDB(job, dJob);

            _ctx.DJobs.AddObject(dJob);
            _ctx.SaveChanges();
            return MapFromDB(dJob);
        }

        public JsonResult StopAllJobs()
        {
            try
            {
                foreach (var dJob in _ctx.DJobs)
                {
                    dJob.State = JobState.Idle.ToString();
                }
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.InternalServerError);
            }

            return JsonX(HttpStatusCode.OK);
        }

        public Job GetJob(Guid id)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == id);
            return MapFromDB(djob);
        }
        public JsonResult Delete(Guid id)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            _ctx.DJobs.DeleteObject(djob);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return JsonX(HttpStatusCode.NotFound);
            }

            return JsonX(HttpStatusCode.OK, MapFromDB(djob));
        }

        protected override void Dispose(bool disposing)
        {
            _ctx.Dispose();
            base.Dispose(disposing);
        }

        // ReSharper disable InconsistentNaming
        private static Job MapFromDB(DJob d)
        {
            return new Job
                       {
                           Id = d.Id,
                           Owner = d.Owner,
                           Name = d.Name,
                           Started = d.Started,
                           Finished = d.Finished,
                           RunningSince = d.RunningSince,
                           State = (JobState)Enum.Parse(typeof(JobState), d.State),
                           Result = (JobResult) Enum.Parse(typeof (JobResult), d.Result),
                           ResultData = d.ResultData
                       };
        }

        private static void MapToDB(Job job, DJob djob)
        {
            djob.Started = job.Started;
            djob.Finished = job.Finished;
            djob.Name = job.Name;
            djob.Owner = job.Owner;
            djob.State = job.State.ToString();
            djob.Result = job.Result.ToString();
            djob.ResultData = job.ResultData;
        }

        // ReSharper restore InconsistentNaming

        private JsonResult JsonX(HttpStatusCode status, object data)
        {
            return Json(new { status, data }, JsonRequestBehavior.AllowGet);
        }

        private JsonResult JsonX(HttpStatusCode status)
        {
            return Json(new { status, data = "" }, JsonRequestBehavior.AllowGet);
        }
    }
}