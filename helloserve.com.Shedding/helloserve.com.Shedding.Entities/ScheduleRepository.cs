using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Entities
{
    public class ScheduleRepository : Base.BaseRepository
    {
        public UserAreaLink AddUserArea(int userId, int areaId)
        {
            UserArea link = new UserArea()
            {
                UserId = userId,
                AreaId = areaId
            };

            Db.UserAreas.Add(link);
            Db.SaveChanges();

            return GetUserAreaLink(userId, areaId);
        }

        public void RemoveUserArea(int userId, int areaId)
        {
            UserArea link = Db.UserAreas.Single(l => l.UserId == userId && l.AreaId == areaId);
            Db.UserAreas.Remove(link);
            Db.SaveChanges();
        }

        public void RemoveUserAreas(int userId)
        {
            var links = Db.UserAreas.Where(l => l.UserId == userId);
            Db.UserAreas.RemoveRange(links);
            Db.SaveChanges();
        }

        private UserAreaLink GetUserAreaLink(int userId, int areaId)
        {
            return (from ua in Db.UserAreas
                    join ar in Db.Areas on ua.AreaId equals ar.Id
                    where ua.UserId == userId && ua.AreaId == areaId
                    select new UserAreaLink()
                    {
                        Id = ua.Id,
                        UserId = ua.UserId,
                        AreaId = ar.Id,
                        AuthorityId = ar.AuthorityId
                    }).Single();
        }

        public IQueryable<UserAreaLink> GetUserAreaLinks(int userId)
        {
            return from ua in Db.UserAreas
                   join ar in Db.Areas on ua.AreaId equals ar.Id
                   where ua.UserId == userId
                   select new UserAreaLink()
                   {
                       Id = ua.Id,
                       UserId = ua.UserId,
                       AreaId = ar.Id,
                       AuthorityId = ar.AuthorityId
                   };
        }
    }
}
