using AlperAslanApps.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using AlperAslanApps.Core.Contract.Models;

namespace AlperAslanApps.AspNetCore.Services
{
    public class Authenticator : IAuthenticator
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Authenticator(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void SignIn(AuthenticationUser user)
        {
            var context = _httpContextAccessor.HttpContext;

            var userIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            userIdentity.AddClaim(new Claim("SecurityStamp", user.SecurityStamp.ToString()));

            var principal = new ClaimsPrincipal(userIdentity);

            var signInTask = context.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true
                }
            );

            signInTask.GetAwaiter().GetResult();

            context.User = principal;
        }

        public void SignOut()
        {
            var context = _httpContextAccessor.HttpContext;

            var signOutTask = context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            signOutTask.GetAwaiter().GetResult();
        }
    }
}
