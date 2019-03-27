using System.Collections.Generic;

namespace helloserve.com.Domain.Syndication
{
    public class BlogSyndicationOption
    {
        public string Provider { get; set; }
        public string ApiKey { get; set; }
    }

    public class BlogSyndicationOptionCollection : List<BlogSyndicationOption>
    {

    }
}
