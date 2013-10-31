using System;
using Offwind.WebApp.Models;

namespace Offwind.WebApp.Areas.Management.Tools
{
    public static class ContentTypeHelpers
    {
        public static string S(this ContentType pt)
        {
            return pt.ToString();
        }

        public static ContentType S(this string pts)
        {
            try
            {
                return (ContentType)Enum.Parse(typeof(ContentType), pts);
            }
            catch (ArgumentNullException)
            {
                return ContentType.Undefined;
            }
        }
    }
}