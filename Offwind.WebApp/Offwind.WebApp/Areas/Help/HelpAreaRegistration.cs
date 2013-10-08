using System.Web.Mvc;

namespace Offwind.WebApp.Areas.Help
{
    public class HelpAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Help";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Help_default",
                "Help/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
