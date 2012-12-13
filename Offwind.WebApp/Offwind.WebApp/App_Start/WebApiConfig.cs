using System;
using System.Web.Http;
using Offwind.WebApp.Infrastructure;

namespace Offwind.WebApp.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "SingleJob",
                routeTemplate: "api/jobs/single/{guid}",
                defaults: new { controller = "Jobs", action = "Single", guid = Guid.Empty },
                constraints: new { guid = new GuidConstraint() });
            config.Routes.MapHttpRoute(
                name: "ActionApi",
                routeTemplate: "api/{controller}/{action}");
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}"
            );
        }
    }
}
