using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Web.Controllers
{
    public class AccountController : Controller
    {
        readonly IAuth _authService;
        public AccountController(IAuth authService)
        {
            _authService = authService;
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
            var user = await _authService.LoginAsync(loginRequestDTO);
            if (user == null)
            {
                return View();
            }

            // creating Identity
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, user.Name)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
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
            await _authService.RegisterAsync(registerRequestDTO);
            return RedirectToAction("Index", "Home");
        }

        #endregion
        public void Logout()
        {

        }
    }
}
