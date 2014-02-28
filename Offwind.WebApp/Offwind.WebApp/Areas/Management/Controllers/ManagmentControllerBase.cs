using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Offwind.Web.Core.Extensions;

namespace Offwind.WebApp.Areas.Management.Controllers
{
    public class ManagmentControllerBase : _BaseController
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sw = Stopwatch.StartNew();
            base.OnActionExecuting(filterContext);
            var actions = typeof(ManagementController).GetMethods(BindingFlags.Public | BindingFlags.Instance);
            var actionNames = actions.SelectMany(x => x.GetCustomAttributes(typeof(DescriptionAttribute), false))
                .Cast<DescriptionAttribute>()
                .OrderBy(x => x.Index)
                .Select(x => x.Partition);
            var partitions = new List<ExpandoObject>();

            foreach (var name in actionNames)
            {
                partitions.Add(new
                {
                    Name = name,
                    IsActive = filterContext.ActionDescriptor.ActionName == name
                }.ToExpando());
            }

            ViewBag.Partitions = partitions;
            sw.Stop();
            Debug.WriteLine("Executing 'OnActionExecuting' method. Elapsed: " + sw.Elapsed.TotalMilliseconds + "ms", "ManagementController");
        }
    }
}