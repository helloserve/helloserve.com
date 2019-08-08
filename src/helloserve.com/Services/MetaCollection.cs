using System.Collections.Generic;

namespace helloserve.com
{
    public class MetaCollection
    {
        public string ProviderSource { get; set; }
        public List<(string, string)> MetaTags { get; set; }
    }
}
