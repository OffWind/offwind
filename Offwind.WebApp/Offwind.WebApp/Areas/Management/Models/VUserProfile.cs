using System;
using System.Collections.Generic;
using Offwind.Web.Core;

namespace Offwind.WebApp.Areas.Management.Models
{
    public sealed class VUsersHome
    {
        public List<VUserProfile> Users { get; set; }
    }

    public sealed class UserRole
    {
        public string Role { set; get; }
        public bool IsSelected { set; get; }
    }

    public sealed class VUserProfile : BaseModel<VUserProfile, DVUserProfile>
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string MiddleName { set; get; }
        public string Email { get { return UserName; } }
        public string WorkEmail { get; set; }
        public bool IsVerified { set; get; }
        public string CompanyName { set; get; }
        public string Country { set; get; }
        public string City { set; get; }
        public string Position { set; get; }
        public string AcademicDegree { set; get; }
        public string Info { set; get; }
        public List<string> Roles { set; get; }

        public DateTime? LastActivity { set; get; }
        public DateTime CreateDate { set; get; }

        public VUserProfile()
        {
            Roles = new List<string>();
        }

        //public void SelectRoles(string[] roles = null)
        //{
        //    SelectedRoles = "";
        //    if (roles == null)
        //    {
        //        foreach (var r in Roles.Where(r => r.IsSelected))
        //        {
        //            if (SelectedRoles != "") SelectedRoles += ";";
        //            SelectedRoles = SelectedRoles + r.Role;
        //        }
        //        return;
        //    }
        //    foreach (var x in roles)
        //    {
        //        foreach (var r in Roles.Where(r => r.Role == x))
        //        {
        //            r.IsSelected = true;
        //            if (SelectedRoles != "") SelectedRoles += ";";
        //            SelectedRoles = SelectedRoles + x;
        //        }
        //    }
        //}

    }
}
