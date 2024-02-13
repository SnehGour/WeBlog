using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models.DTOs
{
    public class PostDTO
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Content { get; set; }

        
    }
}
