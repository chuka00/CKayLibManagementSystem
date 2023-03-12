using Microsoft.AspNetCore.Mvc;
using Asp.NetMVCCrud.Models;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Asp.NetMVCCrud.Controllers
{
    public class AccessController : Controller
    {
        //checks if user is already logged in
        public IActionResult LogIn()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginModel)
        {
            if (loginModel.Email == "user@example.com" && loginModel.Password == "7878")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier , loginModel.Email),
                    new Claim("OtherProperties", "Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = loginModel.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "User not found";
            return View();
        }

    }
}
