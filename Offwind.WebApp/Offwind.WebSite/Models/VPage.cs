using System;
using System.ComponentModel.DataAnnotations;

namespace Offwind.Web.Models
{
    public sealed class VPage
    {
        //[Required(ErrorMessage = "обязательное поле")]
        //public string PageTitle { get; set; }

        public string Slug { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        public int Id { get; set; }

        public DateTime Updated { get; set; }
        public DateTime Published { get; set; }

        public bool IsHot { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        public string NTitle { get; set; } // "Title" doesn't work for unobtrusive javascript validation =(((

        public string Announce { get; set; }
        public string PageType { get; set; }

        [Required(ErrorMessage = "обязательное поле")]
        public string Text { get; set; }

        public string ReturnUrl { get; set; }

        public VPage()
        {
            Published = DateTime.UtcNow;
        }
    }
}