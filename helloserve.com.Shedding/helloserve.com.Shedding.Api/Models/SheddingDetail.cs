using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public class SheddingDetail
    {
        /// <summary>
        /// The area id being shedded.
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// The stage id under effect or as requested.
        /// </summary>
        public int StageId { get; set; }
        /// <summary>
        /// The start date and time (UTC) of the shedding.
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// The stop date and time (UTC) of the shedding.
        /// </summary>
        public DateTime StopTime { get; set; }
    }
}