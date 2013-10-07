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
            navigation.AddGroup("Mesoscale Wind Database", isActive: controller == "MesoWind")
                .AddItem("Overview", new NavUrl("Index", "MesoWind", "EngineeringTools"))
                .AddItem("Database", new NavUrl("Database", "MesoWind", "EngineeringTools"));
            /*
                .AddItem("Current Data", new NavUrl("CurrentData", "MesoWind", "EngineeringTools"))
                .AddItem("Velocity Freq.", new NavUrl("VelocityFreq", "MesoWind", "EngineeringTools"))
                .AddItem("Wind Rose", new NavUrl("WindRose", "MesoWind", "EngineeringTools"));
            */
            navigation.AddGroup("Wake Simulation", isActive: controller == "WakeSimulation")
                .AddItem("General Properties", new NavUrl("GeneralProperties", "WakeSimulation", "EngineeringTools"))
                .AddItem("Turbine Coordinates", new NavUrl("TurbineCoordinates", "WakeSimulation", "EngineeringTools"))
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation", "EngineeringTools"))
                .AddItem("Post-processing", new NavUrl("PostProcessing", "WakeSimulation", "EngineeringTools"));

            navigation.AddGroup("Wind Farm Control", isActive: controller == "WakeSimulation2")
                .AddItem("Overview", new NavUrl("Index", "WakeSimulation2", "EngineeringTools"))
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation2", "EngineeringTools"))
                .AddItem("Results", new NavUrl("Results", "WakeSimulation2", "EngineeringTools"));

            navigation.AddGroup("Wind Farm Power Calculator", isActive: controller == "WindWave")
                .AddItem("Overview", new NavUrl("Index", "WindWave", "EngineeringTools"))
                .AddItem("Input Data", new NavUrl("InputData", "WindWave", "EngineeringTools"))
                .AddItem("Power Output", new NavUrl("PowerOutput", "WindWave", "EngineeringTools"))
                .AddItem("Power Output Adv.", new NavUrl("PowerOutputAdvanced", "WindWave", "EngineeringTools"));

            //navigation.AddGroup("Wind Farm Control", isActive: controller == "WindFarm")
            //    .AddItem("Input Data", new NavUrl("InputData", "WindFarm", "EngineeringTools"))
            //    .AddItem("Simulation", new NavUrl("Simulation", "WindFarm", "EngineeringTools"));

            ViewBag.SideNav = navigation;
        }
    }
}
