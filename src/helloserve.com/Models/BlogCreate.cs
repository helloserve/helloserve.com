using System;
using System.ComponentModel.DataAnnotations;

namespace helloserve.com.Models
{
    public class BlogCreate
    {
        public string Key { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }
    }
}
