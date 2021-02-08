using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class AccountViewModel : BaseViewModel
    {
        public User User { get; set; }
        public Role Role { get; set; }
        public List<Ticket> Tickets { get; set; }
        public DateTime ExpDate { get; set; }
        public List<Invoice> Invoices { get; set; }
        public List<Message> ReceivedMessages { get; set; }
        public List<Message> SentMessages { get; set; }

        public AccountViewModel()
        {
            this.Tickets = new List<Ticket>();
            this.Invoices = new List<Invoice>();
            this.ReceivedMessages = new List<Message>();
            this.SentMessages = new List<Message>();
        }

    }
}
