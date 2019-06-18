using System.Collections.Generic;
using System.Threading.Tasks;
using helloserve.com.Domain.Models;

namespace helloserve.com.Domain
{
    public interface IBlogDatabaseAdaptor
    {
        Task<Blog> Read(string title);
        Task Save(Blog blog);
        Task<IEnumerable<BlogListing>> GetListings();
    }
}
