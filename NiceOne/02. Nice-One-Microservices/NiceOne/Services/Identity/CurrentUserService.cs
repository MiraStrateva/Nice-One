namespace NiceOne.Services.Identity
{
    using Microsoft.AspNetCore.Http;
    using NiceOne.Infrastructure;
    using System;
    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly ClaimsPrincipal user;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user == null)
            {
                throw new InvalidOperationException("This request does not have an authenticated user.");
            }

            this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            this.UserRole = user.FindFirstValue(ClaimTypes.Role);
            this.FirstName = user.FindFirstValue(ClaimTypes.Name);
            //this.FirstName = user.FindFirstValue("FirstName");
        }
        public string UserId { get; }

        public string UserRole { get; }

        public string FirstName { get; }

        public bool IsAdministrator => this.user.IsAdministrator();
    }
}
