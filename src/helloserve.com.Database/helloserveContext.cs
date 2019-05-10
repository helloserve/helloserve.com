using helloserve.com.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace helloserve.com.Database
{
    public class helloserveContext : DbContext, IhelloserveContext
    {
        public helloserveContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }
    }
}
