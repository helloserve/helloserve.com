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
    
    public partial class User
    {
        public User()
        {
            this.UserAreas = new HashSet<UserArea>();
        }
    
        public int Id { get; set; }
        public string UniqueNumber { get; set; }
        public Nullable<int> NotificationPeriod { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string PushNotificationId { get; set; }
    
        public virtual ICollection<UserArea> UserAreas { get; set; }
    }
}
