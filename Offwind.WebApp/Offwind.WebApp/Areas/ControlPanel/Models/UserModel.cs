using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.ControlPanel.Models
{
    public sealed class UserModel
    {
        public Int32 Id { set; get; }
        [Display(Name = "User name")]
        [ReadOnly(true)]
        public string Name { set; get; }
        public string Role { set; get; }

        [Required]
        [Display(Name = "User name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string NewName { set; get; }

        [Required]
        [Display(Name = "User role")]
        public SystemRoleType RoleT { set; get; }

        public DateTime LastVisit { set; get; }
        public DateTime CreateDate { set; get; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { set; get; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { set; get; }

        public string OldPassword { set; get; }
    }
}
