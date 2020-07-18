namespace NiceOne.Identity.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Models;
    using NiceOne.Identity.Services;
    using NiceOne.Services.Identity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Authentication;
    using System.Threading.Tasks;

    public class AccountController : ApiController
    {
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;
        private readonly ICurrentUserService currentUserService;

        public AccountController(IMapper mapper, IIdentityService identityService, ICurrentUserService currentUserService)
        {
            this.mapper = mapper;
            this.identityService = identityService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult<LoginOutputModel>> Register(UserRegistrationModel input)
        {
            var user = mapper.Map<User>(input);
            var result = await identityService.RegisterAsync(user, input.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            return await Login(new UserLoginModel { 
                Email = input.Email, 
                Password = input.Password});
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginOutputModel>> Login(UserLoginModel userModel, string returnUrl = null)
        {
            try
            {
                return Ok(await this.identityService.LoginAsync(userModel));
            }
            catch (InvalidCredentialException)
            {
                return BadRequest("Invalid UserName or Password");
            }
        }

        [HttpPost]
        [Route(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel input)
        {
            var result = await identityService.ChangePasswordAsync(currentUserService.UserId, input);
            var errors = result.Errors.Select(e => e.Description);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(errors);
            }
        }

        [Route("Names")]
        public async Task<IEnumerable<UserGetModel>> GetUserNames([FromQuery]IEnumerable<string> ids)
            => await identityService.GetUserNames(ids);
    }
}
