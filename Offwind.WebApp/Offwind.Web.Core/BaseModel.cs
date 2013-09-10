using EmitMapper;

namespace Offwind.Web.Core
{
    public class BaseModel<TView, TData>
        where TView : new()
    {
        public static TView MapFromDb(TData db)
        {
            var model = new TView();
            MapFromDb(model, db);
            return model;
        }

        public static void MapFromDb(TView model, TData db)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TData, TView>();
            mapper.Map(db, model);
        }

        public static void MapToDb(TData db, TView model)
        {
            var mapper = ObjectMapperManager.DefaultInstance.GetMapper<TView, TData>();
            mapper.Map(model, db);
        }
    }
}
