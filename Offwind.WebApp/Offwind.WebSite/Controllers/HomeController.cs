using System.Web.Mvc;
using Offwind.Web.Core;

namespace Offwind.Web.Controllers
{
    public class HomeController : PagesController
    {
        public ActionResult Index()
        {
            return ShowByType(PageType.Home);
        }
    }
}
