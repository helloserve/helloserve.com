using System;

namespace helloserve.com.Database.Queries
{
    public class BlogListing
    {
        public string Key { get; set; }
        public string Title { get; set; }
        public DateTime? PublishDate { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
