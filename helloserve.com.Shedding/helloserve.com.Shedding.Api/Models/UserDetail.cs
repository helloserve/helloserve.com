using helloserve.com.Shedding.Model;
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
        /// The unqiue platform registration string for notification services (APN, GNS etc)
        /// </summary>
        public string PushRegistrationId { get; set; }

        /// <summary>
        /// Createse a user and adds it to the session variable
        /// </summary>
        /// <param name="session">The session instance to use</param>
        /// <param name="uniqueNumber">The unique identifier for the user</param>
        public void Create(ShedSession session, string uniqueNumber)
        {
            session.User = UserModel.Create(uniqueNumber, NotificationPeriod, PushRegistrationId);
            session.Set();
        }

        /// <summary>
        /// Update method for user.
        /// </summary>
        /// <param name="session">The session containing the UserModel instance.</param>
        public void Update(ShedSession session)
        {            
            session.User.Update(NotificationPeriod, PushRegistrationId);
            session.Set();
        }
    }
}