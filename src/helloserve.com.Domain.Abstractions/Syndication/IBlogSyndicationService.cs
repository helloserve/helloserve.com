using helloserve.com.Domain.Models;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationService
    {
        Task Syndicate(Blog blog);
    }
}
