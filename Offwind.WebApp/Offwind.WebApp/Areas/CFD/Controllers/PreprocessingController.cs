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

        public ActionResult AblProperties()
        {
            Title = "Atmospheric Boundary Layer (ABL) Setup";
            ShortTitle = "ABL Properties";

            var m = new VAblProperties();
            var sd = GetSolverData();
            m.Width = sd.BlockMeshDict.vertices[1].X;
            m.Length = sd.BlockMeshDict.vertices[2].Y;
            m.Height = sd.BlockMeshDict.vertices[4].Z;

            m.GridX = sd.BlockMeshDict.MeshBlocks.numberOfCells[0];
            m.GridY = sd.BlockMeshDict.MeshBlocks.numberOfCells[1];
            m.GridZ = sd.BlockMeshDict.MeshBlocks.numberOfCells[2];

            return View(m);
        }

        public JsonResult AblPropertiesSave(VAblProperties m)
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
            m.BetaM = d.betaM;
            m.BetaSurfaceStress = d.betaSurfaceStress;
            m.DeltaLESCoeff = d.deltaLESCoeff;
            m.GammM = d.gammM;
            m.LESModel = d.LESModel;
            m.MolecularViscosity = d.nu;
            m.RoughnessHeight = d.z0;
            m.SmagorinskyConstant = d.Cs;
            m.SurfaceStressModel = d.surfaceStressModel;
            m.SurfaceTemperatureFlux = d.q0;
            m.TRef = d.TRef;
            m.TransportModel = d.transportModel;
            m.VonKarmanConstant = d.kappa;

            return View(m);
        }
    }
}
