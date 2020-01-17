using helloserve.com.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public interface IProjectDatabaseAdaptor
    {
        Task<IEnumerable<Project>> ReadAll();
    }
}
