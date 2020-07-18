namespace NiceOne.Client.Services.Identity
{
    using System.Threading.Tasks;
    using NiceOne.Client.Models.Identity;
    using Refit;

    public interface IIdentityService
    {
        [Post("/Account/Login")]
        Task<LoginOutputModel> Login([Body] UserLoginModel loginInput);

        [Post("/Account/Register")]
        Task<LoginOutputModel> Register([Body] UserRegistrationModel input);

        [Post("/Account/ChangePassword")]
        Task ChangePassword([Body] ChangePasswordModel input);
    }
}