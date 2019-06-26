using System;
using System.ComponentModel.DataAnnotations;

namespace helloserve.com.Models
{
    public class BlogCreate
    {
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
