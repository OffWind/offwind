using System.Web.Mvc;
using Offwind.OpenFoam.Sintef;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class CaseManagementController : __BaseCfdController
    {
        public CaseManagementController()
        {
            SectionTitle = "Case Management";
        }

        public ActionResult Reset()
        {
            ShortTitle = "Reset";
            var m = new VWebPage();
            InitNavigation(m.Navigation);
            return View(m);
        }

        [ActionName("Reset")]
        [HttpPost]
        public ActionResult ResetSave()
        {
            ShortTitle = "Reset";
            var sd = SolverData.GetDefaultModel();
            SetSolverData(sd);
            if (Request.IsAjaxRequest()) return Json("OK");
            var m = new VWebPage();
            InitNavigation(m.Navigation);
            return View(m);
        }
    }
}