namespace NiceOne.Services.Identity
{
    using Microsoft.AspNetCore.Http;

    using System.Security.Claims;

    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            if (user != null)
            {
                this.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
                this.UserRole = user.FindFirstValue(ClaimTypes.Role);
                this.FirstName = user.FindFirstValue("FirstName");
            }
        }
        public string UserId { get; }

        public string UserRole { get; }

        public string FirstName { get; }
    }
}
