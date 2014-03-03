using System.Security.Policy;
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
            "Management_users",
            "Management/users/{action}",
            new { action = "Index", controller = "users", id = UrlParameter.Optional },
            new[] { "Offwind.WebApp.Areas.Management.Controllers" }
        );
            context.MapRoute(
             "Management_help",
             "Management/Help/{action}/{id}",
             new { action = "details", controller = "Help", id = UrlParameter.Optional },
             new[] { "Offwind.WebApp.Areas.Management.Controllers" }
           );
            context.MapRoute(
              "Management_partition",
              "Management/{action}",
              new { action = "Home", controller = "Management" },
              new[] { "Offwind.WebApp.Areas.Management.Controllers" }
          );
           
          
            context.MapRoute(
              "Management_home",
              "Management/home/{controller}/{action}/{id}",
              new { id = UrlParameter.Optional },
              new[] { "Offwind.WebApp.Areas.Management.Controllers" }
          );
            context.MapRoute(
                "Management_news",
                "Management/news/{action}/{id}",
                new { controller = "News",id=UrlParameter.Optional },
                new[] { "Offwind.WebApp.Areas.Management.Controllers" }
            );
           
            
          
        }
    }
}
