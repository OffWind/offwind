using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using Offwind.Web.Core;
using Offwind.WebApp.Infrastructure;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected OffwindEntities _ctx = new OffwindEntities();

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.Version = WebConfigurationManager.AppSettings["AppVersion"];
            ViewBag.IsAdmin = AccountsHelper.IsAdmin(filterContext.HttpContext.User.Identity.Name);
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
