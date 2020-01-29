using System.ComponentModel.DataAnnotations;

namespace helloserve.com.Database.Entities
{
    public class Project
    {
        [Required, Key]
        public string Key { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ComponentPage { get; set; }
        public bool IsActive { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public int SortOrder { get; set; }
    }
}
