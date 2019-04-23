using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public class BlogSyndicationQueue : IBlogSyndicationQueue
    {
        readonly ConcurrentQueue<IBlogSyndication> _queue = new ConcurrentQueue<IBlogSyndication>();

        public async Task<IBlogSyndication> DequeueAsync()
        {
            return await Task.Run(() =>
            {
                if (_queue.TryDequeue(out IBlogSyndication blogSyndication))
                {
                    return blogSyndication;
                }

                return null;
            });
        }

        public async Task EnqueueAsync(IBlogSyndication blogSyndication)
        {
            await Task.Run(() => _queue.Enqueue(blogSyndication));
        }
    }
}
