using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    /// <summary>
    /// Entity that defines user specific settings
    /// </summary>
    public class UserDetail
    {
        /// <summary>
        /// The period lead time (in minutes) for notification. If this value is omitted (null), the user will be notified at the time the event occurs.
        /// </summary>
        public int? NotificationPeriod { get; set; }

        /// <summary>
        /// Update method for user.
        /// </summary>
        /// <param name="session">The session containing the UserModel instance.</param>
        public void Update(ShedSession session)
        {
            session.User.NotificationPeriod = NotificationPeriod;
            session.User.Update();
            session.Set();
        }
    }
}