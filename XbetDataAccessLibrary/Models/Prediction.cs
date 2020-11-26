using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Prediction
    {
        public Prediction()
        {
            this.UserRolePredictions = new HashSet<UserRolePredictions>();
        }

        public int PredictionId { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Odds { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Chance { get; set; }
        public bool? IsCorrect { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; }
        public int TipId { get; set; }
        public Tip Tip { get; set; }
        public ICollection<UserRolePredictions> UserRolePredictions { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
