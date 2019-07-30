using helloserve.com.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace helloserve.com.Test.Auth
{
    [TestClass]
    public class helloserveAuthorizationTests
    {
        [DataTestMethod]
        [DataRow("test@test.com", true, false)]
        [DataRow("<a different email address>", false, true)]
        public async Task HandleAsync_Verify(string email, bool succeeded, bool failed)
        {
            //arrange
            IConfiguration configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>("Users:0", "test@test.com") })
                .Build();
            helloserveAuthorizationHandler handler = new helloserveAuthorizationHandler(configuration);
            IEnumerable<ClaimsIdentity> identities = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, email)
                })
            };
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identities);
            IEnumerable<IAuthorizationRequirement> requirements = new List<IAuthorizationRequirement>()
            {
                new helloserveAuthorizationRequirement()
            };
            AuthorizationHandlerContext context = new AuthorizationHandlerContext(requirements, claimsPrincipal, null);

            //act
            await handler.HandleAsync(context);

            //assert
            Assert.AreEqual(succeeded, context.HasSucceeded);
            Assert.AreEqual(failed, context.HasFailed);
        }
    }
}
