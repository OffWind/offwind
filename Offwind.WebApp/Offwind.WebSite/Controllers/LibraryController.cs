using System.Web.Mvc;
using Offwind.Web.Core;

namespace Offwind.Web.Controllers
{
    public class LibraryController : PagesController
    {
        public ActionResult Index()
        {
            return ShowByType(PageType.Library);
        }
    }
}
