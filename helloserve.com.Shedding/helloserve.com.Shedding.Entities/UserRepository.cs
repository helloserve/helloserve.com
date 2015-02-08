using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Entities
{
    public class UserRepository : Base.BaseRepository
    {
        public User Get(string uniqueNumber)
        {
            return Db.Users.SingleOrDefault(u => u.UniqueNumber == uniqueNumber);
        }

        public User Add(string uniqueNumber, int? notificationPeriod)
        {
            User user = Db.Users.SingleOrDefault(u => u.UniqueNumber == uniqueNumber);
            if (user != null)
                return null;

            user = Db.Users.Add(new User());
            user.CreatedDate = DateTime.UtcNow;

            return Save(user, uniqueNumber, notificationPeriod);
        }

        public User Update(int id, string uniqueNumber, int? notificationPeriod)
        {
            User user = Get(uniqueNumber);

            if (user == null)
                return null;

            return Save(user, uniqueNumber, notificationPeriod);
        }

        private User Save(User user, string uniqueNumber, int? notificationPeriod)
        {
            user.UniqueNumber = uniqueNumber;
            user.NotificationPeriod = notificationPeriod;

            Db.SaveChanges();

            return user;
        }

        public void Delete(int userId)
        {
            User user = Db.Users.Single(u => u.Id == userId);
            Db.Users.Remove(user);
            Db.SaveChanges();
        }
    }
}
