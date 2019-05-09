using helloserve.com.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace helloserve.com.Database
{
    public interface IhelloserveContext
    {
        DbSet<Blog> Blogs { get; set; }
    }
}
