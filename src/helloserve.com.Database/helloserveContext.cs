using helloserve.com.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace helloserve.com.Database
{
    public class helloserveContext : DbContext
    {
        public helloserveContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogOwner>()
                .HasKey(x => new { x.BlogKey, x.OwnerKey });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogOwner> BlogOwners { get; set; }
        public DbSet<Project> Projects { get; set; }
    }
}
