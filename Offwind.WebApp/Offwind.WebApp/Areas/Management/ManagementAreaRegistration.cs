using System.Web.Mvc;

namespace Offwind.WebApp.Areas.Management
{
    public class ManagementAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Management";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
               "Management",
               "Management/",
               new { action = "Index",controller="Management", id = UrlParameter.Optional },
               new[] { "Offwind.WebApp.Areas.Management.Controllers" }
           );
            context.MapRoute(
              "Management_users",
              "Management/users/{action}",
              new { action = "Index", controller = "users", id = UrlParameter.Optional },
              new[] { "Offwind.WebApp.Areas.Management.Controllers" }
          );
            context.MapRoute(
              "Management_partition",
              "Management/{action}/{id}",
              new { action = "Index", controller = "Management", id = UrlParameter.Optional },
              new[] { "Offwind.WebApp.Areas.Management.Controllers" }
          );
            
            context.MapRoute(
                "Management_default",
                "Management/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Offwind.WebApp.Areas.Management.Controllers" }
            );

        }
    }
}
