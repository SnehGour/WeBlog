using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models.DTOs
{
    public class ResponseDTO
    {
        public Object? Result { get; set; }
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
    }
}
