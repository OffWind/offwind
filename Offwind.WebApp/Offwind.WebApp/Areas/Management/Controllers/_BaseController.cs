using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.WebApp.Areas.Management.Tools;
using Offwind.WebApp.Controllers;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class _BaseController : BaseController
    {
        protected OffwindEntities _ctx = new OffwindEntities();
        protected dynamic ViewModel = new ExpandoObject();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");

            ViewBag.Title = "Management";

            // Get current controller metadata
            var cNameAttr = filterContext.Controller.GetType().GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute);
            if (cNameAttr != null)
            {
                var a = (DisplayNameAttribute)cNameAttr;
                if (a.DisplayName != null && a.DisplayName.Trim().Length > 0)
                    ViewBag.Title += " - " + a.DisplayName;
            }

            // Get current action metadata
            var aNameAttr = filterContext.ActionDescriptor.GetCustomAttributes(true).FirstOrDefault(a => a is DisplayNameAttribute);
            if (aNameAttr != null)
            {
                var a = (DisplayNameAttribute)aNameAttr;
                if (a.DisplayName != null && a.DisplayName.Trim().Length > 0)
                    ViewBag.Title += ": " + a.DisplayName;
            }
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var navigation = new NavItem<NavUrl>();

            navigation.AddGroup("Content")
                .AddItem("Block", new NavUrl("Index", "ContentMgmt", "ControlPanel", ContentType.Block.S()))
                .AddItem("Blog", new NavUrl("Index", "ContentMgmt", "ControlPanel", ContentType.Blog.S()))
                .AddItem("Page", new NavUrl("Index", "ContentMgmt", "ControlPanel", ContentType.Page.S()));

            navigation.AddGroup("Users")
                .AddItem("Users", new NavUrl("Index", "Users", "ControlPanel"));

            //ViewBag.SideNav = navigation;

            ViewBag.IsAdmin = AccountsHelper.IsAdmin(filterContext.HttpContext.User.Identity.Name);

            base.OnActionExecuted(filterContext);
        }
        
    }
}