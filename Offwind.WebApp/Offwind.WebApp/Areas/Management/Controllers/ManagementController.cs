using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Offwind.Web.Core;
using Offwind.Web.Core.Data;
using Offwind.Web.Core.Extensions;
using Offwind.WebApp.Areas.Management.Models;
using Offwind.WebApp.Models.Account;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class ManagementController : _BaseController
    {
        [Description("Home",1)]
        public ActionResult Home()
        {
            IEnumerable<DContent> content = _ctx.DContents.Where(x => x.DContentCategory.Name == CategoryNames.Home && x.TypeId == "Block").ToList();

            return View(content);
        }

        [Description("News", 2)]
        public ActionResult News()
        {
            return View();
        }

        [Description("About", 3)]
        public ActionResult About()
        {
            IEnumerable<DContent> content = _ctx.DContents.Where(x => x.DContentCategory.Name == CategoryNames.About).ToList();
            return View(content);
        }

        [Description("Help", 4)]
        public ActionResult Help()
        {
            return View();
        }

        //[Description("Users", 5)]
        //public ActionResult Users()
        //{
        //    ViewBag.Partition = "About";
        //    var model = new VUsersHome();
        //    model.Users = _ctx.DVUserProfiles.OrderBy(x => x.FirstName).Select(VUserProfile.MapFromDb).ToList();
        //    return View(model);
        //}

        public ActionResult Details(Guid id)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id==id);
            if (item != null)
            {
                ViewModel.Header = item.Title;
                ViewModel.Content = item.Content;
                ViewModel.Id = item.Id;
            }
            
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Guid id,string header,string content)
        {
            var item = _ctx.DContents.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                item.Title = header;
                item.Content = content;
                _ctx.DContents.ApplyCurrentValues(item);
                _ctx.SaveChanges();
                if(this.GetAllActionNames().Contains(item.DContentCategory.Name))
                    return RedirectToAction(item.DContentCategory.Name);
            }
            return RedirectToAction("Home");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sw = Stopwatch.StartNew();
            base.OnActionExecuting(filterContext);
            var actions = GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var actionNames = actions.SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), false))
                .Cast<DescriptionAttribute>()
                .OrderBy(x=>x.Index)
                .Select(x=>x.Partition);
            var partitions = new List<ExpandoObject>();

            foreach (var name in actionNames)
            {
                partitions.Add(new
                {
                    Name=name,
                    IsActive = filterContext.ActionDescriptor.ActionName == name
                }.ToExpando());
            }
           
            ViewBag.Partitions = partitions;
            sw.Stop();
        }
    }
}
