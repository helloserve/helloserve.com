using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        public void SaveScheduleCalendarTimes(List<ScheduleCalendar> items)
        {
            string qryTemplate = "IF EXISTS (SELECT 1 FROM ScheduleCalendar WHERE AreaId = {0} AND SheddingStageId = {1} AND StartTime = '{2}') UPDATE ScheduleCalendar SET EndTime = {3} WHERE AreaId = {0} AND SheddingStageId = {1} AND StartTime = '{2}' ELSE INSERT INTO ScheduleCalendar (Date, AreaId, SheddingStageId, ScheduleId, StartTime, EndTime) VALUES (CONVERT(DATE, '{2}'), {0}, {1}, {4}, '{2}', '{3}')";

            StringBuilder qryBuilder = new StringBuilder();
            foreach (var item in items)
            {
                qryBuilder.AppendLine(string.Format(qryTemplate, item.AreaId, item.SheddingStageId, item.StartTime.ToString("yyyy/MM/dd HH:mm"), item.EndTime.ToString("yyyy/MM/dd HH:mm"), item.ScheduleId));
            }

            using (SqlConnection connection = new SqlConnection(Db.Database.Connection.ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(qryBuilder.ToString(), connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
