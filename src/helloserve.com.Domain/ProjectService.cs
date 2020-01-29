using helloserve.com.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace helloserve.com.Domain
{
    public class ProjectService : IProjectService
    {
        readonly IProjectDatabaseAdaptor _repository;

        public ProjectService(IProjectDatabaseAdaptor repository)
        {
            _repository = repository;
        }

        public async Task<Project> Read(string key)
        {
            return await _repository.Read(key);
        }

        public async Task<IEnumerable<Project>> ReadAllActive()
        {
            return await _repository.ReadAll();
        }
    }
}
