using helloserve.com.Domain;
using helloserve.com.Repository;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IBlogDatabaseAdaptor, BlogRepository>();
        }
    }
}
