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
    
    public partial class Area
    {
        public Area()
        {
            this.Schedules = new HashSet<Schedule>();
            this.ScheduleCalendars = new HashSet<ScheduleCalendar>();
            this.UserAreas = new HashSet<UserArea>();
        }
    
        public int Id { get; set; }
        public Nullable<int> AuthorityId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    
        public virtual Authority Authority { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<ScheduleCalendar> ScheduleCalendars { get; set; }
        public virtual ICollection<UserArea> UserAreas { get; set; }
    }
}
