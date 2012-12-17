using System;
using System.Reflection;
using System.Web.Configuration;
using System.Web.Mvc;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Models.Jobs;
using log4net;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class DownloadsController : Controller
    {
        protected readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public FileResult GetInputData(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            Guid jobId;
            if (!Guid.TryParse(id, out jobId))
            {
                _log.ErrorFormat("Unable to parse job ID: {0}", id);
                throw new ArgumentException("Invalid ID");
            }

            Job job = new JobsController().GetJob(jobId);
            string fileName = String.Format("{0}.zip", job.Id);

            bool isTestMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
            if (isTestMode)
            {
                return File(ProcessingController.CreateTestJobPath(), "application/octet-stream", fileName);
            }
            string path = ProcessingController.CreateJobPath(job);
            return File(path, "application/octet-stream", fileName);
        }

        public FileResult GetResults(string id)
        {
            if (id == null) throw new ArgumentNullException("id");

            Guid jobId;
            if (!Guid.TryParse(id, out jobId))
            {
                _log.ErrorFormat("Unable to parse job ID: {0}", id);
                throw new ArgumentException("Invalid ID");
            }

            Job job = new JobsController().GetJob(jobId);
            string fileName = String.Format("{0}.zip", job.Id);

            return File(ProcessingController.CreateTestJobPath(), "application/octet-stream", fileName);

            bool isTestMode = bool.Parse(WebConfigurationManager.AppSettings["TestMode"]);
            if (isTestMode)
            {
                return File(ProcessingController.CreateTestJobPath(), "application/octet-stream", fileName);
            }
            string path = ProcessingController.CreateJobPath(job);
            return File(path, "application/octet-stream", fileName);
        }
    }
}