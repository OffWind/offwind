using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EmitMapper;
using Offwind.OpenFoam.Models.FvSolution;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.Sowfa.System.ControlDict;
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
            ObjectMapperManager.DefaultInstance.GetMapper<ControlDictData, VControlDict>().Map(sd.ControlDict, m);
            return View(m);
        }

        [ActionName("Time")]
        [HttpPost]
        public JsonResult TimeSave(VControlDict m)
        {
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<VControlDict, ControlDictData>().Map(m, sd.ControlDict);
            SetSolverData(sd);
            return Json("OK");
        }

        public ActionResult Schemes()
        {
            ShortTitle = "Schemes";
            return View();
        }

        
        public ActionResult Solution()
        {
            ShortTitle = "Solution";
            var m = new VFvSolution();
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<FvSolutionData, VFvSolution>().Map(sd.FvSolution, m);
            //ObjectMapperManager.DefaultInstance.GetMapper<List<FvSolver> , List<VSolver>>().Map(sd.FvSolution.Solvers, m.Solver);

            return View(m);
        }

        public JsonResult VFvSolutionSolverData()
        {
            var sd = GetSolverData();
            var arr = sd.FvSolution
                .Solvers
                .Select( t => new object[]
                {
                    t.Name,
                    t.Solver.ToString(),
                    t.Preconditioner.ToString(),
                    t.Tolerance,
                    t.RelTol
                }); 
            return Json(arr, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VFvSolutionSolverSave(List<string[]> modified)
        {
            var sd = GetSolverData();
            var l = sd.FvSolution.Solvers;

            l.Clear();
            foreach (var x in modified)
            {
                if ((x[0] != null) && (x[1] != null) && (x[2] != null))
                {
                    var s = new FvSolver();
                    s.Name = x[0];
                    s.Solver = (LinearSolver) Enum.Parse(typeof (LinearSolver), x[1]);
                    s.Preconditioner = (Preconditioner) Enum.Parse(typeof (Preconditioner), x[2]);
                    s.Tolerance = Convert.ToDecimal(x[3]);
                    s.RelTol = Convert.ToDecimal(x[4]);
                    l.Add(s);
                }
            }
            SetSolverData(sd);
            return Json("OK");
        }

        public ActionResult ParallelExecution()
        {
            ShortTitle = "Parallel Execution";
            return View();
        }
    }
}