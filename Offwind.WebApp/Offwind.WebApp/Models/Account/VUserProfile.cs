using System;
using System.Collections.Generic;

namespace Offwind.WebApp.Models.Account
{
    public class VUserProfile
    {
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string Info { get; set; }
        public DateTime Created { get; set; }

        public List<VProfileCase> Cases { get; set; }
    }

    public class VProfileCase
    {
        public Guid Id { get; set; }
        public DateTime Created { get; set; }
        public string Name { get; set; }
    }
}