using helloserve.com.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Adaptors
{
    public interface IBlogServiceAdaptor
    {
        Task<BlogView> Read(string title);
        Task<IEnumerable<BlogItemView>> ReadAll(int page, int count, string ownerKey, bool isAuthenticated);
        Task<BlogCreate> Edit(string title);
        Task Submit(BlogCreate blog);
        Task Publish(string title);
        Task<IEnumerable<BlogView>> ReadLatest(int count);
    }
}
