using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication;
using helloserve.com.Domain.Syndication.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public class BlogService : IBlogService
    {
        readonly IBlogDatabaseAdaptor _dbAdaptor;
        readonly IBlogSyndicationService _blogSyndicationService;

        public BlogService(IBlogDatabaseAdaptor dbAdaptor, IBlogSyndicationService blogSyndicationService)
        {
            _dbAdaptor = dbAdaptor;
            _blogSyndicationService = blogSyndicationService;
        }

        public async Task<Blog> Read(string title)
        {
            return await _dbAdaptor.Read(title);
        }

        public async Task CreateUpdate(Blog blog)
        {
            ValidateSave(blog);

            blog.Key ??= AsUrlTitle(blog.Title);

            await _dbAdaptor.Save(blog);
        }

        private void ValidateSave(Blog blog)
        {
            if (string.IsNullOrEmpty(blog.Title))
            {
                throw new ArgumentNullException(nameof(blog.Title));
            }
        }

        private void ValidatePublish(Blog blog)
        {
            ValidateSave(blog);

            blog.PublishDate ??= DateTime.Today;
        }

        private string AsUrlTitle(string title)
        {
            new List<string>() { "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", ".", ",", "?", "'", "\"", "~", "+", ":" }
                .ForEach(x => title = title.Replace(x, string.Empty));

            title = title.Replace(" ", "-");
            title = title.ToLower();
            return title;
        }

        public async Task Publish(string title, IEnumerable<SyndicationText> syndicationTexts)
        {
            var blog = await _dbAdaptor.Read(title);

            blog.IsPublished = true;

            ValidatePublish(blog);

            await _dbAdaptor.Save(blog);

            await _blogSyndicationService.SyndicateAsync(blog, syndicationTexts);
        }

        public async Task<IEnumerable<BlogListing>> ReadAll(int page, int count, bool isAuthenticated)
        {
            return await _dbAdaptor.ReadListings(page, count, publishedOnly: !isAuthenticated);
        }
    }
}
