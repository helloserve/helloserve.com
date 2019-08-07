namespace helloserve.com.Domain.Models
{
    public class Blog : BlogListing
    {
        public bool IsPublished { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
}
