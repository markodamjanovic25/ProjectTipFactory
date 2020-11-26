using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class League
    {
        public int LeagueId { get; set; }

        [Required]
        [MaxLength(25)]
        public string LeagueName { get; set; }

        [MaxLength(25)]
        [Column(TypeName = "varchar(50)")]
        public string LeagueFlag { get; set; }

        public ICollection<Match> Matches { get; set; }
    }
}
