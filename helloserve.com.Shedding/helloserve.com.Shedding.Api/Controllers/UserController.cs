using helloserve.com.Shedding.Api.Controllers.Base;
using helloserve.com.Shedding.Api.Filters;
using helloserve.com.Shedding.Api.Models;
using helloserve.com.Shedding.Model;
using log4net;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace helloserve.com.Shedding.Api.Controllers
{
    /// <summary>
    /// Manage the user account
    /// </summary>
    [RoutePrefix("user")]
    public class UserController : BaseApiController
    {
        ILog _log = LogManager.GetLogger(typeof(UserController));

        /// <summary>
        /// Register a user.
        /// </summary>
        /// <param name="uniqueNumber">A unique string that identifies the user or the device.</param>
        /// <param name="data">An object detailing specific user criteria, like the notification period.</param>
        /// <remarks>
        /// No header is required to be passed with this method. This method only needs to be called once.
        /// </remarks>
        /// <returns>
        /// 204 - No Content, 206 - Partial Content : Data not supplied, 409 - Conflict : user identification string already exists
        /// </returns>
        [Route("{uniqueNumber}")]
        public void Put(string uniqueNumber, [FromBody]UserDetail data)
        {
            if (data == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.PartialContent);

            try
            {
                UserModel user = UserModel.Create(uniqueNumber, data.NotificationPeriod, data.PushNotificationId);
            }
            catch (InvalidOperationException)
            {
                _log.Warn(string.Format("User for {0} already exist", uniqueNumber));
                throw new HttpResponseException(System.Net.HttpStatusCode.Conflict);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error occured creating new user for {0}", uniqueNumber), ex);
                throw;
            }
        }

        /// <summary>
        /// Update the user's details.
        /// </summary>
        /// <param name="data">A data object containing the new values to use.</param>
        /// <returns>
        /// 204 - No Content, 206 - Partial Content : Data not supplied
        /// </returns>
        [SessionFilter]
        [Route("")]
        public void Post([FromBody]UserDetail data)
        {
            if (data == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.PartialContent);

            try
            {
                ShedSession session = ShedSession.GetSession(Request.Headers);
                data.Update(session);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error occured updating user"), ex);
                throw;
            }
        }

        /// <summary>
        /// Gets the user's specific settings.
        /// </summary>
        /// <returns>
        /// Object detailing the settings.
        /// </returns>
        [SessionFilter]
        [Route("")]
        public UserDetail Get()
        {
            try
            {
                return Session.User.AsDetail();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error occured getting user"), ex);
                throw;
            }
        }

        /// <summary>
        /// Remove a use from the database.
        /// </summary>
        [SessionFilter]
        [Route("")]
        public void Delete()
        {
            try
            {
                Session.User.Delete();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error deleting user"), ex);
                throw;
            }
        }
    }
}
