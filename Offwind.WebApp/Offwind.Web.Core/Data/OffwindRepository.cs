using MongoRepository;

namespace Offwind.Web.Core.Data
{
    public class OffwindRepository<T> : MongoRepository<T>
        where T: IEntity
    {
    }
}
