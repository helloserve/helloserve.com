using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public static class ListExtentions
    {
        public static StageModel AsModel(this Entities.SheddingStage entity)
        {
            return new StageModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static AuthorityModel AsModel(this Entities.Authority entity)
        {
            return new AuthorityModel()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public static AreaModel AsModel(this Entities.Area entity)
        {
            return new AreaModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Code = entity.Code
            };
        }

        public static List<StageModel> ToModelList(this IQueryable<Entities.SheddingStage> collection)
        {
            List<StageModel> list = new List<StageModel>();
            collection.ToList().ForEach(e => { list.Add(e.AsModel()); });
            return list;
        }

        public static List<AuthorityModel> ToModelList(this IQueryable<Entities.Authority> collection)
        {
            List<AuthorityModel> list = new List<AuthorityModel>();
            collection.ToList().ForEach(e => { list.Add(e.AsModel()); });
            return list;
        }

        public static List<AreaModel> ToModelList(this IQueryable<Entities.Area> collection)
        {
            List<AreaModel> list = new List<AreaModel>();
            collection.ToList().ForEach(e => { list.Add(e.AsModel()); });
            return list;
        }
    }
}
