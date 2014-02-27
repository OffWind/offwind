using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;

namespace Offwind.Web.Core.Extensions
{
    public static class ControllerExtensions
    {
        public static IEnumerable<string> GetAllActionNames(this Controller controller)
        {
            var methodInfos = controller.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
            return methodInfos.Select(x => x.Name);
        }
        public static IEnumerable<MethodInfo> GetAllActions(this Controller controller)
        {
            return controller.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
        }
    }
}
