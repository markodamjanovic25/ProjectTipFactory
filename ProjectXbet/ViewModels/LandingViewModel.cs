using DataAccessLibrary.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class LandingViewModel : BaseViewModel
    {
        public IEnumerable<Prediction> PredictionsMonotonous;
        public IEnumerable<Prediction> PredictionsPreviousMonotonous;
        public IDictionary<League, int[]> LeaguesMonotonous;
        public IDictionary<Tip, decimal[]> TipsMonotonous;

        public IEnumerable<Prediction> PredictionsAdventurous;
        public IEnumerable<Prediction> PredictionsPreviousAdventurous;
        public IDictionary<League, int[]> LeaguesAdventurous;
        public IDictionary<Tip, decimal[]> TipsAdventurous;

        public IEnumerable<Prediction> PredictionsLudicrous;
        public IEnumerable<Prediction> PredictionsPreviousLudicrous;
        public IDictionary<League, int[]> LeaguesLudicrous;
        public IDictionary<Tip, decimal[]> TipsLudicrous;

        public List<decimal[]> TipTypeStats;
        
        public LandingViewModel()
        {
            PredictionsMonotonous = new List<Prediction>();
            PredictionsPreviousMonotonous = new List<Prediction>();
            LeaguesMonotonous = new ConcurrentDictionary<League, int[]>();
            TipsMonotonous = new ConcurrentDictionary<Tip, decimal[]>();

            PredictionsAdventurous = new List<Prediction>();
            PredictionsPreviousAdventurous = new List<Prediction>();
            LeaguesAdventurous = new ConcurrentDictionary<League, int[]>();
            TipsAdventurous = new ConcurrentDictionary<Tip, decimal[]>();

            PredictionsLudicrous = new List<Prediction>();
            PredictionsPreviousLudicrous = new List<Prediction>();
            LeaguesLudicrous = new ConcurrentDictionary<League, int[]>();
            TipsLudicrous = new ConcurrentDictionary<Tip, decimal[]>();

            TipTypeStats = new List<decimal[]>();
            
        }
    }
}
