namespace NiceOne.Identity.Services
{
    using Microsoft.AspNetCore.Identity;

    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(User user, string password);
        Task<LoginOutputModel> LoginAsync(UserLoginModel input);
        Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordModel input);

        Task<IEnumerable<UserGetModel>> GetUserNames(IEnumerable<string> ids);
    }
}
