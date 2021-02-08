using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class BetslipViewModel : BaseViewModel
    {
        public int TicketId { get; set; }
        public decimal TotalOdds { get; set; }
        public List<Prediction> Predictions { get; set; }
    }
}
