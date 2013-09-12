using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Serialization;
using Offwind.OpenFoam.Sintef;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;
using Offwind.WebApp.Models.Account;
using log4net;
using Offwind.Web.Core;

namespace Offwind.WebApp.Areas.CFD.Controllers
{
// ReSharper disable InconsistentNaming
    [Authorize(Roles = SystemRole.User)]
    public class __BaseCfdController : BaseController
    {
        protected readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected string Title;
        protected string ShortTitle;
        protected string SectionTitle;
        protected string rootPath;

        protected OffwindEntities ctx = new OffwindEntities();

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            Debug.Assert(Request.IsAuthenticated);

            var user = User.Identity.Name;
            var dCase = ctx.DCases.FirstOrDefault(c => c.Owner == user && c.Name == StandardCases.CfdCase);
            if (dCase == null)
            {
                // Init basic properties
                dCase = new DCase();
                dCase.Id = Guid.NewGuid();
                dCase.Name = StandardCases.CfdCase;
                dCase.Owner = user;
                dCase.Created = DateTime.UtcNow;

                // Init model
                var model = SolverData.GetDefaultModel();
                var serializer = new XmlSerializer(typeof (SolverData));
                using (var writer = new StringWriter())
                {
                    serializer.Serialize(writer, model);
                    dCase.Model = writer.ToString();
                    writer.Close();
                }

                ctx.DCases.AddObject(dCase);
                ctx.SaveChanges();
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.SectionTitle = SectionTitle;
            ViewBag.ShortTitle = ShortTitle ?? Title;
            ViewBag.Title = String.Format("{0} | {1} | CFD | Offwind", Title ?? ShortTitle, SectionTitle);

            InitNavigation();
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            _log.Error(filterContext.Exception);
        }

        private void InitNavigation()
        {
            var navigation = new NavItem<NavUrl>();
            navigation.AddGroup("Pre-processing")
                .AddItem("Domain Setup", new NavUrl("DomainSetup", "Preprocessing", "CFD"))
                .AddItem("Transport Properties", new NavUrl("TransportProperties", "Preprocessing", "CFD"))
                .AddItem("Earth STL Generator", new NavUrl("StlGenerator", "Preprocessing", "CFD"));

            navigation.AddGroup("Boundary Conditions")
                .AddItem("k", new NavUrl("FieldK", "BoundaryConditions", "CFD"))
                .AddItem("epsilon", new NavUrl("FieldEpsilon", "BoundaryConditions", "CFD"))
                .AddItem("p", new NavUrl("FieldP", "BoundaryConditions", "CFD"))
                .AddItem("R", new NavUrl("FieldR", "BoundaryConditions", "CFD"))
                .AddItem("U", new NavUrl("FieldU", "BoundaryConditions", "CFD"));

            navigation.AddGroup("Turbines")
                .AddItem("Turbine Types", new NavUrl("TurbineTypes", "Turbines", "CFD"))
                .AddItem("Turbine Array", new NavUrl("TurbineArray", "Turbines", "CFD"));

            navigation.AddGroup("Airfoil & Turbulence")
                .AddItem("Airfoil Properties", new NavUrl("AirfoilProperties", "AirfoilAndTurbulence", "CFD"))
                .AddItem("Turbulence Properties", new NavUrl("TurbulenceProperties", "AirfoilAndTurbulence", "CFD"));

            navigation.AddGroup("System")
                .AddItem("Time", new NavUrl("Time", "SystemControls", "CFD"))
                .AddItem("Schemes", new NavUrl("Schemes", "SystemControls", "CFD"))
                .AddItem("Solution", new NavUrl("Solution", "SystemControls", "CFD"))
                .AddItem("ParallelExecution", new NavUrl("ParallelExecution", "SystemControls", "CFD"));

            navigation.AddGroup("Processing")
                .AddItem("Settings", new NavUrl("Settings", "Processing", "CFD"))
                .AddItem("Simulation", new NavUrl("Simulation", "Processing", "CFD"))
                .AddItem("History", new NavUrl("History", "Processing", "CFD"));

            navigation.AddGroup("Case Management")
                .AddItem("Reset", new NavUrl("Reset", "CaseManagement", "CFD"));

            foreach (var grp in navigation)
            {
                grp.IsActive = grp.Title == SectionTitle;
            }

            ViewBag.SideNav = navigation;
        }

        protected DCase GetCase()
        {
            return ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.CfdCase);
        }

        protected SolverData GetSolverData()
        {
            var dCase = ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.CfdCase);
            var serializer = new XmlSerializer(typeof(SolverData));
            using (var reader = new StringReader(dCase.Model))
            {
                try
                {
                    return (SolverData) serializer.Deserialize(reader);
                }
                catch (Exception e)
                {
                    _log.Error("Failed to deserialize SolverData", e);
                    return SolverData.GetDefaultModel();
                }
            }
        }

        protected void SetCaseJob(Guid id)
        {
            var dCase = GetCase();
            dCase.CurrentJobId = id;
            ctx.SaveChanges();
        }

        protected void SetSolverData(SolverData model)
        {
            var serializer = new XmlSerializer(typeof(SolverData));
            using (var writer = new StringWriter())
            {
                var dCase = ctx.DCases.First(c => c.Owner == User.Identity.Name && c.Name == StandardCases.CfdCase);
                serializer.Serialize(writer, model);
                dCase.Model = writer.ToString();
                writer.Close();
                ctx.SaveChanges();
            }
        }
    }
    // ReSharper restore InconsistentNaming
}
