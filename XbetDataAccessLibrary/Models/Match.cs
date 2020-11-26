using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        public DateTime MatchDateTime { get; set; }

        [MaxLength(25)]
        public string ClubHomeName { get; set; }

        [MaxLength(25)]
        public string ClubAwayName { get; set; }

        public int? ClubHomeGoals { get; set; }
        public int? ClubHomeGoalsHalf { get; set; }

        public int? ClubAwayGoals { get; set; }
        public int? ClubAwayGoalsHalf { get; set; }

        public int LeagueId { get; set; }
        public League League { get; set; }

    }
}
