using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace helloserve.com.Shedding.Api.Models
{
    public class ScheduleDetail
    {
        public int AreaId { get; set; }
        public int CurrentStageId { get; set; }
        public bool IsCurrentlyShedding { get; set; }        
    }
}