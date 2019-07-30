using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace helloserve.com.Auth
{
    public class helloserveAuthorizationHandler : AuthorizationHandler<helloserveAuthorizationRequirement>
    {
        readonly List<string> users;

        public helloserveAuthorizationHandler(IConfiguration configuration)
        {
            users = configuration.GetSection("Users").Get<List<string>>();
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, helloserveAuthorizationRequirement requirement)
        {
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                if (!users.Contains(email))
                {
                    context.Fail();
                }
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class helloserveAuthorizationRequirement : IAuthorizationRequirement
    {

    }

    public class helloserveAuthorizationHandlerDefaults
    {
        public const string AuthorizationPolicy = "helloserve";
    }
}
