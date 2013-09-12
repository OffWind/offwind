using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.Web.Core;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Partners.Controllers
{
    [Authorize(Roles = "Partner")]
    public class _BaseController : BaseController
    {
        protected string _currentGroup;
        protected OffwindEntities _ctx = new OffwindEntities();
        protected bool _noNavigation = false;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!_noNavigation)
            {
                InitNavigation();
            }
        }

        private void InitNavigation()
        {
            var navigation = new NavItem<NavUrl>();
            //navigation.AddGroup("")
            //    .AddItem("Overview", new NavUrl("Index", "MesoWind", "EngineeringTools"))
            //    .AddItem("Database", new NavUrl("Database", "MesoWind", "EngineeringTools"));
            ///*
            //    .AddItem("Current Data", new NavUrl("CurrentData", "MesoWind", "EngineeringTools"))
            //    .AddItem("Velocity Freq.", new NavUrl("VelocityFreq", "MesoWind", "EngineeringTools"))
            //    .AddItem("Wind Rose", new NavUrl("WindRose", "MesoWind", "EngineeringTools"));
            //*/
            //navigation.AddGroup("Wake Simulation")
            //    .AddItem("General Properties", new NavUrl("GeneralProperties", "WakeSimulation", "EngineeringTools"))
            //    .AddItem("Turbine Coordinates", new NavUrl("TurbineCoordinates", "WakeSimulation", "EngineeringTools"))
            //    .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation", "EngineeringTools"))
            //    .AddItem("Post-processing", new NavUrl("PostProcessing", "WakeSimulation", "EngineeringTools"));

            //navigation.AddGroup("Wind Wave Power")
            //    .AddItem("Overview", new NavUrl("Index", "WindWave", "EngineeringTools"))
            //    .AddItem("Input Data", new NavUrl("InputData", "WindWave", "EngineeringTools"))
            //    .AddItem("Power Output", new NavUrl("PowerOutput", "WindWave", "EngineeringTools"))
            //    .AddItem("Power Output Adv.", new NavUrl("PowerOutputAdvanced", "WindWave", "EngineeringTools"));

            //navigation.AddGroup("Wind Farm Control")
            //    .AddItem("Input Data", new NavUrl("InputData", "WindFarm", "EngineeringTools"))
            //    .AddItem("Simulation", new NavUrl("Simulation", "WindFarm", "EngineeringTools"));

            //foreach (var grp in navigation)
            //{
            //    grp.IsActive = grp.Title == _currentGroup;
            //}
            ViewBag.SideNav = navigation;
        }
    }
}
