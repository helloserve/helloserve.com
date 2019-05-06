using System;

namespace helloserve.com.Domain.Models
{
    public class Blog
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public string Content { get; set; }
    }
}
