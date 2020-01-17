using helloserve.com.Domain;
using helloserve.com.Domain.Syndication;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IProjectService, ProjectService>();
        }

        public static IServiceCollection AddSyndicationServices(this IServiceCollection services, IConfiguration syndicationConfiguration)
        {
            services.Configure<BlogSyndicationOptionCollection>(syndicationConfiguration);

            return services
                .AddTransient<IBlogSyndicationService, BlogSyndicationService>()
                .AddSingleton<IBlogSyndicationQueue, BlogSyndicationQueue>()
                .AddTransient(s => new Mock<IBlogSyndicationFactory>().Object);
        }
    }
}
