using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public class BlogSyndicationService : IBlogSyndicationService
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

        public async Task Syndicate(Blog blog, IEnumerable<SyndicationText> syndicationTexts)
        {
            if (_syndicationCollection == null || _syndicationCollection.Count == 0)
            {
                _logger?.LogWarning("No syndications configured for processing");
            }
            else
            {
                IEnumerable<Task> syndicationTasks = syndicationTexts.Select(t => SyndicateItem(t, blog));
                await Task.WhenAll(syndicationTasks);
            }
        }

        private async Task SyndicateItem(SyndicationText text, Blog blog)
        {
            await Task.Run(() =>
            {
                try
                {
                    BlogSyndicationOption config = _syndicationCollection.Single(o => o.Provider == text.Name);
                    IBlogSyndication syndication = _blogSyndicationFactory.GetInstance(config.Provider);
                    syndication.Blog = blog;
                    syndication.Config = config;
                    _blogSyndicationQueue.Enqueue(syndication);
                }
                catch (InvalidOperationException ex)
                {
                    _logger?.LogError(ex, $"Config for provider '{text.Name}' not valid.");
                }
            });
        }
    }
}
