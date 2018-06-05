using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportStore.Models.ViewModels;
using SportStore.Models;

namespace SportStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        public AccountController(  UserManager<IdentityUser> userMan,
        SignInManager<IdentityUser> signInMan)
        {
            userManager = userMan;
            signInManager = signInMan;
            IdentitySeedData.EnsurePopulated(userManager).Wait();
        }
        [AllowAnonymous]
        public ViewResult Login(string returnUrl) { return View(new LoginModel { returnUrl=returnUrl}); }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await userManager.FindByNameAsync(loginModel.Name);
                if (user!=null)
                {
                    await signInManager.SignOutAsync();
                    if ((await signInManager.PasswordSignInAsync(user,loginModel.password,false,false)).Succeeded)
                    {
                        return Redirect(loginModel?.returnUrl??"/Admin/Index");
                    }
                }
            }
            ModelState.AddModelError("","Invalid name or password.");
            return View(loginModel);
        }
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManager.SignOutAsync();
            return Redirect(returnUrl);
        }
    }
}