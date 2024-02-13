using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Contracts.Contracts;
using WeBlog.Entities.Models;
using WeBlog.Entities.Models.DTOs;
using WeBlog.Repository.Data;

namespace WeBlog.Repository
{
    public class AuthService : IAuth
    {
        readonly UserManager<AppUser> _userManager;
        readonly ApplicationDbContext _context;
        public AuthService(UserManager<AppUser> userManager,
            ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<AppUser> LoginAsync(LoginRequestDTO loginRequestDTO)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(user => user.Email == loginRequestDTO.Username);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return null;
            }

            return user;
        }

        public async Task RegisterAsync(RegisterRequestDTO registerRequestDTO)
        {
            AppUser user = new AppUser
            {
                Email = registerRequestDTO.Email,
                PhoneNumber = registerRequestDTO.PhoneNumber,
                NormalizedUserName = registerRequestDTO.Name,
                Name=registerRequestDTO.Email,
                UserName=registerRequestDTO.Name,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerRequestDTO.Password);
                
            }catch(Exception ex)
            {
                return;
            }
        }
    }
}
