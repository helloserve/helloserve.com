namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationFactory
    {
        IBlogSyndication GetInstance(string provider);
    }
}
