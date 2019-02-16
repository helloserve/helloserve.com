using helloserve.com.Domain.Models;
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
    }
}
