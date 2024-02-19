using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        public string UserName { get; set; }
        public Guid PostId { get; set; }
        public string AppUserId { get; set; }    
    }
}
