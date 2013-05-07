using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.ControlPanel.Models
{
    public class ContentModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public ContentType ContentType { get; set; }

        [DisplayName("Date")]
        [Display(Description = "YYYY-MM-DD.")]
        public string DisplayDateTime { get; set; }

        [DisplayName("Browser title")]
        [Display(Description = "For page contents this field is necessary as it appears in the browser header.")]
        public string BrowserTitle { get; set; }

        [Display(Description = "For pages this is a relative URL.")]
        public string Route { get; set; }

        [Display(Description = "Unique name. It is not appeared anywhere on public web-site.")]
        public string Name { get; set; } // "Title" doesn't work for unobtrusive javascript validation =(((

        [DisplayName("Title")]
        [Display(Description = "This is a public title that will appear on web-site.")]
        public string NTitle { get; set; } // "Title" doesn't work for unobtrusive javascript validation =(((

        [Display(Description = "For news this is a short preview text.")]
        public string Announce { get; set; }

        [DisplayName("Text")]
        public string Content { get; set; }

        public string ReturnUrl { get; set; }
    }
}
