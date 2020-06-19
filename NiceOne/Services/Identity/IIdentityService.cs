using Microsoft.AspNetCore.Identity;
using NiceOne.Data.Entities;
using System.Threading.Tasks;

namespace NiceOne.Services.Identity
{
    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<SignInResult> SignInAsync(string email, string password, bool rememberMe);
        Task SignOutAsync();
        Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    }
}
