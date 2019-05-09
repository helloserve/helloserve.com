using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace helloserve.com.Database.Entities
{
    [Table("Blogs")]
    public class Blog
    {
        [Required, Key]
        public string Key { get; set; }

        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
