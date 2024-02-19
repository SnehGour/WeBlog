using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Application.Contracts
{
    public interface IPost
    {
        Task CreateAsync(Post post);
        Task<Post> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllAsync();
        Task<bool> UpdateAsync(Guid id, Post post);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Post>> GetPostByUserId(string userId);
    }
}
