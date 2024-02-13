using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeBlog.Entities.Models.DTOs
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Email should be in proper format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression("^[0-9]*$",ErrorMessage ="Phone Number should be Only Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string? Role { get; set; }
    }
}
