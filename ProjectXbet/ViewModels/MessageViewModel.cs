using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {
        [Required]
        [MinLength(4, ErrorMessage = "Subject has to be at least 4 characters long.")]
        public string Subject { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Message text has to be at least 5 characters long.")]
        public string Text { get; set; }
    }
}
