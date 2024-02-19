using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Contracts.Contracts
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllPostAsync();
        Task CreateAsync(Post postDTO);
        Task<bool> UpdateAsync(Guid id, Post post);
        Task<Post> GetPostByIdAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Post>> GetUserById(string id);
    }
}
