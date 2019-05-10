using helloserve.com.Domain;
using helloserve.com.Domain.Syndication;
using Moq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            return services
                .AddTransient<IBlogService, BlogService>()
                .AddTransient<IBlogSyndicationService, BlogSyndicationService>()
                .AddSingleton<IBlogSyndicationQueue, BlogSyndicationQueue>()
                .AddTransient(s => new Mock<IBlogSyndicationFactory>().Object);
        }
    }
}
