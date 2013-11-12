using System.Web.Mvc;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class NewsController : _BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
