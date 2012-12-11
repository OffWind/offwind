using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Offwind.WebApp.Models.Jobs
{
    public class Job
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
    }
}