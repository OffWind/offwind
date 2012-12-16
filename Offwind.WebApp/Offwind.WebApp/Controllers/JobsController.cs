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
    public class JobsController : Controller
    {
        private readonly OffwindEntities _ctx = new OffwindEntities();

        [ActionName("all")]
        public JsonResult GetAllJobs()
        {
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Select(MapFromDB)
                .AsEnumerable());
        }

        [ActionName("started")]
        public JsonResult GetStartedJobs()
        {
            string state = JobState.Started.ToString();
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable());
        }

        [ActionName("running")]
        public JsonResult GetRunningJobs()
        {
            string state = JobState.Running.ToString();
            return JsonX(HttpStatusCode.OK,
                _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable());
        }

        public JsonResult Single(Guid id)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                return JsonX(HttpStatusCode.NotFound, JsonRequestBehavior.AllowGet);
            }
            return JsonX(HttpStatusCode.OK, MapFromDB(djob));
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

        public Job AddJobManually(Job job)
        {
            var dJob = new DJob();
            dJob.Id = job.Id;
            MapToDB(job, dJob);

            _ctx.DJobs.AddObject(dJob);
            _ctx.SaveChanges();
            return MapFromDB(dJob);
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
                           State = (JobState) Enum.Parse(typeof (JobState), d.State),
                           Result = (JobResult) Enum.Parse(typeof (JobResult), d.Result),
                           Finished = d.Finished,
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