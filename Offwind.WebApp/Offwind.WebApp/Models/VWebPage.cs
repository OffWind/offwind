using System.Configuration;
using Offwind.WebApp.Infrastructure.Breadcrumbs;
using Offwind.WebApp.Infrastructure.Navigation;

namespace Offwind.WebApp.Models
{
    public class VWebPage
    {
        public string SiteName { get; set; }
        public string BrowserTitle { get; set; }
        public NavItem<NavUrl> Navigation { get; set; }
        public BreadcrumbsCollection Breadcrumbs { get; protected set; }
        public string CurrentCulture { get; set; }

        public VWebPage()
        {
            SiteName = ConfigurationManager.AppSettings["SiteName"];
            BrowserTitle = ConfigurationManager.AppSettings["SiteName"];
            Breadcrumbs = new BreadcrumbsCollection();
            Navigation = new NavItem<NavUrl>();
        }
    }
}