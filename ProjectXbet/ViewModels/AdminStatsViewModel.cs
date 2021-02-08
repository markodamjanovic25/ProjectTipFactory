using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class AdminStatsViewModel : BaseViewModel
    {
        public List<decimal> Odds { get; set; }
        public Dictionary<League, List<int[]>> LeagueStatsByOdds { get; set; }

        public AdminStatsViewModel()
        {
            this.Odds = new List<decimal>();
            this.LeagueStatsByOdds = new Dictionary<League, List<int[]>>();
        }
    }
}
