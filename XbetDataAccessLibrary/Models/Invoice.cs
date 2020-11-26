using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        [Required]
        [Timestamp]
        public DateTime CreatedTimeStamp { get; set; }

        public Subscription Subscription { get; set; }
        public int SubscriptionId { get; set; }
    }
}
