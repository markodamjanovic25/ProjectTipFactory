using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectXbet.ViewModels
{
    public class AdministrationViewModel : BaseViewModel
    {

        public AdministrationViewModel()
        {
            Matches = new List<Match>();
            Tips = new List<Tip>();
            Roles = new List<Role>();
            Leagues = new List<League>();
            Predictions = new List<Prediction>();
        }

        
            //Prediction model
            [Required]
            public decimal Odds { get; set; }

            public decimal Chance { get; set; }

            [DisplayName("Match")]
            public int MatchId { get; set; }

            [DisplayName("Tip")]
            public int TipId { get; set; }
        
            [DisplayName("Role")]
            public string RoleId { get; set; }

            public List<Match> Matches;
            public List<Tip> Tips;
            public List<Role> Roles;
        

            //Match model
            [Required]
            public DateTime MatchDateTime { get; set; }
            [Required]
            [DisplayName("Home team")]
            public string ClubHomeName { get; set; }
            [Required]
            [DisplayName("Away team")]
            public string ClubAwayName { get; set; }
            [DisplayName("League")]
            public int LeagueId { get; set; }

            public List<League> Leagues;


        //Predictions
        public List<Prediction> Predictions { get; set; }
        public int ClubHomeGoalsHalf { get; set; }
        public int ClubAwayGoalsHalf { get; set; }
        public int ClubHomeGoals { get; set; }
        public int ClubAwayGoals { get; set; }

    }
}
