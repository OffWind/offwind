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
    public sealed class UserRole
    {
        public string Role { set; get; }
        public bool IsSelected { set; get; }
    }

    public sealed class UserModel
    {
        public Int32 Id { set; get; }
        [Display(Name = "User name")]
        public string Name { set; get; }

        public string SelectedRoles { set; get; }
        public List<UserRole> Roles { set; get; }

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

        [Display(Name = "Email address")]
        public string Email { set; get; }

        public string OldPassword { set; get; }

        public UserModel()
        {
            Roles = new List<UserRole>()
                        {
                            new UserRole {IsSelected = false, Role = SystemRole.Admin},
                            new UserRole {IsSelected = false, Role = SystemRole.Partner},
                            new UserRole {IsSelected = false, Role = SystemRole.User}
                        };
        }

        public void SelectRoles(string[] roles = null)
        {
            SelectedRoles = "";
            if (roles == null)
            {
                foreach (var r in Roles.Where(r => r.IsSelected))
                {
                    if (SelectedRoles != "") SelectedRoles += ";";
                    SelectedRoles = SelectedRoles + r.Role;
                }
                return;
            }
            foreach (var x in roles)
            {
                foreach (var r in Roles.Where(r => r.Role == x))
                {
                    r.IsSelected = true;
                    if (SelectedRoles != "") SelectedRoles += ";";
                    SelectedRoles = SelectedRoles + x;
                }
            }
        }

    }
}
