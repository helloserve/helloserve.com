using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public static class ScheduleExtentions
    {
        public static UserAreaModel AsModel(this Entities.UserAreaLink entity)
        {
            return new UserAreaModel()
            {
                Id = entity.Id,
                UserId = entity.UserId,
                AreaId = entity.AreaId,
                AuthorityId = entity.AuthorityId
            };
        }

        public static List<UserAreaModel> ToModelList(this IQueryable<Entities.UserAreaLink> collection)
        {
            List<UserAreaModel> list = new List<UserAreaModel>();
            collection.ToList().ForEach(e => { list.Add(e.AsModel()); });
            return list;
        }
    }
}
