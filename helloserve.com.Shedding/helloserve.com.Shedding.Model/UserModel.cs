using helloserve.com.Shedding.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public class UserModel
    {
        public int Id;
        public string UniqueNumber;
        public int? NotificationPeriod;

        public static UserModel Create(string uniqueNumber, int? notificationPeriod)
        {
            UserRepository repo = new UserRepository();
            User entity = repo.Add(uniqueNumber, notificationPeriod);
            if (entity == null)
                throw new InvalidOperationException();

            return entity.AsModel();
        }

        public static UserModel Get(string phoneNumber) {
            UserRepository repo = new UserRepository();
            User entity = repo.Get(phoneNumber);
            return entity.AsModel();
        }

        public void Update()
        {
            UserRepository repo = new UserRepository();
            User entity = repo.Update(Id, UniqueNumber, NotificationPeriod);
        }

        public void Delete()
        {
            ScheduleRepository scheduleRepo = new ScheduleRepository();
            scheduleRepo.RemoveUserAreas(Id);

            UserRepository userRepo = new UserRepository();
            userRepo.Delete(Id);
        }
    }
}
