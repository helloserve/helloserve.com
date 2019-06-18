using System;

namespace helloserve.com.Domain.Models
{
    public class Blog : BlogListing
    {
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public string Content { get; set; }
    }
}
