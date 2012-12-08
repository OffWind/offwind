using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD
{
    public class CFDAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CFD";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CFD_default",
                "CFD/{controller}/{action}/{id}",
                new { controller = "CFD", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
