using System.Web;

namespace Offwind.Web.Infrastructure
{
    public static class WebStringExtensions
    {
        public static string HtmlEncode(this string input)
        {
            if (input == null) return null;
            return HttpUtility.HtmlEncode(input);
        }

        public static string HtmlDecode(this string input)
        {
            if (input == null) return null;
            return HttpUtility.HtmlDecode(input);
        }
    }
}