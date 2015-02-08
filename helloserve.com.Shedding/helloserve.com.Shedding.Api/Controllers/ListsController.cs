using helloserve.com.Shedding.Api.Controllers.Base;
using helloserve.com.Shedding.Api.Filters;
using helloserve.com.Shedding.Api.Models;
using helloserve.com.Shedding.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace helloserve.com.Shedding.Api.Controllers
{
    /// <summary>
    /// Simple controller that returns the basic lists of static data.
    /// </summary>
    [RoutePrefix("list")]
    public class ListsController : BaseApiController
    {
        /// <summary>
        /// Get the main list data.
        /// </summary>
        /// <returns>Two lists detailing the different load shedding stages and all the schedule authorities (excluding Eskom itself).</returns>
        [SessionFilter]
        [Route("")]
        public ListDetail Get()
        {
            return new ListDetail()
            {
                Authorities = ListModel.GetAuthorities().ToDetailList(),
                Stages = ListModel.GetStages().ToDetailList()
            };
        }

        /// <summary>
        /// Get the list of areas controlled by an authority.
        /// </summary>
        /// <param name="authority">The authority Id value.</param>
        /// <returns>A list detailing each area.</returns>
        [SessionFilter]
        [Route("{authority:int}")]
        public List<AreaDetail> Get(int authority)
        {
            return ListModel.GetAreas(authority).ToDetailList();
        }
    }
}
