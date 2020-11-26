using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public DateTime TimeCreated { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalOdds { get; set; }
        public string Status { get; set; }
        public bool IsCurrent { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
