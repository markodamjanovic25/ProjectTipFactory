using DataAccessLibrary.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class StatisticsViewModel : BaseViewModel
    {
        public IDictionary<League, int[]> LeagueStats;
        public IDictionary<Tip, decimal[]> TipStats;
        public ICollection<Prediction> Predictions;
        public decimal[] TipTypeStats;

        public StatisticsViewModel()
        {
            LeagueStats = new ConcurrentDictionary<League, int[]>();
            TipStats = new ConcurrentDictionary<Tip, decimal[]>();
            Predictions = new List<Prediction>();
        }
    }
}
