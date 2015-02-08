using helloserve.com.Shedding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class UserAreaModel
    {
        public int Id;
        public int UserId;
        public int AreaId;
        public int? AuthorityId;

        public static UserAreaModel Create(int userId, int areaId)
        {
            ScheduleRepository repo = new ScheduleRepository();
            UserAreaLink entity = repo.AddUserArea(userId, areaId);

            return entity.AsModel();
        }

        public static List<UserAreaModel> Get(int userId)
        {
            ScheduleRepository repo = new ScheduleRepository();
            return repo.GetUserAreaLinks(userId).ToModelList();
        }

        public static void Remove(int userId, int areaId)
        {
            ScheduleRepository repo = new ScheduleRepository();
            repo.RemoveUserArea(userId, areaId);
        }
    }
}
