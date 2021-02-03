using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class LeagueViewModel
    {
        public League League { get; set; }
        public ICollection<Prediction> Predictions { get; set; }

        public IDictionary<Tip, decimal[]> TipStats { get; set; }
        
        //best tip
    }
}
