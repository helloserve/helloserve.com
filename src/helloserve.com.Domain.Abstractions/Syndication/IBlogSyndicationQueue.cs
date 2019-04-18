namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationQueue
    {
        void Enqueue(IBlogSyndication blogSyndication);
        IBlogSyndication Dequeue();
    }
}
