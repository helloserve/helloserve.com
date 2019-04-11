using helloserve.com.Domain.Models;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndication
    {
        Blog Blog { get; set; }
        BlogSyndicationOption Config { get; set; }
    }
}
