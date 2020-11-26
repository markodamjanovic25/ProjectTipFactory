using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class UserRolePredictions
    {
        public string UserRoleId { get; set; }
        public int PredictionId { get; set; }

        public Role UserRole { get; set; }
        public Prediction Prediction { get; set; }
    }
}
