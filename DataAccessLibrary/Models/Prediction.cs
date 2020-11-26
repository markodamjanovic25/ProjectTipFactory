using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccessLibrary.Models
{
    public class Prediction
    {
        public int PredictionId { get; set; }
        [Required]
        public decimal Odds { get; set; }
        public bool IsCorrect { get; set; }
        public Match Match { get; set; }
        public Tip Tip { get; set; }
    }
}
