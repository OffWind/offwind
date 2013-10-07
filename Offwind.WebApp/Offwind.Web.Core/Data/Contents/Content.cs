using System;
using MongoDB.Bson;

namespace Offwind.Web.Core.Data.Contents
{
    public class Content : BaseEntity
    {
        public ObjectId ParentId { get; set; }
        public ObjectId CategoryId { get; set; }
        public int Position { get; set; }
        public ContentType ContentType { get; set; }
        public string Route { get; set; }
        public string RouteTitle { get; set; }
        public string BrowserTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Announce { get; set; }
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsPublished { get; set; }
    }
}
