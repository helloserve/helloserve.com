using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace helloserve.com.Database
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddhelloserveContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<helloserveContext>(options => {
                options
                    .UseSqlServer(configuration.GetConnectionString("helloserveContext"), sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(2);
                    });
            });
            return services;
        }
    }
}
