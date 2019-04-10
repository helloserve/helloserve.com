using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public class BlogSyndicationService
    {
        readonly IBlogSyndicationQueue _blogSyndicationQueue;
        readonly IBlogSyndicationFactory _blogSyndicationFactory;
        readonly BlogSyndicationOptionCollection _syndicationCollection;
        readonly ILogger _logger;

        public BlogSyndicationService(IBlogSyndicationQueue blogSyndicationQueue, IBlogSyndicationFactory blogSyndicationFactory, IOptionsMonitor<BlogSyndicationOptionCollection> syndicationCollectionOptions, ILoggerFactory loggerFactory)
        {
            _blogSyndicationQueue = blogSyndicationQueue;
            _blogSyndicationFactory = blogSyndicationFactory;
            _syndicationCollection = syndicationCollectionOptions.CurrentValue;
            _logger = loggerFactory?.CreateLogger<BlogSyndicationService>();
        }

        public async Task Syndicate(Blog blog)
        {
            if (_syndicationCollection == null || _syndicationCollection.Count == 0)
            {
                _logger?.LogWarning("No syndications configured for processing");
            }
            else
            {
                _syndicationCollection.ForEach(async x =>
                {
                    IBlogSyndication syndication = _blogSyndicationFactory.GetInstance(x.Provider);
                    syndication.Blog = blog;
                    await _blogSyndicationQueue.Enqueue(syndication);
                });
            }
        }
    }
}
