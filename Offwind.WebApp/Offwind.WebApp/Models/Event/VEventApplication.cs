using System;
using System.ComponentModel.DataAnnotations;
using Offwind.Web.Core;

namespace Offwind.WebApp.Models.Event
{
    public class VEventApplication : BaseModel<VEventApplication, DEventApplication>
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
    }
}