using helloserve.com.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace helloserve.com.Test.Repository
{
    /// <summary>
    /// Base class for setting up repository tests quickly. Use Options and ArrangeDatabase to configure your test's in memory database.
    /// </summary>
    /// <typeparam name="TRepository"></typeparam>
    public abstract class RepositoryTests<TRepository>
    {
        readonly IServiceCollection _services = new ServiceCollection();
        public IServiceProvider ServiceProvider { get; private set; }
        public DbContextOptions<helloserveContext> Options;

        public TRepository Repository => ServiceProvider.GetService<TRepository>();

        protected RepositoryTests()
        {
            ServiceProvider = ConfigureInternal(_services).BuildServiceProvider();            
        }

        private IServiceCollection ConfigureInternal(IServiceCollection services)
        {
            services.AddTransient(sp => new helloserveContext(Options));
            services.AddRepositories();
            return Configure(services);
        }

        protected virtual IServiceCollection Configure(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        /// Configures a test's options and sets up InMemory. Specify a unique name to not cross paths with other tests.
        /// </summary>
        /// <param name="name"></param>
        protected virtual void ArrangeDatabase(string name)
        {
            Options = new DbContextOptionsBuilder<helloserveContext>()
                .UseInMemoryDatabase(name)
                .Options;
        }
    }
}
