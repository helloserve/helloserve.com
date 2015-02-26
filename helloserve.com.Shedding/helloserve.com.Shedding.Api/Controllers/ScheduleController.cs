using helloserve.com.Shedding.Api.Controllers.Base;
using helloserve.com.Shedding.Api.Filters;
using helloserve.com.Shedding.Api.Models;
using helloserve.com.Shedding.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace helloserve.com.Shedding.Api.Controllers
{
    /// <summary>
    /// This controller handles all the schedule related calls
    /// </summary>
    [RoutePrefix("schedule")]
    public class ScheduleController : BaseApiController
    {
        ILog _log = LogManager.GetLogger(typeof(ScheduleController));

        /// <summary>
        /// Link an area to a user for notification purposes.
        /// </summary>
        /// <param name="area">The id of the area.</param>
        [SessionFilter]
        [Route("{area:int}")]
        public void Post(int area)
        {
            try
            {
                UserAreaModel.Create(Session.User.Id, area);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error linking user to area {0}", area), ex);
                throw;
            }
        }

        /// <summary>
        /// Remove an area from a user's notification list.
        /// </summary>
        /// <param name="area">The id of the area.</param>
        [SessionFilter]
        [Route("{area:int}")]
        public void Delete(int area)
        {
            try
            {
                UserAreaModel.Remove(Session.User.Id, area);
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error un-linking user to area {0}", area), ex);
                throw;
            }            
        }

        /// <summary>
        /// Retrieve a list of all the areas linked to the user for notification.
        /// </summary>
        /// <returns>List object of areas.</returns>
        [SessionFilter]
        [Route("")]
        public List<UserAreaDetail> Get()
        {
            try
            {
                return UserAreaModel.Get(Session.User.Id).ToDetailList();
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error getting area for user"), ex);
                throw;
            }
        }

        /// <summary>
        /// Retrieve the schedule data for a specific area for the prevailing load shedding stage.
        /// </summary>
        /// <param name="area">The area id.</param>
        /// <returns>Object detailing related schedule values; 405 - User not linked to requested area.</returns>
        [SessionFilter]
        [Route("{area:int}")]
        public ScheduleDetail Get(int area)
        {
            try
            {
                return ScheduleModel.GetSchedule(Session.User.Id, area, null).AsDetail();
            }
            catch (InvalidOperationException ioex)
            {
                _log.Error(string.Format("Invalid Op - user {0} not linked to area area {1}, prevailing stage ", Session.User.Id, area), ioex);
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error getting schedule for area {0}", area), ex);
                throw;
            }
        }

        /// <summary>
        /// Retrieve the schedule data for a specific area, for a specific load shedding stage.
        /// </summary>
        /// <param name="area">The area id.</param>
        /// <param name="stage">The stage id to view schedule data for a different stage than what is currently in effect.</param>
        /// <returns>Object detailing related schedule values; 405 - User not linked to requested area.</returns>
        [SessionFilter]
        [Route("{area:int}/{stage:int}")]
        public ScheduleDetail Get(int area, int stage)
        {
            try
            {
                return ScheduleModel.GetSchedule(Session.User.Id, area, stage).AsDetail();
            }
            catch (InvalidOperationException ioex)
            {
                _log.Error(string.Format("Invalid Op - user {0} not linked to area area {1}, stage {2}", Session.User.Id, area, stage), ioex);
                throw new HttpResponseException(new HttpResponseMessage(System.Net.HttpStatusCode.MethodNotAllowed));
            }
            catch (Exception ex)
            {
                _log.Error(string.Format("Error getting schedule for area {0}, stage {1}", area, stage), ex);
                throw;
            }
        }
    }
}