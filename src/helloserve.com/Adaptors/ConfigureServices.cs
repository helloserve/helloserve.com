using helloserve.com.Adaptors;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddAdaptors(this IServiceCollection services)
        {
            return services
#if UITEST
                .AddSingleton<IBlogServiceAdaptor, MockBlogServiceAdaptor>()
                .AddSingleton<IProjectServiceAdaptor, MockProjectServiceAdaptor>();
#else
                .AddTransient<IBlogServiceAdaptor, BlogServiceAdaptor>()
                .AddTransient<IProjectServiceAdaptor, ProjectServiceAdaptor>();
#endif
        }
    }
}
