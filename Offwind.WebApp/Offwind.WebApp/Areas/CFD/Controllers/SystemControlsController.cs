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
using Offwind.WebApp.Infrastructure;

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
            //var m = new VSchemes(); // TODO
            return View();
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
                                                t.GetHeader(),
                                                t.type.ToString()
                                            });
                    break;
                case 1:
                    res = sch
                        .gradSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetHeader(),
                                                t.discretisation.ToString(),
                                                t.interpolation.ToString()
                                            });
                    break;
                case 2:
                    res = sch
                        .divSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetHeader(),
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
                                                t.GetHeader(),
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
                                                t.GetHeader(),
                                                t.interpolation.ToString(),
                                                t.psi.ToString()
                                            });
                    break;
                case 5:
                    res = sch
                        .snGradSchemes
                        .Select(t => new object[]
                                            {
                                                t.GetHeader(),
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

        

        public JsonResult VSchemeSetData(int id, IEnumerable<string[]> modified)
        {
            var sd = GetSolverData();
            switch (id)
            {
                case 0:
                    {
                        var lst = sd.FvScheme.ddtSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new TimeScheme(s));
                    }
                    break;
                case 1:
                    {
                        var lst = sd.FvScheme.gradSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new GradientScheme(s));
                    }
                    break;
                case 2:
                    {
                        var lst = sd.FvScheme.divSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new DivergenceScheme(s));
                    }
                    break;
                case 3:
                    {
                        var lst = sd.FvScheme.laplacianSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new LaplacianScheme(s));
                    }
                    break;
                case 4:
                    {
                        var lst = sd.FvScheme.interpolationSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new InterpolationScheme(s));
                    }
                    break;
                case 5:
                    {
                        var lst = sd.FvScheme.snGradSchemes;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new SurfaceNormalGradientScheme(s));
                    }
                    break;
                case 6:
                    {
                        var lst = sd.FvScheme.fluxCalculation;
                        lst.Clear();
                        lst.AddRange(from s in modified where s[0] != null select new FluxCalculation(s));
                    }
                    break;
                default:
                    return Json("ERROR");
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
            EmitMapperExtensions.MapList<FvSolver, VSolver>(sd.FvSolution.Solvers, m.Solver);
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