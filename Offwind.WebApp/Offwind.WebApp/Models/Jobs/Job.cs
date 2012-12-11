using System;
using System.ComponentModel.DataAnnotations;

namespace Offwind.WebApp.Models.Jobs
{
    public sealed class Job
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Owner { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Started { get; set; }

        public DateTime? Finished { get; set; }

        [Required]
        public JobState State { get; set; }

        public JobResult Result { get; set; }
        public string ResultData { get; set; }
    }
}