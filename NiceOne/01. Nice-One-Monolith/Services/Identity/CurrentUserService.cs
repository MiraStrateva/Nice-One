using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace NiceOne.Services.Identity
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user != null)
            {
                this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        }
        public string UserId { get; }
    }
}
