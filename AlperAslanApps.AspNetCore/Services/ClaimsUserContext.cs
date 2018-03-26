using AlperAslanApps.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace AlperAslanApps.AspNetCore.Services
{
    public class ClaimsUserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsUserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Id
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;

                if (user.Identity.IsAuthenticated)
                {
                    var claim = user.FindFirst(ClaimTypes.NameIdentifier);
                    return claim.Value;
                }

                return Guid.Empty.ToString(); //system
            }
        }
    }
}
