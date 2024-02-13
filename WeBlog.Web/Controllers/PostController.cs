using Microsoft.AspNetCore.Mvc;
using WeBlog.Application.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly ILogger<HomeController> _logger;

        public PostController(ILogger<HomeController> logger,
            IPost postService)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PostDTO post)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data", "Please enter valid data");
                return View();
            }
            await _postService.CreateAsync(post);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var isValid = Guid.TryParse(id, out Guid postId);
            if (isValid)
            {
                var post = await _postService.GetPostByIdAsync(postId);
                return View(post);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Data", "Please enter valid data");
                return View();
            }
            var isUpdated = await _postService.UpdateAsync(post.Id, post.GetDTO());

            if (!isUpdated)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var isValid = Guid.TryParse(id, out Guid postId);
            if (isValid)
            {
                var post = await _postService.GetPostByIdAsync(postId);
                return View(post);
            }
            return View("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Post post)
        {

            if (post.Id == null)
            {
                ModelState.AddModelError("Data", "Please enter valid data");
                return View();
            }
            await _postService.DeleteAsync(post.Id);
            return RedirectToAction("Index", "Home");
        }
    }
}
