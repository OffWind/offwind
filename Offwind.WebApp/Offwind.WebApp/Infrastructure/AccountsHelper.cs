using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Offwind.WebApp.Infrastructure
{
    public class AccountsHelper
    {
        public static bool IsAdmin(string userName)
        {
            try
            {
                return Roles.GetRolesForUser(userName).Contains("Admin");
            }
            catch (InvalidOperationException)
            {
                WebSecurity.Logout();
                return false;
            }
        }
    }
}