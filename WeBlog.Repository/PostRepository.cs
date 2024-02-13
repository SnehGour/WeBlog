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
    public class PostRepository : IPostRepository
    {
        readonly ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(PostDTO postDTO)
        {
            var post = new Post
            {
                Content = postDTO.Content,
                SubTitle = postDTO.SubTitle,
                Title = postDTO.Title,
                IsUpdatedAt = DateTime.Now,
                Id = Guid.NewGuid(),

            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x=>x.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var allPostList = await _context.Posts.ToListAsync();
            /*List<PostDTO> posts = new List<PostDTO>();
            foreach (var post in allPostList)
            {
                posts.Add(post.GetDTO());
            }
*/
            return allPostList;
        }

     
        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> UpdateAsync(Guid id, PostDTO postDTO)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post == null)
            {
                return false;
            }
            post.Title = postDTO.Title;
            post.Content = postDTO.Content;
            post.SubTitle = postDTO.SubTitle;
            post.IsUpdatedAt = DateTime.Now;

            _context.Update(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
