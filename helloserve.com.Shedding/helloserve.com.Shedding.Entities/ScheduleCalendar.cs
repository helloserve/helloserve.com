//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace helloserve.com.Shedding.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ScheduleCalendar
    {
        public int Id { get; set; }
        public int AreaId { get; set; }
        public int SheddingStageId { get; set; }
        public Nullable<int> ScheduleId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
    
        public virtual Area Area { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual SheddingStage SheddingStage { get; set; }
    }
}
