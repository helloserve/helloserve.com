using helloserve.com.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Adaptors
{
    public interface IProjectServiceAdaptor
    {
        Task<IEnumerable<ProjectItemView>> ReadAllActive();
        Task<ProjectView> Read(string key);
    }
}
