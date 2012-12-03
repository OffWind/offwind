using System.Web.Mvc;

namespace MvcApplication1.Areas.EngineeringTools
{
    public class EngineeringToolsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "EngineeringTools";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "EngineeringTools_default",
                "EngineeringTools/{controller}/{action}/{id}",
                new { controller = "EngineeringTools", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
