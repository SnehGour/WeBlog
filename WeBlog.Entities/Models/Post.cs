using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeBlog.Entities.Models.DTOs;

namespace WeBlog.Entities.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime IsUpdatedAt { get; set; } = DateTime.Now;
        public readonly DateTime IsCreatedAt = DateTime.Now;

        [ForeignKey(nameof(AppUser))]
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }

        public PostDTO GetDTO()
        {
            return new PostDTO
            {
                Content = Content,
                SubTitle = SubTitle,
                Title = Title,
            };
        }
    }
}
