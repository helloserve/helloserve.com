//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace helloserve.com.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class ForumTopic
    {
        public ForumTopic()
        {
            this.ForumPosts = new HashSet<ForumPost>();
        }
    
        public int ForumTopicID { get; set; }
        public int ForumCategoryID { get; set; }
        public int UserID { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public bool Sticky { get; set; }
        public bool Locked { get; set; }
    
        public virtual ForumCategory ForumCategory { get; set; }
        public virtual ICollection<ForumPost> ForumPosts { get; set; }
        public virtual User User { get; set; }
    }
}
