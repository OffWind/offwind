using System;
using System.Collections.Generic;
using System.Text;

namespace Offwind.WebApp.Models.Account
{
    public class VUserProfile
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Info { get; set; }
        public DateTime Created { get; set; }
        public List<string> Roles { get; set; }
        public List<VProfileCase> Cases { get; set; }

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

        public VUserProfile()
        {
            Roles = new List<string>();
            Cases = new List<VProfileCase>();
        }
    }

    public class VProfileCase
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
    }


}