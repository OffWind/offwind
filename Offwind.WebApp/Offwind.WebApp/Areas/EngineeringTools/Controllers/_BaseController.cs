using System.Web.Mvc;
using NUnit.Framework;
using Offwind.Web.Core;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    [Authorize(Roles = SystemRole.User)]
    public class _BaseController : BaseController
    {

        protected OffwindEntities _ctx = new OffwindEntities();
        protected bool _noNavigation = false;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            if (!_noNavigation)
            {
                var controllerName = filterContext.RequestContext.RouteData.Values["controller"].ToString();
                InitNavigation(controllerName);
            }
        }

        private void InitNavigation(string controller)
        {
            var navigation = new NavItem<NavUrl>();
            navigation.AddGroup("Meso. Wind DB", isActive: controller == "MesoWind")
                .AddItem("Database", new NavUrl("Database", "MesoWind", "EngineeringTools"))
                .AddItem("Help", new NavUrl("Index", "EngMesoWind", "Help"), openInNewWindow: true);
            /*
                .AddItem("Current Data", new NavUrl("CurrentData", "MesoWind", "EngineeringTools"))
                .AddItem("Velocity Freq.", new NavUrl("VelocityFreq", "MesoWind", "EngineeringTools"))
                .AddItem("Wind Rose", new NavUrl("WindRose", "MesoWind", "EngineeringTools"));
            */
            navigation.AddGroup("Power Calculator", isActive: controller == "WindWave")
                .AddItem("Input Data", new NavUrl("InputData", "WindWave", "EngineeringTools"))
                .AddItem("Power Output", new NavUrl("PowerOutput", "WindWave", "EngineeringTools"))
                .AddItem("Power Output Adv.", new NavUrl("PowerOutputAdvanced", "WindWave", "EngineeringTools"))
                .AddItem("Help", new NavUrl("Index", "EngPowerCalc", "Help"), openInNewWindow: true);

            navigation.AddGroup("Wake Simulation", isActive: controller == "WakeSimulation")
                .AddItem("General Properties", new NavUrl("GeneralProperties", "WakeSimulation", "EngineeringTools"))
                .AddItem("Turbine Coordinates", new NavUrl("TurbineCoordinates", "WakeSimulation", "EngineeringTools"))
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation", "EngineeringTools"))
                .AddItem("Post-processing", new NavUrl("PostProcessing", "WakeSimulation", "EngineeringTools"))
                .AddItem("Help", new NavUrl("Index", "EngWindSim", "Help"), openInNewWindow: true);

            navigation.AddGroup("Wind Farm Control", isActive: controller == "WakeSimulation2")
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation2", "EngineeringTools"))
                .AddItem("Results", new NavUrl("Results", "WakeSimulation2", "EngineeringTools"))
                .AddItem("Help", new NavUrl("Index", "EngWindSim", "Help"), openInNewWindow: true);

            navigation.AddGroup("Wind Farm Control-N", isActive: controller == "WakeSimulation2New")
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation2New", "EngineeringTools"))
                .AddItem("Results", new NavUrl("Results", "WakeSimulation2New", "EngineeringTools"))
                .AddItem("Nowcasting", new NavUrl("Nowcasting", "WakeSimulation2New", "EngineeringTools"))
                .AddItem("Help", new NavUrl("Index", "EngWindFarmControlNew", "Help"), openInNewWindow: true);

            //navigation.AddGroup("Wind Farm Control", isActive: controller == "WindFarm")
            //    .AddItem("Input Data", new NavUrl("InputData", "WindFarm", "EngineeringTools"))
            //    .AddItem("Simulation", new NavUrl("Simulation", "WindFarm", "EngineeringTools"));

            ViewBag.SideNav = navigation;
        }
    }
}
