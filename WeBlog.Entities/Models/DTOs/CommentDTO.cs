using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models.DTOs
{
    public class CommentDTO
    {
        [Required(ErrorMessage ="Message is required")]
        public string Message { get; set; }
        public string UserName { get; set; }
        public string AppUserId { get; set; }
        public string PostId { get; set; }
    }
}
