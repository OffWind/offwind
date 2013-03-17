using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Offwind.WebApp.Areas.CFD.Models.AirfoilAndTurbulence;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class AirfoilAndTurbulenceController : __BaseCfdController
    {
        public AirfoilAndTurbulenceController()
        {
            SectionTitle = "Airfoil & Turbulence";
        }

        public ActionResult AirfoilProperties()
        {
            ShortTitle = "Airfoil Properties";
            return View(new VWebPage());
        }

        public JsonResult VGetAirfoilsList()
        {
            var sd = GetSolverData();

            IEnumerable<object[]> res = sd.AirfoilData.collection.Select(t => new object[] {t.airfoilName});
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VGetAirfoilData(int id)
        {
            var sd = GetSolverData();

            IEnumerable<object[]> res = sd.AirfoilData.collection[id].row.Select(t => new object[] {t.X, t.Y, t.Z});
            return Json(res, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TurbulenceProperties()
        {
            var m = new VTurbulenceProperties();
            var sd = GetSolverData();
            ShortTitle = "Turbulence Properties";

            m.SimulationType = sd.TurbulenceProperties.SimulationType;
            m.RASProperties.RasModelName = sd.TurbulenceProperties.RasProperties.RasModelName;
            m.RASProperties.Turbulence = sd.TurbulenceProperties.RasProperties.Turbulence;
            m.RASProperties.PrintCoeffs = sd.TurbulenceProperties.RasProperties.PrintCoeffs;

            return View(m);
        }

        [ActionName("TurbulenceProperties")]
        [HttpPost]
        public JsonResult TurbulencePropertiesSave(VTurbulenceProperties m)
        {
            var sd = GetSolverData();

            sd.TurbulenceProperties.SimulationType = m.SimulationType;
            sd.TurbulenceProperties.RasProperties.RasModelName = m.RASProperties.RasModelName;
            sd.TurbulenceProperties.RasProperties.Turbulence = m.RASProperties.Turbulence;
            sd.TurbulenceProperties.RasProperties.PrintCoeffs = m.RASProperties.PrintCoeffs;

            SetSolverData(sd);

            return Json("OK");
        }
    }
}