namespace Offwind.WebApp.Infrastructure.Breadcrumbs
{
    public sealed class BreadcrumbsItem
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public bool IsLast { get; set; }
    }
}