namespace NiceOne.Client.Controllers
{
    using System;
    using System.Threading.Tasks;

    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using NiceOne.Client.Models.Identity;
    using NiceOne.Client.Services.Identity;
    using static NiceOne.Infrastructure.InfrastructureConstants;

    public class AccountController : BaseController
    {
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        public AccountController(IIdentityService identityService, IMapper mapper)
        {
            this.mapper = mapper;
            this.identityService = identityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
            => await this.Handle(
                async () =>
                {
                    var result = await this.identityService
                        .Register(userModel);

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: View(userModel));

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
            => await this.Handle(
                async () =>
                {
                    var result = await this.identityService
                        .Login(userModel);

                    this.Response.Cookies.Append(
                        AuthenticationCookieName,
                        result.Token,
                        new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            MaxAge = TimeSpan.FromDays(1)
                        });
                },
                success: RedirectToLocal(returnUrl),
                failure: View(userModel));

        [Authorize]
        public IActionResult Logout()
        {
            this.Response.Cookies.Delete(AuthenticationCookieName);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel input)
            => await this.Handle(async () => await this.identityService.ChangePassword(input),
                success: RedirectToAction(nameof(HomeController.Index), "Home"),
                failure: View());

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
