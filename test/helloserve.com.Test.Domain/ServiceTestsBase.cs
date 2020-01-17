using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace helloserve.com.Test.Domain
{
    public abstract class ServiceTestsBase<TService>
    {
        readonly IServiceCollection _services = new ServiceCollection();
        public IServiceProvider ServiceProvider;

        public TService Service => ServiceProvider.GetService<TService>();

        protected ServiceTestsBase()
        {
            ServiceProvider = ConfigureInternal(_services).BuildServiceProvider();
        }

        private IServiceCollection ConfigureInternal(IServiceCollection services)
        {
            services
                .AddDomainServices()
                .AddSyndicationServices(new ConfigurationBuilder().Build());
            return Configure(services);
        }

        protected virtual IServiceCollection Configure(IServiceCollection services)
        {
            return services;
        }
    }
}
