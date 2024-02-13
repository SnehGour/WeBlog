using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeBlog.Application.Contracts;
using WeBlog.Services;
using WeBlog.Web.Models;

namespace WeBlog.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IPost _postService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IPost postService)
        {
            _postService = postService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            TempData["success"] = "You are Logged In";
            var postList = await _postService.GetAllAsync(); 
            return View(postList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
