using System;
using MongoDB.Bson.Serialization.Attributes;
using MongoRepository;

namespace Offwind.Web.Core.Data
{
    [BsonIgnoreExtraElements]
    public class BaseEntity : IEntity
    {
        public string Id { get; set; }
        public Guid SiteId { get; set; }
    }
}
