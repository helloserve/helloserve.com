using System;

namespace helloserve.com.Models
{
    public class BlogView
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
}
