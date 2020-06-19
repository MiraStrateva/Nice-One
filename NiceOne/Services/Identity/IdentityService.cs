using Microsoft.AspNetCore.Identity;
using NiceOne.Data.Entities;
using System.Threading.Tasks;

namespace NiceOne.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private const string DefaultUserRole = "Visitor";

        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(User user, string password)
        {
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, DefaultUserRole);
            }
            
            return result;
        }

        public async Task<SignInResult> SignInAsync(string email, string password, bool rememberMe)
            => await signInManager.PasswordSignInAsync(
                                                    email,
                                                    password,
                                                    rememberMe,
                                                    false);

        public async Task SignOutAsync()
            => await signInManager.SignOutAsync();

        public async Task<IdentityResult> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
        {
            var user = await userManager.FindByIdAsync(userId);

            return await userManager.ChangePasswordAsync(user,
                currentPassword,
                newPassword);
        }
    }
}
