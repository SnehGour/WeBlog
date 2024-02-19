using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;
using WeBlog.Repository.Data;

namespace WeBlog.Repository
{
    public class CommentRepository : ICommentRepository
    {
        readonly ApplicationDbContext _context;
        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateComment(Comment comment)
        {
            // User
            var user = await _context.AppUsers.AsNoTracking().
                FirstOrDefaultAsync(user => user.Id == comment.AppUserId);


            // Post
            var post = await _context.Posts.AsNoTracking()
                .FirstOrDefaultAsync(post => post.Id == comment.PostId);

            if (user != null && post != null)
            {
                try
                {

                    _context.Comments.Add(comment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;

        }

        public async Task<IEnumerable<Comment>> GetAllCommentsByPostId(Guid postId)
        {
            var comments = await _context.Comments.Where(post => post.PostId == postId).ToListAsync();
            return comments;    
        }
    }
}
