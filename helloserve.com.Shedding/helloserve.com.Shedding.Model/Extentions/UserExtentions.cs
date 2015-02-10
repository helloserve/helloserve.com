using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace helloserve.com.Shedding.Model
{
    public static class UserExtentions
    {
        public static UserModel AsModel(this Entities.User entity)
        {
            return new UserModel()
            {
                Id = entity.Id,
                UniqueNumber = entity.UniqueNumber,
                NotificationPeriod = entity.NotificationPeriod,
                PushNotificationId = entity.PushNotificationId
            };
        }
    }
}
