using System.Linq.Expressions;
using System.Web.Mvc;
using System.Linq;
using System.Web.Routing;
using Offwind.Web.Core;
using Offwind.WebApp.Areas.Management.Tools;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            using (var ctx = new OffwindEntities())
            {
                var ct = ContentType.Page.S();
                foreach (var dc in ctx.DContents.Where(c => c.TypeId == ct))
                {
                    if (dc.Route.Trim().Length <= 0) continue;
                    routes.MapRoute(
                        dc.Id.ToString(),
                        dc.Route,
                        new { controller = "Content", action = "ShowById", id = dc.Id },
                        new[] { "Offwind.WebApp.Controllers" }
                    );
                }
            }

            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                new [] {"Offwind.WebApp.Controllers"}
            );
        }
    }
}