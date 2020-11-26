using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class UserType
    {
        public int UserTypeId { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserTypeName { get; set; }
        [Required]
        [MaxLength(25)]
        public string UserTypeFlag { get; set; }
        public int? Credits { get; set; }
        public List<User> Users { get; set; }
    }
}
