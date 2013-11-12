using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.Web.Core;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;
using WebGrease.Css.Extensions;

namespace Offwind.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected OffwindEntities _ctx = new OffwindEntities();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Version = WebConfigurationManager.AppSettings["AppVersion"];
            ViewBag.IsAdmin = AccountsHelper.IsAdmin(filterContext.HttpContext.User.Identity.Name);

            var mainMenu = new Dictionary<string, NavItem<string>>();
            foreach (var cat in _ctx.DContentCategories)
            {
                var navGroup = new NavItem<string>();
                navGroup.Title = cat.Title;
                navGroup.Url = cat.Route;
                foreach (var routeItem in _ctx.VRouteItems.Where(ri => ri.CategoryId == cat.Id).OrderBy(ri => ri.Position))
                {
                    navGroup.AddItem(routeItem.RouteTitle, routeItem.Route);
                }
                mainMenu[cat.Name] = navGroup;
            }

            ViewBag.MainMenu = mainMenu;

            _ctx.UpdateLastOnline(filterContext.HttpContext.User.Identity.Name, DateTime.UtcNow);
            base.OnActionExecuted(filterContext);
        }

        protected BlockModel GetBlock(string name)
        {
            var ctBlock = ContentType.Block.ToString();
            var content = _ctx.DContents.FirstOrDefault(c => c.TypeId == ctBlock && c.Name == name);
            var block = new BlockModel();
            if (content != null)
            {
                block.Name = content.Name;
                block.Title = content.Title;
                block.Content = content.Content;
            }
            return block;
        }
    }
}
