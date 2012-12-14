using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Offwind.WebApp.Areas.CFD.Models.Preprocessing;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
    public class PreprocessingController : __BaseCfdController
    {
        public PreprocessingController()
        {
            SectionTitle = "Pre-processing";
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
            var d = sd.TransportProperties;
            m.MolecularViscosity = d.nu;

            m.CplcNu0 = d.CplcNu0;
            m.CplcNuInf = d.CplcNuInf;
            m.CplcM = d.CplcM;
            m.CplcN = d.CplcN;

            m.BccNu0 = d.BccNu0;
            m.BccNuInf = d.BccNuInf;
            m.BccM = d.BccM;
            m.BccN = d.BccN;

            return View(m);
        }

        [ActionName("TransportProperties")]
        [HttpPost]
        public JsonResult TransportPropertiesSave(VTransportProperties m)
        {
            var sd = GetSolverData();
            var d = sd.TransportProperties;
            d.nu = m.MolecularViscosity;
            d.CplcNu0 = m.CplcNu0;
            d.CplcNuInf = m.CplcNuInf;
            d.CplcM = m.CplcM;
            d.CplcN = m.CplcN;

            d.BccNu0 = m.BccNu0;
            d.BccNuInf = m.BccNuInf;
            d.BccM = m.BccM;
            d.BccN = m.BccN;

            SetSolverData(sd);
            return Json("OK");
        }
    }
}
