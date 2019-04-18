using System.Collections.Concurrent;

namespace helloserve.com.Domain.Syndication
{
    public class BlogSyndicationQueue : IBlogSyndicationQueue
    {
        readonly ConcurrentQueue<IBlogSyndication> _queue = new ConcurrentQueue<IBlogSyndication>();

        public IBlogSyndication Dequeue()
        {
            if (_queue.TryDequeue(out IBlogSyndication blogSyndication))
            {
                return blogSyndication;
            }

            return null;
        }

        public void Enqueue(IBlogSyndication blogSyndication)
        {
            _queue.Enqueue(blogSyndication);
        }
    }
}
