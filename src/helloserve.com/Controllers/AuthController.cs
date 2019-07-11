using helloserve.com.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace helloserve.com.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet("auth/user")]
        public UserState GetUser()
        {
            return new UserState
            {
                IsLoggedIn = User.Identity.IsAuthenticated,
                DisplayName = User.Identity.Name,
                PictureUrl = User.FindFirst("picture")?.Value
            };
        }

        [HttpGet("auth/signin")]
        public async Task SignIn()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet("auth/signincompleted")]
        public IActionResult SignInCompleted()
        {
            var userState = GetUser();
            return Content($@"
                <script>
                    window.opener.onLoginPopupFinished({JsonConvert.SerializeObject(userState)});
                    window.close();
                </script>", "text/html");
        }

        [HttpPut("auth/signout")]
        public async Task<UserState> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return UserState.LoggedOutState;
        }
    }
}
