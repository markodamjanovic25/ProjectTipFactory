using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Plan
    {
        public int PlanId { get; set; }
        [Required]
        [MaxLength(25)]
        public string PlanName { get; set; }
        [MaxLength(25)]
        public string MonotonousTipsAmount { get; set; }
        [MaxLength(25)]
        public string AdventurousTipsAmount { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
        public List<Subscription> Subscriptions { get; set; }
    }
}
