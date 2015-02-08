using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    /// <summary>
    /// Details the shedding schedule of the specific area.
    /// </summary>
    public class ScheduleDetail
    {
        /// <summary>
        /// The area id.
        /// </summary>
        public int AreaId { get; set; }
        /// <summary>
        /// The stage id. Either what is currently in effect, or for the value requested.
        /// </summary>
        public int StageId { get; set; }
        /// <summary>
        /// If the area is currently being load shedded. This value is relative to the requested stage, meaning it will be false for stage 3 if the area is currently being shedded on stage 2.
        /// </summary>
        public bool IsCurrentlyShedding { get; set; }
        /// <summary>
        /// Will be populated of IsCurrentlyShedding is true. Contains details relating to the current ongoing load shedding only.
        /// </summary>
        public SheddingDetail CurrentShedding { get; set; }
        /// <summary>
        /// A ascending list of future scheduled shedding on the selected(or currently in effect) stage.
        /// </summary>
        public List<SheddingDetail> FutureShedding { get; set; }
    }
}