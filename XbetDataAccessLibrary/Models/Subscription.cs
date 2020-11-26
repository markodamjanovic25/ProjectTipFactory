using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Subscription
    {
        public int SubscriptionId { get; set; }

        [Timestamp]
        public DateTime StartTimeStamp { get; set; }

        [Timestamp]
        public DateTime EndTimeStamp { get; set; }

        public bool IsActive { get; set; }

        public int PlanId { get; set; }

        public string UserId { get; set; }

        public Plan Plan { get; set; }

        public User User { get; set; }

    }
}
