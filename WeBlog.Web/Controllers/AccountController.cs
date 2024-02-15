using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using WeBlog.Entities.Models;

namespace WeBlog.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuth _authService;
        readonly SignInManager<AppUser> _signInManager;
        public AccountController(IAuth authService,
            SignInManager<AppUser> signInManager)
        {
            _authService = authService;
            _signInManager = signInManager;
        }
        #region Login
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await _authService.LoginAsync(loginRequestDTO);
                if (user != null)
                {
                    // After Cheking Creadentials
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["success"] = "You are Logged In";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["error"] = "InValid UserName or Password";
            return View();

        }

        #endregion

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDTO registerRequestDTO)
        {
            if (ModelState.IsValid)
            {
                await _authService.RegisterAsync(registerRequestDTO);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        #endregion
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            TempData["error"] = "You are Logged Out";
            return RedirectToAction("Login");
        }
    }
}
