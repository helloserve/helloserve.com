using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    /// <summary>
    /// Complete detail of a specific user/area link.
    /// </summary>
    public class UserAreaDetail
    {
        /// <summary>
        /// The id of the link.
        /// </summary>
        public int Id;
        /// <summary>
        /// The user Id.
        /// </summary>
        public int UserId;
        /// <summary>
        /// The area id.
        /// </summary>
        public int AreaId;
        /// <summary>
        /// The area detail object.
        /// </summary>
        public AreaDetail Area;
        /// <summary>
        /// The authority id that controls this area.
        /// </summary>
        public int? AuthorityId;
    }
}