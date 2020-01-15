using System.ComponentModel.DataAnnotations;

namespace helloserve.com.Database.Entities
{
    public class BlogOwner
    {
        [Required]
        public string BlogKey { get; set; }
        [Required]
        public string OwnerKey { get; set; }
        public string OwnerType { get; set; }
    }
}
