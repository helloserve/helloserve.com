using helloserve.com.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace helloserve.com.Database
{
    public class helloserveContext : DbContext, IhelloserveContext
    {
        public helloserveContext(DbContextOptions options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
    }
}
