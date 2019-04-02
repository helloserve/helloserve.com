using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public class BlogService
    {
        readonly IBlogDatabaseAdaptor _dbAdaptor;
        readonly IBlogSyndicationQueue _blogSyndicationQueue;
        readonly IBlogSyndicationFactory _blogSyndicationFactory;
        readonly BlogSyndicationOptionCollection _syndicationCollection;
        readonly ILogger _logger;

        public BlogService(IBlogDatabaseAdaptor dbAdaptor, IBlogSyndicationQueue blogSyndicationQueue, IBlogSyndicationFactory blogSyndicationFactory, IOptionsMonitor<BlogSyndicationOptionCollection> syndicationCollectionOptions, ILoggerFactory loggerFactory)
        {
            _dbAdaptor = dbAdaptor;
            _blogSyndicationQueue = blogSyndicationQueue;
            _blogSyndicationFactory = blogSyndicationFactory;
            _syndicationCollection = syndicationCollectionOptions.CurrentValue;
            _logger = loggerFactory?.CreateLogger<BlogService>();
        }

        public async Task<Blog> Read(string title)
        {
            return await _dbAdaptor.Read(title);
        }

        public async Task Create(Blog blog)
        {
            Validate(blog);

            blog.Key = AsUrlTitle(blog.Title);

            await _dbAdaptor.Create(blog);
        }

        private void Validate(Blog blog)
        {
            if (string.IsNullOrEmpty(blog.Title))
            {
                throw new ArgumentNullException(nameof(blog.Title));
            }

            blog.PublishDate = blog.PublishDate ?? DateTime.Today;
        }

        private string AsUrlTitle(string title)
        {
            new List<string>() { "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", ".", ",", "?", "'", "\"", "~", "+", "-", ":" }
                .ForEach(x => title = title.Replace(x, string.Empty));

            title = title.Replace(" ", "_");
            title = title.ToLower();
            return title;
        }

        public async Task Publish(string title)
        {
            var blog = await _dbAdaptor.Read(title);

            blog.IsPublished = true;

            Validate(blog);

            //TODO save

            Syndicate(blog);
        }

        private void Syndicate(Blog blog)
        {
            if (_syndicationCollection == null || _syndicationCollection.Count == 0)
            {
                _logger?.LogWarning("No syndications configured for processing");
            }
            else
            {
                _syndicationCollection.ForEach(x =>
                {
                    IBlogSyndication syndication = _blogSyndicationFactory.GetInstance(x.Provider);
                    syndication.Blog = blog;
                    _blogSyndicationQueue.Enqueue(syndication);
                });
            }
        }
    }
}
