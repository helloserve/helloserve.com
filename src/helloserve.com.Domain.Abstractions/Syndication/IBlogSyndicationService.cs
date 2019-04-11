using helloserve.com.Domain.Models;
using helloserve.com.Domain.Syndication.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain.Syndication
{
    public interface IBlogSyndicationService
    {
        Task Syndicate(Blog blog, IEnumerable<SyndicationText> syndicationTexts);
    }
}
