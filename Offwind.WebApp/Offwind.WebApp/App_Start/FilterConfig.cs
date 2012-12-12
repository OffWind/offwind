using System.Web.Mvc;
using Offwind.WebApp.Filters;

namespace Offwind.WebApp.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new InitializeSimpleMembershipAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}