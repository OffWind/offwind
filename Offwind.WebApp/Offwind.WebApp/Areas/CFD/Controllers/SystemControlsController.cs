using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EmitMapper;
using Offwind.OpenFoam.Models.FvSolution;
using Offwind.Products.OpenFoam.Models;
using Offwind.Products.OpenFoam.Models.FvSolution;
using Offwind.Sowfa.System.ControlDict;
using Offwind.Sowfa.System.FvSchemes;
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
            var m = new VSchemes();
            return View(m);
        }

        public JsonResult VSchemeGetData(int id)
        {
            var sch = GetSolverData().FvScheme;
            IEnumerable<object[]> res;

            switch (id)
            {
                case 0:
                    res = sch
                        .ddtSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.type.ToString()
                                            });
                    break;
                case 1:
                    res = sch
                        .gradSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.discretisation.ToString(),
                                                t.interpolation.ToString()
                                            });
                    break;
                case 2:
                    res = sch
                        .divSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.discretisation.ToString(),
                                                t.interpolation.ToString(),
                                                t.psi.ToString()
                                            });
                    break;
                case 3:
                    res = sch
                        .laplacianSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.discretisation.ToString(),
                                                t.interpolation.ToString(),
                                                t.snGradScheme.ToString()
                                            });
                    break;
                case 4:
                    res = sch
                        .interpolationSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.interpolation.ToString(),
                                                t.psi.ToString()
                                            });
                    break;
                case 5:
                    res = sch
                        .snGradSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetFunction(),
                                                t.type.ToString()
                                            });
                    break;
                case 6:
                    res = sch
                            .fluxCalculation
                            .Select(t => new object[]
                                             {
                                                 t.flux,
                                                 t.enable.ToString()
                                             });
                    break;
                default:
                    return Json("ERROR");
            }
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public JsonResult VSchemeSetData(int id, List<string[]> modified)
        {
            var sd = GetSolverData();
            switch (id)
            {
                case 0:
                    {
                        var lst = sd.FvScheme.ddtSchemes;
                        lst.Clear();
                        foreach (var s in modified)
                        {
                            if ((s[0] != null) && (s[1] != null))
                            {
                                var x = new TimeScheme();
                                x.SetHeader(ref s[0]);
                                x.type = (TimeSchemeType) Enum.Parse(typeof (TimeSchemeType), s[1]);
                                lst.Add(x);
                            }
                        }
                    }
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    return Json("OK");
            }

            SetSolverData(sd);
            return Json("OK");
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

        [ActionName("Solution")]
        [HttpPost]
        public JsonResult SolutionSave(VFvSolution m)
        {
            var sd = GetSolverData();
            ObjectMapperManager.DefaultInstance.GetMapper<VFvSolution, FvSolutionData>().Map(m, sd.FvSolution);
            SetSolverData(sd);
            return Json("OK");
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