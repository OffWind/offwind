using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using AutoMapper;
using Offwind.OpenFoam.Models.Fields;
using Offwind.Sowfa.Constant.TransportProperties;
using Offwind.WebApp.Areas.CFD.Models.BoundaryConditions;
using Offwind.WebApp.Areas.CFD.Models.Preprocessing;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class PreprocessingController : __BaseCfdController
    {
        public PreprocessingController()
        {
            SectionTitle = "Pre-processing";
            Mapper.CreateMap<VTransportProperties, TransportPropertiesData>();
            Mapper.CreateMap<TransportPropertiesData, VTransportProperties>();
        }

        public ActionResult DomainSetup()
        {
            Title = "Domain Setup";
            ShortTitle = "Domain Setup";

            var m = new VDomainSetup();
            var sd = GetSolverData();
            m.Width = sd.BlockMeshDict.vertices[1].X;
            m.Length = sd.BlockMeshDict.vertices[2].Y;
            m.Height = sd.BlockMeshDict.vertices[4].Z;

            m.GridX = sd.BlockMeshDict.MeshBlocks.numberOfCells[0];
            m.GridY = sd.BlockMeshDict.MeshBlocks.numberOfCells[1];
            m.GridZ = sd.BlockMeshDict.MeshBlocks.numberOfCells[2];

            return View(m);
        }

        [ActionName("DomainSetup")]
        [HttpPost]
        public JsonResult DomainSetupSave(VDomainSetup m)
        {
            var sd = GetSolverData();
            sd.BlockMeshDict.vertices[1].X = m.Width;
            sd.BlockMeshDict.vertices[2].X = m.Width;
            sd.BlockMeshDict.vertices[5].X = m.Width;
            sd.BlockMeshDict.vertices[6].X = m.Width;

            sd.BlockMeshDict.vertices[2].Y = m.Length;
            sd.BlockMeshDict.vertices[3].Y = m.Length;
            sd.BlockMeshDict.vertices[6].Y = m.Length;
            sd.BlockMeshDict.vertices[7].Y = m.Length;

            sd.BlockMeshDict.vertices[4].Z = m.Height;
            sd.BlockMeshDict.vertices[5].Z = m.Height;
            sd.BlockMeshDict.vertices[6].Z = m.Height;
            sd.BlockMeshDict.vertices[7].Z = m.Height;

            SetSolverData(sd);
            return Json("OK");
        }

        public ActionResult StlGenerator()
        {
            Title = "Earth Elevation STL Generator";
            ShortTitle = "STL Generator";
            return View();
        }

        public FileResult GenerateStl()
        {
            Thread.Sleep(3000);
            return File(new byte[0], "text/plain", "result.stl");
        }

        public ActionResult TransportProperties()
        {
            ShortTitle = "Transport Properties";
            var m = new VTransportProperties();
            var sd = GetSolverData();
            Mapper.Map(sd.TransportProperties, m);
            return View(m);
        }

        [ActionName("TransportProperties")]
        [HttpPost]
        public JsonResult TransportPropertiesSave(VTransportProperties m)
        {
            var sd = GetSolverData();
            Mapper.Map(m, sd.TransportProperties);
            SetSolverData(sd);
            return Json("OK");
        }
    }
}
