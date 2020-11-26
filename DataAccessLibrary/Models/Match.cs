using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Match
    {
        public int MatchId { get; set; }

        [Required]
        public DateTime MatchDateTime { get; set; }

        [Required]
        [MaxLength(25)]
        public string ClubHomeName { get; set; }

        [Required]
        [MaxLength(25)]
        public string ClubAwayName { get; set; }

        public int ClubHomeGoals { get; set; }

        public int ClubAwayGoals { get; set; }

    }
}
