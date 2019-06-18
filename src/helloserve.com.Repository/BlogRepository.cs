using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class BlogRepository : IBlogDatabaseAdaptor
    {
        readonly helloserveContext _context;

        public BlogRepository(helloserveContext context)
        {
            _context = context;
        }

        public async Task<Blog> Read(string title)
        {
            var blog = await GetBlog(title);
            return blog.Map();
        }

        private async Task<Database.Entities.Blog> GetBlog(string title)
        {
            return await _context.Blogs.SingleOrDefaultAsync(x => x.Key == title);
        }

        public Task Save(Blog blog)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BlogListing>> ReadListings()
        {
            return (await _context.Blogs
                .Select(x => new Database.Queries.BlogListing() { Key = x.Key, Title = x.Title })
                .ToListAsync())
                .Map();
        }
    }
}
