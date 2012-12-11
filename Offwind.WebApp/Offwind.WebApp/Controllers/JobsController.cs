using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class JobsController : ApiController
    {
        private OffwindEntities db = new OffwindEntities();

        // GET api/jobs
        public IEnumerable<DJob> GetDJobs()
        {
            return db.DJobs.AsEnumerable();
        }

        // GET api/jobs/5
        public DJob GetDJob(Guid id)
        {
            DJob djob = db.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return djob;
        }

        // PUT api/Jobs/5
        public HttpResponseMessage PutDJob(Guid id, DJob djob)
        {
            if (ModelState.IsValid && id == djob.Id)
            {
                db.DJobs.Attach(djob);
                db.ObjectStateManager.ChangeObjectState(djob, EntityState.Modified);

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Jobs
        public HttpResponseMessage PostDJob(DJob djob)
        {
            if (ModelState.IsValid)
            {
                db.DJobs.AddObject(djob);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, djob);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = djob.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Jobs/5
        public HttpResponseMessage DeleteDJob(Guid id)
        {
            DJob djob = db.DJobs.Single(d => d.Id == id);
            if (djob == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.DJobs.DeleteObject(djob);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, djob);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}