using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Offwind.Web.Helpers
{
    public static class FormattingExtensions
    {
        public static string GetDefaultCulture()
        {
            return ConfigurationManager.AppSettings["DefaultCulture"];
        }

        public static string FormatAsMoney(this int n)
        {
            return String.Format(CultureInfo.GetCultureInfo(GetDefaultCulture()), "{0:N0}", n);
        }

        public static string FormatAsMoney(this decimal n)
        {
            return String.Format(CultureInfo.GetCultureInfo(GetDefaultCulture()), "{0:N0}", n);
        }

        public static string FormatAsMoney(this decimal? n)
        {
            return n == null ? "" : FormatAsMoney(n.Value);
        }

        public static string FormatAsLongDate(this DateTime v)
        {
            return String.Format(CultureInfo.GetCultureInfo(GetDefaultCulture()), "{0:dd MMMM yyyy}", v);
        }

        public static string FormatAsLongDate(this DateTime? v)
        {
            return String.Format(CultureInfo.GetCultureInfo(GetDefaultCulture()), "{0:dd MMMM yyyy}", v);
        }

        public static string FormatText(this string input)
        {
            if (input == null) return "";
            if (input.Trim().Length == 0) return "";
            string[] paragraphs = input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var output = new StringBuilder();
            foreach (var paragraph in paragraphs)
            {
                output.AppendFormat("<p>{0}</p>", paragraph);
            }
            return output.ToString();
        }

        public static string FormatAsTime(this DateTime v)
        {
            return String.Format(Thread.CurrentThread.CurrentUICulture, "{0:HH:mm}", v);
        }

        public static string Cutted(this string str)
        {
            const int max = 100;

            if (str == null) return "";
            if (str.Length < max) return str;
            return str.Substring(0, max);
        }
    }
}