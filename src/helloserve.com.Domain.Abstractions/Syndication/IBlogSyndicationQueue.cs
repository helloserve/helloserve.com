using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationQueue
    {
        Task EnqueueAsync(IBlogSyndication blogSyndication);
        Task<IBlogSyndication> DequeueAsync();
    }
}
