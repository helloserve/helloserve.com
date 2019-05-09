using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class BlogRepository : IBlogDatabaseAdaptor
    {
        IhelloserveContext _context;

        public BlogRepository(IhelloserveContext context)
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
    }
}
