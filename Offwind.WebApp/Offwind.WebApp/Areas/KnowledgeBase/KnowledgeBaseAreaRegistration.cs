using System.Web.Mvc;

namespace Offwind.WebApp.Areas.KnowledgeBase
{
    public class KnowledgeBaseAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "KnowledgeBase";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "KnowledgeBase_default",
                "kb/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
