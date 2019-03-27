using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationQueue
    {
        Task Enqueue(IBlogSyndication blogSyndication);
        Task<IBlogSyndication> Dequeue();
    }
}
