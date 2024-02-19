using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Services
{
    public class CommentService : IComment
    {
        readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<bool> CreateComment(Comment comment)
        {
            
            var isCreated = await _commentRepository.CreateComment(comment);
            if (isCreated)
            {
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByPostId(Guid postId)
        {
            var comments = await _commentRepository.GetAllCommentsByPostId(postId);
            return comments;
        }
    }
}
