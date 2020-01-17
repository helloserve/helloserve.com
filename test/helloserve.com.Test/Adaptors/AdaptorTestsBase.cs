using Microsoft.Extensions.DependencyInjection;
using System;

namespace helloserve.com.Test.Adaptors
{
    public abstract class AdaptorTestsBase<TAdaptor>
    {
        private IServiceCollection _services = new ServiceCollection();
        public IServiceProvider ServiceProvider { get; private set; }

        public TAdaptor Adaptor => ServiceProvider.GetService<TAdaptor>();

        protected AdaptorTestsBase()
        {
            ServiceProvider = ConfigureInternal(_services).BuildServiceProvider();
        }

        private IServiceCollection ConfigureInternal (IServiceCollection services)
        {
            services.AddAdaptors();
            return Configure(services);
        }

        protected virtual IServiceCollection Configure(IServiceCollection services)
        {
            return services;
        }
    }
}
