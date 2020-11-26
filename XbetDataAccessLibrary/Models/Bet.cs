using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Bet
    {
        public int TicketId { get; set; }
        public int PredictionId { get; set; }
        public int PredictionPosition { get; set; }
        public Ticket Ticket { get; set; }
        public Prediction Prediction { get; set; }
    }
}
