using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public interface IBlogService
    {
        Task<Blog> Read(string title);
        Task Create(Blog blog);
        Task Publish(string title, IEnumerable<SyndicationText> syndicationTexts);
        Task<IEnumerable<BlogListing>> ReadAll();
    }
}
