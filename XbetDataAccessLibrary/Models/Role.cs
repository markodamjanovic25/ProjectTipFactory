using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Role : IdentityRole
    {
        public string RoleFlag { get; set; }
        public List<UserRolePredictions> UserRolePredictions { get; set; }
    }
}
