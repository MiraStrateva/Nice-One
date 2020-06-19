using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NiceOne.Data.Entities;
using NiceOne.Models;
using NiceOne.Services.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NiceOne.Controllers
{
    public class AccountController : Controller
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
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistrationModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var user = mapper.Map<User>(userModel);
            var result = await identityService.RegisterAsync(user, userModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View(userModel);
            }
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = await identityService.SignInAsync(
                                                    userModel.Email, 
                                                    userModel.Password, 
                                                    userModel.RememberMe);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName or Password");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await identityService.SignOutAsync();
             
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordModel input)
        {
            var result = await identityService.ChangePasswordAsync(currentUserService.UserId, input.CurrentPassword, input.NewPassword);
            var errors = result.Errors.Select(e => e.Description);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                return BadRequest(errors);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
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
