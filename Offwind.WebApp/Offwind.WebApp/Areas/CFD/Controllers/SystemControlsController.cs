using System.Web.Mvc;
using Offwind.Products.OpenFoam.Models;
using Offwind.WebApp.Areas.CFD.Models.SystemControls;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class SystemControlsController : __BaseCfdController
    {
        public SystemControlsController()
        {
            SectionTitle = "System";
        }

        public ActionResult Time()
        {
            ShortTitle = "Time";
            var m = new VControlDict();
            var sd = GetSolverData();
            var d = sd.ControlDict;
            m.Application = d.application;
            m.StartFrom = d.startFrom;
            m.StartTime = d.startTime;
            m.StopAt = d.stopAt;
            m.EndTime = d.endTime;
            m.DeltaT = d.deltaT;
            m.WriteControl = d.writeControl;
            m.WriteInterval = d.writeInterval;
            m.PurgeWrite = d.purgeWrite;
            m.WriteFormat = d.writeFormat;
            m.WritePrecision = d.writePrecision;
            m.WriteCompression = d.writeCompression;
            m.TimeFormat = d.timeFormat;
            m.TimePrecision = d.timePrecision;
            m.IsRunTimeModifiable = d.runTimeModifiable == FlagYesNo.yes;
            m.AdjustTimeStep = d.adjustTimeStep == FlagYesNo.yes;
            m.MaxCo = d.maxCo;
            m.MaxDeltaT = d.maxDeltaT;
            return View(m);
        }

        public ActionResult Schemes()
        {
            ShortTitle = "Schemes";
            return View();
        }

        public ActionResult Solution()
        {
            ShortTitle = "Solution";
            return View();
        }

        public ActionResult ParallelExecution()
        {
            ShortTitle = "Parallel Execution";
            return View();
        }
    }
}