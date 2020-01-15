using helloserve.com.Adaptors;
using helloserve.com.Auth;
using helloserve.com.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace helloserve.com
{
    public class Startup
    {
        readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options => { options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme; })
                .AddCookie()
                .AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                    googleOptions.CallbackPath = "/auth/signincomplete";
                    googleOptions.Events.OnCreatingTicket = (context) =>
                    {
                        string pictureUrl = context.User.GetProperty("picture").GetString();
                        context.Identity.AddClaim(new Claim("picture", pictureUrl));
                        return Task.CompletedTask;
                    };
                });
            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicy(new List<IAuthorizationRequirement>() { new helloserveAuthorizationRequirement() }, new List<string>() { CookieAuthenticationDefaults.AuthenticationScheme, GoogleDefaults.AuthenticationScheme });
            });
            services.AddScoped<IAuthorizationHandler, helloserveAuthorizationHandler>();

            // Setup HttpClient for server side in a client side compatible fashion
            services.AddScoped<HttpClient>(s =>
            {
                // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.
                var uriHelper = s.GetRequiredService<NavigationManager>();
                return new HttpClient
                {
                    BaseAddress = new Uri(uriHelper.BaseUri)
                };
            });

            services.AddRazorPages();
            services.AddControllers();
            services.AddServerSideBlazor();
#if UITEST
            services.AddSingleton<IBlogServiceAdaptor, MockBlogServiceAdaptor>();
#else
            services.AddTransient<IBlogServiceAdaptor, BlogServiceAdaptor>();
#endif
            services.Configure<DomainOptions>(Configuration.GetSection("Domain"));

            services.AddTransient<IPageState, PageStateModel>();
            services.AddDomainServices();
            services.AddSyndicationServices(Configuration.GetSection("Syndication"));
            services.AddRepositories();
            services.AddhelloserveContext(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("content/{*file}", context =>
                {
                    string file = context.Request.RouteValues["file"].ToString();
                    context.Response.Redirect($"/{file}", true);
                    return Task.CompletedTask;
                });

                endpoints.MapGet("blog/{*title}", context =>
                {
                    string title = context.Request.RouteValues["title"]?.ToString();
                    context.Response.Redirect($"/blogs/{title}", true);
                    return Task.CompletedTask;
                });

                endpoints.MapGet("project/{*title}", context =>
                {
                    string title = context.Request.RouteValues["title"]?.ToString();
                    context.Response.Redirect($"/projects/{title}", true);
                    return Task.CompletedTask;
                });

                endpoints.MapBlazorHub();
                endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
