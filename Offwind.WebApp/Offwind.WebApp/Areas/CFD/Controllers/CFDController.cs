using System.Web.Mvc;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class CFDController : __BaseCfdController
    {
        public ActionResult Index()
        {
            return RedirectToAction("DomainSetup", "Preprocessing", new { area = "CFD" });
        }
    }
}
