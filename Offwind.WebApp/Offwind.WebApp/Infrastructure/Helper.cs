using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Infrastructure
{
    public static class Helper
    {
        public static IHtmlString ActiveIfCurrent(this WebViewPage page, string action, string ctrl, string area)
        {
            var request = page.Request;
            string areaName = (string)request.RequestContext.RouteData.DataTokens["area"] ?? "";
            string controllerName = (string)request.RequestContext.RouteData.Values["controller"] ?? "";
            string actionName = (string)request.RequestContext.RouteData.Values["action"] ?? "";

            if (area == areaName && ctrl == controllerName && action == actionName)
            {
                return page.Html.Raw("class=\"active\"");
            }
            return page.Html.Raw("");
        }
    }
}