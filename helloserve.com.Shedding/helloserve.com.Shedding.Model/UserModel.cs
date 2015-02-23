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

        public static UserModel Create(string uniqueNumber, int? notificationPeriod, string pushRegistrationId)
        {
            UserRepository repo = new UserRepository();
            User entity = repo.Add(uniqueNumber, notificationPeriod);
            if (entity == null)
                throw new InvalidOperationException();

            UserModel model = entity.AsModel();            
            model.SetPushRegistrationId(pushRegistrationId);
            return model;
        }

        public static UserModel Get(string phoneNumber)
        {
            UserRepository repo = new UserRepository();
            User entity = repo.Get(phoneNumber);
            return entity.AsModel();
        }

        public void Update(int? notificationPeriod, string pushRegistrationId)
        {
            NotificationPeriod = notificationPeriod;
            UserRepository repo = new UserRepository();
            User entity = repo.Update(Id, UniqueNumber, NotificationPeriod);

            SetPushRegistrationId(pushRegistrationId);
        }

        public void Delete()
        {
            ScheduleRepository scheduleRepo = new ScheduleRepository();
            scheduleRepo.RemoveUserAreas(Id);

            UserRepository userRepo = new UserRepository();
            userRepo.Delete(Id);
        }

        public void SetPushRegistrationId(string registrationId)
        {
            if (string.IsNullOrEmpty(registrationId))
                return;

            UserRepository userRepo = new UserRepository();
            userRepo.AddPushRegistrationId(Id, registrationId);
        }
    }
}
