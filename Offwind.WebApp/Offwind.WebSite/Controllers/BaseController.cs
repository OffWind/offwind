using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.Web.Core;
using Offwind.Web.Core.Breadcrumbs;
using Offwind.Web.Core.Navigation;
using Offwind.Web.Core.News;
using Offwind.Web.Infrastructure;

namespace Offwind.Web.Controllers
{
    [JsonErrorHandler(Order = 1)]
    [HandleError(Order = 2)]
    [OutputCache(NoStore = true, Duration = 0)]
    public abstract class BaseController : Controller
    {
        //protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected readonly OffwindEntities _ctx = new OffwindEntities();
        protected readonly BreadcrumbsList _breadcrumbs = new BreadcrumbsList();
        private readonly NavItemsList _navItems = new NavItemsList();

        protected bool ShowBlockNews { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ShowBlockNews = true;
            ViewBag.BrowserTitle = "Offwind";

            _navItems
                .Add("Home", "/", IsActiveSection(filterContext, "Home"))
                .Add("Downloads", "/Downloads", IsActiveSection(filterContext, "Downloads"))
                .Add("News", "/News", IsActiveSection(filterContext, "News"))
                .Add("Tasks", "/Tasks", IsActiveSection(filterContext, "Tasks"))
                .Add("Calendar", "/Calendar", IsActiveSection(filterContext, "Calendar"))
                .Add("Partners", "/Partners", IsActiveSection(filterContext, "Partners"))
                .Add("Discussions", "/Discussions", IsActiveSection(filterContext, "Discussions"))
                .Add("Library", "/Library", IsActiveSection(filterContext, "Library"))
                .Add("Contacts", "/Contacts", IsActiveSection(filterContext, "Contacts"));

            var activeNavItem = _navItems.FirstOrDefault(i => i.IsActive);
            if (activeNavItem != null)
            {
                _breadcrumbs.Add(activeNavItem.Title, activeNavItem.Url);
            }
            else
            {
            }
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            ViewBag.CurrentCulture = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            ViewBag.Breadcrumbs = _breadcrumbs;
            ViewBag.NavItems = _navItems;
            ViewBag.IsAdmin = Roles.GetRolesForUser(filterContext.HttpContext.User.Identity.Name).Contains(Role.Admin);

            if (ShowBlockNews)
            {
                ViewBag.News = _ctx.Pages
                    .Where(p => p.PageType == PageType.News)
                    .OrderByDescending(p => p.Published)
                    .ToList()
                    .Select(p => new NewsItem(p))
                    .ToList();
            }
        }

        private bool IsActiveSection(ActionExecutingContext filterContext, string name)
        {
            name = name.ToLowerInvariant();
            var controllerName = filterContext.Controller.GetType().Name.Replace("Controller", "").ToLowerInvariant();
            return controllerName == name;
        }

        private bool IsActiveSection2(ActionExecutingContext filterContext, string name)
        {
            name = name.ToLowerInvariant();
            var controllerName = filterContext.Controller.GetType().Name.Replace("Controller", "").ToLowerInvariant();
            var isActiveSection = controllerName == name;

            if (!isActiveSection && controllerName == "pages" && filterContext.ActionParameters.ContainsKey("type"))
            {
                var pageType = filterContext.ActionParameters["type"].ToString().ToLowerInvariant();
                isActiveSection = pageType == name;
            }
            return isActiveSection;
        }
    }
}
