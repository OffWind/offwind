using System.Web.Mvc;

namespace Offwind.WebApp.Areas.WindFarms
{
    public class WindFarmsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "WindFarms";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "WindFarms_default",
                "WindFarms/{controller}/{action}/{id}",
                new { action = "Index", controller = "HomeWindFarms", id = UrlParameter.Optional }
            );
        }
    }
}
