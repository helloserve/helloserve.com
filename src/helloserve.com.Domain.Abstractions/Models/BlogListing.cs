using System;

namespace helloserve.com.Domain.Models
{
    public class BlogListing
    {
        public string Title { get; set; }
        public string Key { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
