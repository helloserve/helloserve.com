using System.Collections.Generic;

namespace helloserve.com.Models
{
    public class ProjectView
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string ComponentPage { get; set; }

        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Type { get; set; }

        public IEnumerable<MetaCollection> MetaCollection { get; set; }
    }
}
