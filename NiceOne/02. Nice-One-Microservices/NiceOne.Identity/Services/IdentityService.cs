namespace NiceOne.Identity.Services
{
    using Microsoft.AspNetCore.Identity;

    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Models;
    using System.Security.Authentication;
    using System.Threading.Tasks;

    public class IdentityService : IIdentityService
    {
        private const string InvalidErrorMessage = "Invalid credentials.";

        private readonly UserManager<User> userManager;
        private readonly ITokenGeneratorService jwtTokenGenerator;
        private const string DefaultUserRole = "Visitor";

        public IdentityService(UserManager<User> userManager, ITokenGeneratorService jwtTokenGenerator)
        {
            this.userManager = userManager;
            this.jwtTokenGenerator = jwtTokenGenerator;
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

        public async Task<LoginOutputModel> LoginAsync(UserLoginModel input)
        {
            var user = await this.userManager.FindByEmailAsync(input.Email);
            if (user == null)
            {
                throw new InvalidCredentialException(InvalidErrorMessage);
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, input.Password);
            if (!passwordValid)
            {
                throw new InvalidCredentialException(InvalidErrorMessage);
            }

            var token = this.jwtTokenGenerator.GenerateToken(user);

            return new LoginOutputModel(token);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordModel input)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new InvalidCredentialException(InvalidErrorMessage);
            }

            return await userManager.ChangePasswordAsync(user,
                input.CurrentPassword,
                input.NewPassword);
        }
    }
}
