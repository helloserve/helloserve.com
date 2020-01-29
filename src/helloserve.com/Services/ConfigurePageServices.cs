using helloserve.com.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigurePageServices
    {
        public static IServiceCollection AddPageServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IIndexPageLoader, IndexPageLoader>();
        }
    }
}
