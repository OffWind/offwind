using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Jobs;

namespace Offwind.WebApp.Controllers
{
    public class JobsController : ApiController
    {
        private readonly OffwindEntities _ctx = new OffwindEntities();

        // GET api/jobs
        public IEnumerable<Job> GetJobs()
        {
            return _ctx.DJobs
                .Select(MapFromDB)
                .AsEnumerable();
        }

        [ActionName("started")]
        public IEnumerable<Job> GetStartedJobs()
        {
            string state = JobState.Started.ToString();
            return _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable();
        }

        [ActionName("running")]
        public IEnumerable<Job> GetRunningJobs()
        {
            string state = JobState.Running.ToString();
            return _ctx.DJobs
                .Where(d => d.State == state)
                .Select(MapFromDB)
                .AsEnumerable();
        }

        public Job GetJob(Guid id)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }
            return MapFromDB(djob);
        }

        // PUT api/Jobs/5
        public HttpResponseMessage PutJob(Guid id, Job job)
        {
            if (ModelState.IsValid && id == job.Id)
            {
                DJob djob = _ctx.DJobs.Single(d => d.Id == id);
                if (djob == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                MapToDB(job, djob);

                try
                {
                    _ctx.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        public HttpResponseMessage PostJob(Job job)
        {
            try
            {
                var njob = PostJobManually(job);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, njob);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = njob.Id }));
                return response;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }

        public Job PostJobManually(Job job)
        {
            var dJob = new DJob();
            dJob.Id = job.Id;
            MapToDB(job, dJob);

            _ctx.DJobs.AddObject(dJob);
            _ctx.SaveChanges();
            return MapFromDB(dJob);
        }

        public HttpResponseMessage DeleteJob(Guid id)
        {
            DJob djob = _ctx.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            _ctx.DJobs.DeleteObject(djob);

            try
            {
                _ctx.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, MapFromDB(djob));
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
    }
}