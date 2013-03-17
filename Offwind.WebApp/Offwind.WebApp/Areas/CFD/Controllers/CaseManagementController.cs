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
            return View(new VWebPage());
        }

        [ActionName("Reset")]
        [HttpPost]
        public ActionResult ResetSave()
        {
            ShortTitle = "Reset";
            var sd = SolverData.GetDefaultModel();
            SetSolverData(sd);
            if (Request.IsAjaxRequest()) return Json("OK");
            return View(new VWebPage());
        }
    }
}