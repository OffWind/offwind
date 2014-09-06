using System;
using System.Collections.Generic;
using System.Text;
using Offwind.Web.Core;

namespace Offwind.WebApp.Models.Account
{
    public class VUserProfile : BaseModel<VUserProfile, DVUserProfile>
    {
        public int Id { set; get; }
        public int UserId { set; get; }
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
        public List<VProfileCase> Cases { get; set; }

        public DateTime? LastActivity { set; get; }
        public DateTime CreateDate { set; get; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }

        public VUserProfile()
        {
            Roles = new List<string>();
            Cases = new List<VProfileCase>();
        }

        public string FormattedRoles()
        {
            var txt = new StringBuilder();
            foreach (var role in Roles)
            {
                if (txt.Length > 0) txt.Append("; ");
                txt.Append(role);
            }
            return txt.ToString();
        }
    }

    public class VProfileCase
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
    }


}