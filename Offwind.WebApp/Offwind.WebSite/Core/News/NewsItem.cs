using System;

namespace Offwind.Web.Core.News
{
    public sealed class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Published { get; set; }
        public string Announce { get; set; }

        public NewsItem(Page page)
        {
            Id = page.Id;
            Title = page.Title;
            Announce = page.Announce;
            Published = page.Published;
        }
    }
}