using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;
using WeBlog.Entities.Models.ViewModel;

namespace WeBlog.Web.Controllers
{
    public class CommentController : Controller
    {
        readonly IComment _commentService;
        readonly IMapper _mapper;
        public CommentController(IComment commentService,IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        

        [HttpPost]
        public async Task<IActionResult> Create(CommentDTO commentDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // will give the user's userId

            if (userId != null && commentDto.Message != null)
            {
                commentDto.UserName = User.Identity.Name;
                commentDto.AppUserId = userId;
                var comment = _mapper.Map<Comment>(commentDto);
                await _commentService.CreateComment(comment);
            }
            return RedirectToAction("Details", "Post", new {id = commentDto.PostId});
        }
    }
}
