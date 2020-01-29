using helloserve.com.Database;
using helloserve.com.Domain;
using helloserve.com.Domain.Models;
using helloserve.com.Repository.Mappers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace helloserve.com.Repository
{
    public class ProjectRepository : IProjectDatabaseAdaptor
    {
        readonly helloserveContext _context;

        public ProjectRepository(helloserveContext context)
        {
            _context = context;
        }

        public async Task<Project> Read(string key)
        {
            var result = await _context.Projects
                .Where(p => p.Key == key)
                .SingleOrDefaultAsync();

            return result.Map();
        }

        public async Task<IEnumerable<Project>> ReadAll()
        {
            var result = await _context.Projects
                .Where(p => p.IsActive)
                .OrderBy(p => p.SortOrder)
                .ToListAsync();

            return result.Map();
        }
    }
}
