using System;
using System.Linq;
using System.Reflection;
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
            string areaName = (string)request.RequestContext.RouteData.DataTokens["area"] ?? "";
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
            foreach (var key in html.ViewData.ModelState.Keys)
            {
                foreach (var err in html.ViewData.ModelState[key].Errors)
                    retVal += "<li>" + html.Encode(err.ErrorMessage) + "</li>";
            }
            retVal += "</ul></div>";
            return html.Raw(retVal);
        }

        public static string AnchorUrl(this string txt)
        {
            if (txt == null || txt.Trim().Length <= 0) return "";
            if (txt.StartsWith("http://")) return txt;
            if (txt.StartsWith("https://")) return txt;
            if (txt.StartsWith("ftp://")) return txt;
            return "http://" + txt;
        }

        public static void InitEmptyStrings(this object target)
        {
            var type = target.GetType();
            foreach (PropertyInfo info in type.GetProperties())
            {
                if (info.PropertyType != typeof(string)) continue;
                var currentValue = info.GetValue(target, null);
                if (currentValue != null) continue;
                info.SetValue(target, "", null);
            }
        }

        public static IHtmlString BlockTitle(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Block);
            return new HtmlString(block.Title);
        }

        public static IHtmlString BlockContent(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Block);
            return new HtmlString(block.Content);
        }
        public static IHtmlString CarouselTitle(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Carousel);
            return new HtmlString(block.Title);
        }

        public static IHtmlString CarouselContent(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Carousel);
            return new HtmlString(block.Content);
        }
        public static IHtmlString CarouselCaption(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Carousel);
            return new HtmlString(block.Title + block.Content);
        }
        public static IHtmlString CarouselItem(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Carousel);

            var item = new TagBuilder("div");
            item.AddCssClass("active item");
            var caption = new TagBuilder("div");
            var title = new TagBuilder("h2");
            title.InnerHtml = block.Title;

            caption.AddCssClass("carousel-caption");
            caption.InnerHtml = title + block.Content;

            var img = new TagBuilder("img");
            img.MergeAttribute("src", block.Image);
            item.InnerHtml = img.ToString(TagRenderMode.Normal) + caption.ToString(TagRenderMode.Normal);

            return MvcHtmlString.Create(item.ToString(TagRenderMode.Normal));
        }

        public static IHtmlString CarouselImage(this HtmlHelper html, string name)
        {
            var block = BlockModel(html, name, ContentType.Carousel);
            var builder = new TagBuilder("img");
            builder.MergeAttribute("src",block.Image);

            return MvcHtmlString.Create(builder.ToString(TagRenderMode.Normal));
        }
        private static BlockModel BlockModel(HtmlHelper html, string name, ContentType type)
        {
            BlockModel block;
            if (html.ViewData.ContainsKey(name))
            {
                block = (BlockModel) html.ViewData[name];
            }
            else
            {
                block = Models.BlockModel.GetBlock(name, type);
                html.ViewData[name] = block;
            }
            return block;
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