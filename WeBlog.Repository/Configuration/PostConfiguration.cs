using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models;

namespace WeBlog.Repository.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(
                new Post
                {
                    Id = Guid.NewGuid(),
                    Title = ".NET Stack",
                    SubTitle = "All technology under .NET umbrela",
                    Content = "A .NET stack is a software development stack that uses Microsoft's .NET framework and the C# language to build software applications. It also uses an IDE like Visual Studio or VS Code, Windows OS, databases, and backend"
                });
        }
    }
}
