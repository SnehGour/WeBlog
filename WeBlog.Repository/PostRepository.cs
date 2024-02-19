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

        public async Task CreateAsync(Post post)
        {
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> GetAllPostAsync()
        {
            var allPostList = await _context.Posts.Include(user=>user.AppUser).ToListAsync();
            /*            List<PostDTO> posts = new List<PostDTO>();
                        foreach (var post in allPostList)
                        {
                            posts.Add(post.GetDTO());
                        }*/

            return allPostList;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                return post;
            }
            return null;
        }

        public async Task<IEnumerable<Post>> GetUserById(string id)
        {
            var postList = await _context.Posts.Where(post => post.AppUserId == id).ToListAsync(); 
            return postList;
        }

        public async Task<IEnumerable<Post>> SearchAsync(Search searchText)
        {
            var posts = await _context.Posts.
                                Where(post => post.Title.Contains(searchText.SearchText) ||
                                    post.SubTitle.Contains(searchText.SearchText) || post.AppUser.UserName.ToLower() == searchText.SearchText.ToLower())
                                .ToListAsync();

            return posts;   
        }

        public async Task<bool> UpdateAsync(Guid id, Post post)
        {
            var obj = await _context.Posts.FirstOrDefaultAsync(x => x.Id == id);
            if (obj == null)
            {
                return false;
            }
            obj.Title = post.Title;
            obj.Content = post.Content;
            obj.SubTitle = post.SubTitle;
            obj.IsUpdatedAt = DateTime.Now;

            _context.Update(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
