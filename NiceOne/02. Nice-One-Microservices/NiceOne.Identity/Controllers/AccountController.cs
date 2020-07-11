namespace NiceOne.Identity.Controllers
{
    using AutoMapper;

    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Controllers;
    using NiceOne.Identity.Data.Entities;
    using NiceOne.Identity.Models;
    using NiceOne.Identity.Services;
    using NiceOne.Services.Identity;
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

        [HttpGet]
        [Route(nameof(Register))]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(UserRegistrationModel input)
        {
            var user = mapper.Map<User>(input);
            var result = await identityService.RegisterAsync(user, input.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(input);
            }
            return await Login(new UserLoginModel { 
                Email = input.Email, 
                Password = input.Password});
        }

        [HttpGet]
        [Route(nameof(Login))]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            try
            {
                var user = await identityService.LoginAsync(userModel);
                return Ok(new LoginOutputModel(user.Token));
            }
            catch (InvalidCredentialException)
            { 
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        [HttpGet]
        [Route(nameof(ChangePassword))]
        public IActionResult ChangePassword()
        {
            return View();
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
    }
}
