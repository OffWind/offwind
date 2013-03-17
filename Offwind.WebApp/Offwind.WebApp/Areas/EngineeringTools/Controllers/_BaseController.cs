using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Offwind.WebApp.Infrastructure.Navigation;

namespace Offwind.WebApp.Areas.EngineeringTools.Controllers
{
    public class _BaseController : Controller
    {
        protected string _currentGroup;

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            InitNavigation();
        }

        private void InitNavigation()
        {
            var navigation = new NavItem<NavUrl>();
            navigation.AddGroup("Meso Wind")
                .AddItem("Database", new NavUrl("Database", "MesoWind", "EngineeringTools"))
                .AddItem("Current Data", new NavUrl("CurrentData", "MesoWind", "EngineeringTools"))
                .AddItem("Velocity Freq.", new NavUrl("VelocityFreq", "MesoWind", "EngineeringTools"))
                .AddItem("Wind Rose", new NavUrl("WindRose", "MesoWind", "EngineeringTools"));

            navigation.AddGroup("Wake Simulation")
                .AddItem("General Properties", new NavUrl("GeneralProperties", "WakeSimulation", "EngineeringTools"))
                .AddItem("Turbine Coordinates", new NavUrl("TurbineCoordinates", "WakeSimulation", "EngineeringTools"))
                .AddItem("Simulation", new NavUrl("Simulation", "WakeSimulation", "EngineeringTools"))
                .AddItem("Post-processing", new NavUrl("PostProcessing", "WakeSimulation", "EngineeringTools"));

            navigation.AddGroup("Wind Wave Power")
                .AddItem("Input Data", new NavUrl("InputData", "WindWave", "EngineeringTools"))
                .AddItem("Power Output", new NavUrl("PowerOutput", "WindWave", "EngineeringTools"))
                .AddItem("Power Output Adv.", new NavUrl("PowerOutputAdvanced", "WindWave", "EngineeringTools"));

            navigation.AddGroup("Wind Farm")
                .AddItem("Input Data", new NavUrl("InputData", "WindFarm", "EngineeringTools"))
                .AddItem("Simulation", new NavUrl("Simulation", "WindFarm", "EngineeringTools"));

            foreach (var grp in navigation)
            {
                grp.IsActive = grp.Title == _currentGroup;
            }
            ViewBag.SideNav = navigation;
        }
    }
}
