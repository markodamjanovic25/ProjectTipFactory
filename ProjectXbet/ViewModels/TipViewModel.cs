using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class TipViewModel : BaseViewModel
    {
        public Tip Tip { get; set; }
        public ICollection<Prediction> Predictions { get; set; }

        public IDictionary<Tip, decimal[]> TipStats { get; set; }
        public IDictionary<League, int[]> LeagueStats { get; set; }
    }
}
