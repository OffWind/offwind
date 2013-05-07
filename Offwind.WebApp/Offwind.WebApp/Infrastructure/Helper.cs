using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Offwind.WebApp.Infrastructure.Navigation;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Infrastructure
{
    public static class Helper
    {
        public static IHtmlString ActiveIfCurrent(this WebViewPage page, NavUrl navUrl)
        {
            var request = page.Request;
            string areaName = (string)request.RequestContext.RouteData.Values["area"] ?? "";
            string controllerName = (string)request.RequestContext.RouteData.Values["controller"] ?? "";
            string actionName = (string)request.RequestContext.RouteData.Values["action"] ?? "";

            var isTypeOk = true;
            if (navUrl.Type != null && navUrl.Type.Trim().Length > 0)
            {
                string type = request.Params[NavUrl.TypeParam] ?? "";
                isTypeOk = navUrl.Type == type;
            }

            if (navUrl.Area == areaName && navUrl.Controller == controllerName && navUrl.Action == actionName && isTypeOk)
            {
                return page.Html.Raw("class=\"active\"");
            }
            return page.Html.Raw("");
        }

        public static IHtmlString ActiveIfCurrent(this WebViewPage page, string url)
        {
            var request = page.Request;

            if (url == request.RawUrl)
            {
                return page.Html.Raw("class=\"current\"");
            }
            return page.Html.Raw("");
        }

        public static string GenerateUrl(this WebViewPage page, NavUrl navUrl)
        {
            return page.Url.Action(navUrl.Action, navUrl.Controller, navUrl.RouteValues);
        }

        public static IHtmlString BootstrapValidationSummary(this HtmlHelper html)
        {
            string retVal = "";
            if (html.ViewData.ModelState.IsValid)
                return html.Raw("");
            retVal += "<div class='alert alert-error'>";
            retVal += "<ul>";
            foreach (string key in html.ViewData.ModelState.Keys)
            {
                foreach (ModelError err in html.ViewData.ModelState[key].Errors)
                    retVal += "<li>" + html.Encode(err.ErrorMessage) + "</li>";
            }
            retVal += "</ul></div>";
            return html.Raw(retVal);
        }
        /*
        public static BlockModel GetBlock(string route)
        {
            using (var ctx = new OffwindEntities())
            {
                var ctBlock = ContentType.Block.ToString();
                var content = ctx.DContents.FirstOrDefault(c => c.TypeId == ctBlock && c.Route == route);
                var block = new BlockModel();
                if (content != null)
                {
                    block.Title = content.Title;
                    block.Content = content.Content;
                }
                return block;
            }
        }
        */
    }
}