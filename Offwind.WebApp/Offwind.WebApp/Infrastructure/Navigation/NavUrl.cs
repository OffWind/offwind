using System.Web.Routing;

namespace Offwind.WebApp.Infrastructure.Navigation
{
    public class NavUrl
    {
        public const string TypeParam = "type";

        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Type { get; set; }

        public NavUrl()
        {
        }

        public NavUrl(string a, string c, string ar)
        {
            Action = a;
            Controller = c;
            Area = ar;
        }

        public NavUrl(string a, string c, string ar, string t)
        {
            Action = a;
            Controller = c;
            Area = ar;
            Type = t;
        }
        public RouteValueDictionary RouteValues
        {
            get
            {
                var rv = new RouteValueDictionary();
                rv["area"] = Area;
                if (Type != null && Type.Trim().Length > 0)
                {
                    rv[TypeParam] = Type;
                }
                return rv;
            }
        }
    }
}