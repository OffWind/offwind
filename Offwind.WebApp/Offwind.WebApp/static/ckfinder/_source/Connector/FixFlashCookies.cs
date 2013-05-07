using System;
using System.Reflection;
using System.Web;

namespace CKFinder.Utils
{
    /// <summary> 
    /// Fix for the Flash Player Cookie bug in Non-IE browsers.
    /// </summary> 
    public class FixFlashCookiesModule : IHttpModule
    {
        void context_BeginRequest(object sender, EventArgs e)
        {
            HttpCookie cookie;
            string cookie_prefix = "ckfcookie_";
            string cookie_name;
            string cookie_value;
            string command = HttpContext.Current.Request.QueryString["command"];

            if (command == null || command != "FileUpload")
                return;

            try
            {
                foreach (string formKey in HttpContext.Current.Request.Form.AllKeys)
                {
                    if (formKey.StartsWith(cookie_prefix))
                    {
                        cookie_name = formKey.Replace(cookie_prefix, "");
                        cookie_value = HttpContext.Current.Request.Form[formKey];

                        cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
                        if (cookie == null)
                        {
                            cookie = new HttpCookie(cookie_name);
                            HttpContext.Current.Request.Cookies.Add(cookie);
                        }
                        cookie.Value = cookie_value;
                        HttpContext.Current.Request.Cookies.Set(cookie);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void Dispose()
        { }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += context_BeginRequest;
        }
    }
}
