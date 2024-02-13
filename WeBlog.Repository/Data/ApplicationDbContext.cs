using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models;
using WeBlog.Repository.Configuration;

namespace WeBlog.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Post>().HasData(
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = ".NET Stack",
                    SubTitle = "All technology under .NET umbrela",
                    Content = "A .NET stack is a software development stack that uses Microsoft's .NET framework and the C# language to build software applications. It also uses an IDE like Visual Studio or VS Code, Windows OS, databases, and backend"
                });
        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
