using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Offwind.Web.Core;
using Offwind.WebApp.Infrastructure.Breadcrumbs;

namespace Offwind.WebApp.Models
{
    public class VWebPage
    {
        public string SiteName { get; set; }
        public string H1 { get; set; }
        public string BrowserTitle { get; set; }
        public BreadcrumbsCollection Breadcrumbs { get; protected set; }
        public string CurrentCulture { get; set; }
        public string ReturnTo { get; set; }
        public bool CanEdit { get; set; }

        public VWebPage()
        {
            SiteName = ConfigurationManager.AppSettings["SiteName"];
            BrowserTitle = ConfigurationManager.AppSettings["SiteName"];
            Breadcrumbs = new BreadcrumbsCollection();
        }
    }
}