using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models
{
    public class Search
    {
        [Required]
        public string SearchText { get; set; } = null!;
    }
}
