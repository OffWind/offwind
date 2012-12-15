using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Offwind.OpenFoam.Models.Fields;
using Offwind.OpenFoam.Sintef.BoundaryFields;
using Offwind.Products.OpenFoam.Models.Fields;
using Offwind.WebApp.Areas.CFD.Models.BoundaryConditions;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class BoundaryConditionsController : __BaseCfdController
    {
        public BoundaryConditionsController()
        {
            Mapper.CreateMap<PatchValueScalar, VFieldScalarValue>();
            Mapper.CreateMap<VFieldScalarValue, PatchValueScalar>();

            Mapper.CreateMap<FieldK, VFieldK>();
            Mapper.CreateMap<VFieldK, FieldK>();

            SectionTitle = "Boundary Conditions";
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FieldK()
        {
            ShortTitle = "k";
            var m = new VFieldK();
            var sd = GetSolverData();
            Mapper.Map(sd.FieldK, m);
            return View(m);
        }

        public ActionResult FieldEpsilon()
        {
            ShortTitle = "epsilon";
            var m = new VFieldEpsilon();
            var sd = GetSolverData();
            Mapper.Map(m, sd.FieldK);
            return View(m);
        }

        public ActionResult FieldP()
        {
            ShortTitle = "p";
            var m = new VFieldP();
            return View(m);
        }

        public ActionResult FieldR()
        {
            ShortTitle = "R";
            var m = new VFieldR();
            return View(m);
        }

        public ActionResult FieldU()
        {
            ShortTitle = "U";
            var m = new VFieldU();
            m.BottomType = PatchType.cyclic;
            return View(m);
        }

        [ActionName("FieldU")]
        [HttpPost]
        public ActionResult FieldUSave(VFieldU m)
        {
            ShortTitle = "U";
            return View(m);
        }
    }
}
