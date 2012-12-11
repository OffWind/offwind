using System;

namespace Offwind.WebApp.Models.Jobs
{
    public class Job
    {
        public Guid Id { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Finished { get; set; }
        public JobState State { get; set; }
        public JobResult Result { get; set; }
        public string ResultData { get; set; }
    }
}