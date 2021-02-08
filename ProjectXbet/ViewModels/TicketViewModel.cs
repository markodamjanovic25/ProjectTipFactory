using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class TicketViewModel : BaseViewModel
    {
        public Ticket Ticket { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}
