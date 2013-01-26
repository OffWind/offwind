namespace Offwind.WebApp.Infrastructure.Navigation
{
    public class NavUrl
    {
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }

        public NavUrl()
        {
        }

        public NavUrl(string a, string c, string ar)
        {
            Action = a;
            Controller = c;
            Area = ar;
        }
    }
}