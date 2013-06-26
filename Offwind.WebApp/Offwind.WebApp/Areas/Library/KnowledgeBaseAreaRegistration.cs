using System.Web.Mvc;

namespace Offwind.WebApp.Areas.Library
{
    public class KnowledgeBaseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Library";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Library_default",
                "library/{controller}/{action}/{id}",
                new { action = "Index", controller = "HomeLibrary", id = UrlParameter.Optional }
            );
        }
    }
}
