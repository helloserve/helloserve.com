using helloserve.com.Domain.Models;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndication
    {
        Blog Blog { get; set; }
        BlogSyndicationOption Config { get; set; }
        Task ProcessAsync();
    }
}
