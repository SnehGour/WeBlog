using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Contracts.Contracts
{
    public interface IAuth
    {
        Task RegisterAsync (RegisterRequestDTO registerRequestDTO);
        Task<AppUser> LoginAsync(LoginRequestDTO loginRequestDTO);
    }
}
