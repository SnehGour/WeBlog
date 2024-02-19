using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeBlog.Application.Contracts;
using VM = WeBlog.Entities.Models.ViewModel;
using WeBlog.Entities.Models.DTOs;
using WeBlog.Entities.Models;
using WeBlog.Contracts.Contracts;
using System.Security.Claims;
using AutoMapper;

namespace WeBlog.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPost _postService;
        private readonly IAuth _authService;
        private readonly IComment _commentService;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public PostController(ILogger<HomeController> logger,
            IPost postService,
            IComment commentService,
            IAuth authService,
            IMapper mapper)
        {
            _postService = postService;
            _logger = logger;
            _commentService = commentService;
            _authService = authService;
            _mapper = mapper;
        }

        #region Create

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(PostDTO postDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _authService.GetUserById(userId);

            if (string.IsNullOrEmpty(postDTO.Title) && string.IsNullOrEmpty(postDTO.Content) && appUser != null)
            {
                ModelState.AddModelError("Data", "Please enter valid data");
                return View();
            }
            var post = _mapper.Map<Post>(postDTO);
            post.AppUserId = userId;
            await _postService.CreateAsync(post);
            return RedirectToAction("Index", "Home");
        }

        #endregion


        #region Edit

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
            var isUpdated = await _postService.UpdateAsync(post.Id, post);

            if (!isUpdated)
            {
                return View();
            }
            TempData["success"] = "Updated successfully";
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Delete
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
            TempData["success"] = "Deleted !!!";
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            Guid postId;
            if (Guid.TryParse(id, out postId))
            {
                var comments = await _commentService.GetAllCommentsByPostId(postId);
                var post = await _postService.GetPostByIdAsync(postId);
                VM.CommentViewModel commentViewModel = new VM.CommentViewModel
                {
                    Post = post,
                    Comments = (List<Comment>)comments
                };
                return View(commentViewModel);
            }

            return View("Error");
        }
        #endregion

        #region MyPost
        [HttpGet]

        public async Task<IActionResult> MyPost()
        {
            List<PostDTO> postListDto = [];
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Account", "Login");
            }
            var postList = await _postService.GetPostByUserId(userId);
            return View(postList);
        }
        #endregion

    }
}
