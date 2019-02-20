using helloserve.com.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public class BlogService
    {
        readonly IBlogDatabaseAdaptor _dbAdaptor;

        public BlogService(IBlogDatabaseAdaptor dbAdaptor)
        {
            _dbAdaptor = dbAdaptor;
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
    }
}
