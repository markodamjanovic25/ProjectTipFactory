using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class MatchViewModel : BaseViewModel
    {
        public int MatchId { get; set; }

        [DisplayName("Time")]
        public DateTime MatchDateTime { get; set; }
        
        [DisplayName("Home team")]
        public string ClubHomeName { get; set; }
        
        [DisplayName("Away team")]
        public string ClubAwayName { get; set; }

        [DisplayName("Home team half")]
        public int ClubHomeGoalsHalf { get; set; }

        [DisplayName("Away team half")]
        public int ClubAwayGoalsHalf { get; set; }

        [DisplayName("Home team total")]
        public int ClubHomeGoals { get; set; }

        [DisplayName("Away team total")]
        public int ClubAwayGoals { get; set; }
        [DisplayName("League")]
        public int LeagueId { get; set; }
        //public League League { get; set; }

        public List<League> Leagues;
    }
}
