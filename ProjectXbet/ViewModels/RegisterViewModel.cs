using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters long.")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }      
        
        [Required]
        [Compare("Password" , ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }
    }
}
