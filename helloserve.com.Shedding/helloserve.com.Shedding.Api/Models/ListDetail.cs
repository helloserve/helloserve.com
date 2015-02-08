using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    /// <summary>
    /// A complex type containing the lists of the static data.
    /// </summary>
    public class ListDetail
    {
        /// <summary>
        /// The list of load shedding stages provided.
        /// </summary>
        public List<StageDetail> Stages { get; set; }
        /// <summary>
        /// The list of municipal authorities that determine the various schedules.
        /// </summary>
        public List<AuthorityDetail> Authorities { get; set; }
    }
}