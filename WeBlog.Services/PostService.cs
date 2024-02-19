using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Application.Contracts;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Services
{
    public class PostService : IPost
    {
        private readonly IPostRepository _postRepository;
        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task CreateAsync(Post post)
        {
            await _postRepository.CreateAsync(post);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllPostAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        public Task<IEnumerable<Post>> GetPostByUserId(string userId)
        {
            return _postRepository.GetUserById(userId);
        }

        public async Task<bool> UpdateAsync(Guid id, Post post)
        {
            var isUpdated = await _postRepository.UpdateAsync(id, post);
            return isUpdated;
        }
    }
}
