using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
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

        public async Task Save(Blog blog)
        {
            Database.Entities.Blog dbBlog = (await _context.Blogs.Where(x => x.Key == blog.Key).ToListAsync()).SingleOrDefault();

            if (dbBlog == null)
            {
                dbBlog = new Database.Entities.Blog
                {
                    Key = blog.Key
                };
                _context.Blogs.Add(dbBlog);
            }
            blog.MapOnto(dbBlog);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BlogListing>> ReadListings(int page, int count, bool publishedOnly = true)
        {
            var listing = await _context.Blogs
                .Where(x => x.IsPublished)
                .OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();

            if (!publishedOnly)
            {
                listing.InsertRange(0, await _context.Blogs
                    .Where(x => !x.IsPublished)
                    .ToListAsync());
            }

            return listing
                .Select(x => new Database.Queries.BlogListing()
                {
                    Key = x.Key,
                    Title = x.Title,
                    PublishDate = x.PublishDate,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                })
                .Map();
        }
    }
}
