namespace NiceOne.Services.Identity
{
    using Microsoft.AspNetCore.Identity;

    using NiceOne.Data.Entities;

    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
