using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class LeagueTipDetailedViewModel : BaseViewModel
    {
        public Tip Tip { get; set; }
        public League League { get; set; }
        public decimal LeagueTotalPlayed { get; set; }
        public decimal LeagueWins { get; set; }
        public decimal LeaguePercentage { get; set; }
        public decimal LeagueAverageOdds { get; set; }
        public decimal LeagueRoi { get; set; }
        public ICollection<Prediction> Predictions { get; set; }
        
    }
}
