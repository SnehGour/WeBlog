using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeBlog.Application.Contracts;
using WeBlog.Entities.Models.DTOs;
using WeBlog.Services;
using WeBlog.Web.Models;

namespace WeBlog.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly IPost _postService;
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger,
            IPost postService,
            IMapper mapper)
        {
            _mapper = mapper;
            _postService = postService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var postList = await _postService.GetAllAsync();
            var postDTOList = _mapper.Map<List<PostDTO>>(postList);
            return View(postDTOList);
        }
    }
}
