using helloserve.com.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> ReadAllActive();
        Task<Project> Read(string key);
    }
}
