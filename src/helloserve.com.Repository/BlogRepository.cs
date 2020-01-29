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

        public async Task<IEnumerable<BlogListing>> ReadListings(int page, int count, string blogOwnerKey = null, bool publishedOnly = true)
        {
            var source = (
                    from b in _context.Blogs
                    join bo in _context.BlogOwners on b.Key equals bo.BlogKey into bo_G
                    from bo_L in bo_G.DefaultIfEmpty()
                    where (!string.IsNullOrEmpty(blogOwnerKey) && bo_L.OwnerKey == blogOwnerKey) || string.IsNullOrEmpty(blogOwnerKey)
                    select b
                );

            var listing = await source
                .Where(x => x.IsPublished)
                .OrderByDescending(x => x.PublishDate)
                .Skip((page - 1) * count)
                .Take(count)
                .ToListAsync();

            if (!publishedOnly)
            {
                listing.InsertRange(0, await source
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

        public async Task<IEnumerable<Blog>> ReadLatest(int count)
        {
            var source = await (from b in _context.Blogs
                                where b.IsPublished
                                orderby b.PublishDate descending
                                select b)
                .Take(count)
                .ToListAsync();

            return source.Map();
        }
    }
}
