using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace helloserve.com.Auth
{
    public class helloserveAuthorizationHandler : AuthorizationHandler<helloserveAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, helloserveAuthorizationRequirement requirement)
        {
            string email = context.User.FindFirst(ClaimTypes.Email)?.Value;
            if (!string.IsNullOrEmpty(email))
            {
                if (email != "<hardcoded email address>")
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
