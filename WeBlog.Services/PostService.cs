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
        public async Task CreateAsync(PostDTO postDTO)
        {
            await _postRepository.CreateAsync(postDTO);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _postRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            return await _postRepository.GetAllPostAsync();
        }

        public Task<Post> GetPostByIdAsync(Guid id)
        {
            return _postRepository.GetPostByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Guid id, PostDTO postDTO)
        {
            var isUpdated = await _postRepository.UpdateAsync(id, postDTO);
            return isUpdated;
        }
    }
}
